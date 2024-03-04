using EPiServer.Commerce.Catalog.ContentTypes;
using QuicksilverCore.Web.Features.Products.ViewModels;

namespace QuicksilverCore.Web.Features.Products.Services;
public interface IProductService
{
    ProductTileViewModel GetProductTileViewModel(EntryContentBase entry);
    ProductTileViewModel GetProductTileViewModel(ContentReference contentLink);
    string GetSiblingVariantCodeBySize(string siblingCode, string size);
}
