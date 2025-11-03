using IMS.Domain.Entities;
using IMS.Business.DTOs.Responses;
using Mapster;

namespace IMS.Business.Configurations;

public static class MapsterConfig
{
    public static void Configure()
    {
        // Apply global configuration to prevent circular references
        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
        TypeAdapterConfig.GlobalSettings.Default.MaxDepth(3);
        
        // Configure AblRevenue mapping to prevent circular references
        TypeAdapterConfig<AblRevenue, AblRevenueRes>.NewConfig()
            .MaxDepth(2) // Limit recursion depth to prevent stack overflow
            .PreserveReference(true)
            .Map(dest => dest.Children, src => src.Children ?? new List<AblRevenue>())
            .Map(dest => dest.ParentAccount, src => src.ParentAccount, srcCond => srcCond.ParentAccount != null);

        // Configure the reverse mapping as well
        TypeAdapterConfig<AblRevenueRes, AblRevenue>.NewConfig()
            .MaxDepth(2)
            .PreserveReference(true)
            .Map(dest => dest.Children, src => src.Children ?? new List<AblRevenueRes>())
            .Map(dest => dest.ParentAccount, src => src.ParentAccount, srcCond => srcCond.ParentAccount != null);
            
        // Special configuration to break circular reference at parent level
        TypeAdapterConfig<AblRevenue, AblRevenueRes>.NewConfig()
            .Map(dest => dest.ParentAccount, src => src.ParentAccount == null ? null : 
                new AblRevenueRes 
                { 
                    Id = src.ParentAccount.Id,
                    Description = src.ParentAccount.Description,
                    Listid = src.ParentAccount.Listid,
                    ParentAccountId = src.ParentAccount.ParentAccountId,
                    DueDate = src.ParentAccount.DueDate,
                    FixedAmount = src.ParentAccount.FixedAmount,
                    Paid = src.ParentAccount.Paid,
                    // Explicitly set to null to break circular reference
                    Children = null,
                    ParentAccount = null
                });
    }
}