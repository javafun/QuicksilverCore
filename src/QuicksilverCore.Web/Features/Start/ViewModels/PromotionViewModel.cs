using System.ComponentModel.DataAnnotations;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Marketing;
using EPiServer.Web;

namespace QuicksilverCore.Web.Features.Start.ViewModels;
public class PromotionViewModel
{
    public string Name { get; set; }

    [UIHint(UIHint.Image)]
    public ContentReference BannerImage { get; set; }

    public IEnumerable<CatalogContentBase> Items { get; set; }

    public CatalogItemSelectionType SelectionType { get; set; }
}
