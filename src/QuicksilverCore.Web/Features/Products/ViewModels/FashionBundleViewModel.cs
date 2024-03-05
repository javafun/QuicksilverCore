using EPiServer.Commerce.Catalog.ContentTypes;
using QuicksilverCore.Web.Features.Products.Models;
using QuicksilverCore.Web.Features.Recommendations.ViewModels;

namespace QuicksilverCore.Web.Features.Products.ViewModels;

public class FashionBundleViewModel : ProductViewModelBase
{
    public FashionBundle Bundle { get; set; }
    public IEnumerable<EntryContentBase> Entries { get; set; }
}
