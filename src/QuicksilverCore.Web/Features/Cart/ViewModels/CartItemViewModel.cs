using EPiServer.Commerce.Catalog.ContentTypes;
using Mediachase.Commerce;
using QuicksilverCore.Web.Features.Shared.Models;

namespace QuicksilverCore.Web.Features.Cart.ViewModels;
public class CartItemViewModel : IProductModel
{
    public string DisplayName { get; set; }

    public string ImageUrl { get; set; }

    public string Url { get; set; }

    public string Brand { get; set; }

    public Money? DiscountedPrice { get; set; }

    public Money PlacedPrice { get; set; }

    public string Code { get; set; }

    public EntryContentBase Entry { get; set; }

    public decimal Quantity { get; set; }

    public Money? DiscountedUnitPrice { get; set; }

    public IEnumerable<string> AvailableSizes { get; set; }

    public bool IsAvailable { get; set; }

    public string AddressId { get; set; }

    public bool IsGift { get; set; }
}
