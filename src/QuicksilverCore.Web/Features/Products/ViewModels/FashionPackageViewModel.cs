using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer.Commerce.Catalog.ContentTypes;
using Mediachase.Commerce;
using QuicksilverCore.Web.Features.Products.Models;
using QuicksilverCore.Web.Features.Recommendations.ViewModels;

namespace QuicksilverCore.Web.Features.Products.ViewModels;
public class FashionPackageViewModel : ProductViewModelBase
{
    public FashionPackage Package { get; set; }
    public Money? DiscountedPrice { get; set; }
    public Money ListingPrice { get; set; }
    public IEnumerable<CatalogContentBase> Entries { get; set; }
    public bool IsAvailable { get; set; }
}
