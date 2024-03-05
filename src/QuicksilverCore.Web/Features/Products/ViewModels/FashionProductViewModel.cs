using Mediachase.Commerce;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuicksilverCore.Web.Features.Products.Models;
using QuicksilverCore.Web.Features.Recommendations.ViewModels;

namespace QuicksilverCore.Web.Features.Products.ViewModels;
public class FashionProductViewModel : ProductViewModelBase
{
    public FashionProduct Product { get; set; }
    public Money? DiscountedPrice { get; set; }
    public Money ListingPrice { get; set; }
    public FashionVariant Variant { get; set; }
    public IList<SelectListItem> Colors { get; set; }
    public IList<SelectListItem> Sizes { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public bool IsAvailable { get; set; }
}
