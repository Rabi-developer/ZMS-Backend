using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ZMS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using ZMS.API.Middleware;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
/*using IMS.Domain.Migrations;
*/
namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChargesController : BaseController<ChargesController, IChargesService, ChargesReq, ChargesRes, Charges>
{
    private readonly IConfiguration _configuration;
    public ChargesController(ILogger<ChargesController> logger, IChargesService service, IConfiguration configuration) : base(logger, service)
    {
        _configuration = configuration;
    }

    [HttpPost("status")]
    [Permission("Organization", "Update")]
    public async Task<IActionResult> UpdateStatus([FromBody] ChargesStatus contractstatus)
    {
        try
        {
            var result = await Service.UpdateStatusAsync((Guid)contractstatus.Id, contractstatus.Status);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while updating the contract status.");
        }
    }

    [HttpGet("charges-payments")]
    public async Task<IActionResult> GetChargesPayments()
    {
        var list = new List<ChargePaymentRes>();

        string query = @"
        SELECT *
        FROM (
            -- 1. Charges + Their Payments
            SELECT
                ch.ChargeNo,
                COALESCE(TRY_CAST(ch.OrderNo AS NVARCHAR(50)), '') AS OrderNo,
                bo.VehicleNo,
                m.ChargesDesc AS Charge,
                ISNULL(cl.Amount, 0) AS ChargeAmount,
                p.PaymentNo,
                ISNULL(pai.PaidAmount, 0) AS PaidAmount,
                ISNULL(pay.TotalPaid, 0) AS TotalPaid,
                CASE 
                    WHEN ISNULL(cl.Amount, 0) - ISNULL(pay.TotalPaid, 0) < 0 THEN 0 
                    ELSE ISNULL(cl.Amount, 0) - ISNULL(pay.TotalPaid, 0) 
                END AS RemainingBalance,
                CONVERT(VARCHAR(10), COALESCE(ch.ChargeDate, p.PaymentDate), 23) AS RefDate
            FROM Charges ch
            LEFT JOIN BookingOrder bo ON TRY_CAST(ch.OrderNo AS NVARCHAR(50)) = TRY_CAST(bo.OrderNo AS NVARCHAR(50))
            LEFT JOIN ChargeLine cl ON ch.Id = cl.ChargesId
            LEFT JOIN Munshyana m ON TRY_CONVERT(UNIQUEIDENTIFIER, cl.Charge) = m.Id
            LEFT JOIN PaymentABLItem pai ON TRY_CAST(ch.OrderNo AS NVARCHAR(50)) = TRY_CAST(pai.OrderNo AS NVARCHAR(50))
            LEFT JOIN PaymentABL p ON pai.PaymentABLId = p.Id
            LEFT JOIN (
                SELECT 
                    CAST(OrderNo AS NVARCHAR(50)) AS OrderNo,
                    SUM(PaidAmount) AS TotalPaid
                FROM PaymentABLItem 
                GROUP BY CAST(OrderNo AS NVARCHAR(50))
            ) pay ON TRY_CAST(ch.OrderNo AS NVARCHAR(50)) = pay.OrderNo

            UNION ALL

            -- 2. Standalone Payments (No Charge)
            SELECT
                NULL AS ChargeNo,
                COALESCE(TRY_CAST(pai.OrderNo AS NVARCHAR(50)), '') AS OrderNo,
                bo.VehicleNo,
                NULL AS Charge,
                0 AS ChargeAmount,
                p.PaymentNo,
                ISNULL(pai.PaidAmount, 0) AS PaidAmount,
                ISNULL(pai.PaidAmount, 0) AS TotalPaid,
                0 AS RemainingBalance,
                CONVERT(VARCHAR(10), p.PaymentDate, 23) AS RefDate
            FROM PaymentABLItem pai
            LEFT JOIN PaymentABL p ON pai.PaymentABLId = p.Id
            LEFT JOIN BookingOrder bo ON TRY_CAST(pai.OrderNo AS NVARCHAR(50)) = TRY_CAST(bo.OrderNo AS NVARCHAR(50))
            LEFT JOIN Charges ch ON TRY_CAST(pai.OrderNo AS NVARCHAR(50)) = TRY_CAST(ch.OrderNo AS NVARCHAR(50))
            WHERE ch.Id IS NULL   -- More reliable than checking OrderNo

            UNION ALL

            -- 3. Opening Balance
            SELECT
                CAST(ob.OpeningNo AS NVARCHAR(50)) AS ChargeNo,
                COALESCE(TRY_CAST(obe.BiltyNo AS NVARCHAR(50)), '') AS OrderNo,
                obe.VehicleNo,
                m.ChargesDesc AS Charge,
                ISNULL(obe.Debit, 0) AS ChargeAmount,
                NULL AS PaymentNo,
                ISNULL(obe.Credit, 0) AS PaidAmount,
                ISNULL(pay2.TotalPaid, ISNULL(obe.Credit, 0)) AS TotalPaid,
                CASE 
                    WHEN ISNULL(obe.Debit, 0) - ISNULL(pay2.TotalPaid, ISNULL(obe.Credit, 0)) < 0 THEN 0 
                    ELSE ISNULL(obe.Debit, 0) - ISNULL(pay2.TotalPaid, ISNULL(obe.Credit, 0)) 
                END AS RemainingBalance,
                CONVERT(VARCHAR(10), obe.BiltyDate, 23) AS RefDate
            FROM OpeningBalanceEntry obe
            LEFT JOIN OpeningBalances ob ON obe.OpeningBalanceId = ob.Id
            LEFT JOIN (
                SELECT 
                    CAST(OrderNo AS NVARCHAR(50)) AS OrderNo,
                    SUM(PaidAmount) AS TotalPaid
                FROM PaymentABLItem 
                GROUP BY CAST(OrderNo AS NVARCHAR(50))
            ) pay2 ON TRY_CAST(obe.BiltyNo AS NVARCHAR(50)) = pay2.OrderNo
            LEFT JOIN Munshyana m ON TRY_CONVERT(UNIQUEIDENTIFIER, obe.ChargeType) = m.Id
            WHERE obe.ChargeType IS NOT NULL
        ) AS FinalData
        ORDER BY RefDate, OrderNo, ChargeNo;
    ";

        using SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("AEMSConnection"));

        using SqlCommand cmd = new SqlCommand(query, conn);

        try
        {
            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new ChargePaymentRes
                {
                    ChargeNo = reader["ChargeNo"] == DBNull.Value ? null : reader["ChargeNo"].ToString(),
                    OrderNo = reader["OrderNo"] == DBNull.Value ? null : reader["OrderNo"].ToString(),
                    VehicleNo = reader["VehicleNo"] == DBNull.Value ? null : reader["VehicleNo"].ToString(),
                    Charge = reader["Charge"] == DBNull.Value ? null : reader["Charge"].ToString(),
                    ChargeAmount = reader["ChargeAmount"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["ChargeAmount"]),
                    PaymentNo = reader["PaymentNo"] == DBNull.Value ? null : reader["PaymentNo"].ToString(),
                    PaidAmount = reader["PaidAmount"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["PaidAmount"]),
                    TotalPaid = reader["TotalPaid"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["TotalPaid"]),
                    RemainingBalance = reader["RemainingBalance"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["RemainingBalance"]),
                    RefDate = reader["RefDate"] == DBNull.Value ? null : reader["RefDate"].ToString()
                });
            }

            return Ok(list);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error fetching charges and payments", error = ex.Message });
        }
    }
}




