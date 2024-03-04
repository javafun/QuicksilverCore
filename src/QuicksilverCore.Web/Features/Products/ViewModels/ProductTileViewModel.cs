using Mediachase.Commerce;
using QuicksilverCore.Web.Features.Shared.Models;

namespace QuicksilverCore.Web.Features.Products.ViewModels;
public class ProductTileViewModel : IProductModel
{
    public string DisplayName { get; set; }
    public string ImageUrl { get; set; }
    public string Url { get; set; }
    public string Brand { get; set; }
    public Money? DiscountedPrice { get; set; }
    public Money PlacedPrice { get; set; }
    public string Code { get; set; }
    public bool IsAvailable { get; set; }
}
