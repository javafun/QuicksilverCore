using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediachase.Commerce;

namespace QuicksilverCore.Web.Features.Cart.ViewModels;
public class LargeCartViewModel
{
    public IEnumerable<ShipmentViewModel> Shipments { get; set; }

    public Money TotalDiscount { get; set; }

    public Money Total { get; set; }
}
