using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuicksilverCore.Web.Features.Shared.Models;

namespace QuicksilverCore.Web.Features.Cart.ViewModels;
public class ShipmentViewModel
{
    public int ShipmentId { get; set; }

    public IList<CartItemViewModel> CartItems { get; set; }

    public AddressModel Address { get; set; }

    public Guid ShippingMethodId { get; set; }

    public IEnumerable<ShippingMethodViewModel> ShippingMethods { get; set; }
}
