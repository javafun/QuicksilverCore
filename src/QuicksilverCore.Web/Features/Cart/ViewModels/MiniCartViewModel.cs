using Mediachase.Commerce;

namespace QuicksilverCore.Web.Features.Cart.ViewModels;
public class MiniCartViewModel
{
    public ContentReference CheckoutPage { get; set; }

    public decimal ItemCount { get; set; }

    public IEnumerable<ShipmentViewModel> Shipments { get; set; }

    public Money Total { get; set; }
}
