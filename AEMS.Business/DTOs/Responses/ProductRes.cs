using IMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace IMS.Business.DTOs.Requests;

public class ProductRes

{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string ProductCode { get; set; }
    public string? SKU { get; set; }
    public string Description { get; set; }
    public Attachment? FeaturedImage { get; set; }
    public List<Attachment>? Images { get; set; }
    public string? ProductType { get; set; }
    public string? ProductCost { get; set; }
    public Guid UnitId { get; set; }
    public string Unit { get; set; }
    public Guid CategoryId { get; set; }
    public string Category { get; set; }
    public Guid BrandId { get; set; }
    public string Brand { get; set; }
    public int? Stock { get; set; }
    public string? StockAlert { get; set; }
}