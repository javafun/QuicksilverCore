using Mediachase.Commerce;

namespace QuicksilverCore.Web.Features.Cart.ViewModels;
public class ShippingMethodViewModel
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public Money Price { get; set; }
}
