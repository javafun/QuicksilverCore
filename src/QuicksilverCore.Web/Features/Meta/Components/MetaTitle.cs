using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;
using QuicksilverCore.Web.Features.Start.Pages;


namespace QuicksilverCore.Web.Features.Meta.Components;
public class MetaTitle : ViewComponent
{
    private readonly IContentLoader _contentLoader;
    private readonly IContentRouteHelper _contentRouteHelper;
    private const string FormatPlaceholder = "{title}";

    public MetaTitle(IContentLoader contentLoader, IContentRouteHelper contentRouteHelper)
    {
        _contentLoader = contentLoader;
        _contentRouteHelper = contentRouteHelper;
    }

    public IViewComponentResult Invoke()
    {
        var content = _contentRouteHelper.Content;
        if (content == null)
        {
            return View<string>(string.Empty);
        }

        var product = content as EntryContentBase;
        if (product != null)
        {
            // Note: If this product is placed in more than one category, we might pick the wrong category here
            var parentContent = _contentLoader.Get<CatalogContentBase>(content.ParentLink);

            var node = parentContent as NodeContent;
            string title;
            if (node != null)
            {
                title = node.SeoInformation.Title ?? node.DisplayName;
            }
            else
            {
                title = parentContent.Name;
            }
            return View<string>(FormatTitle(
                $"{product.SeoInformation.Title ?? product.DisplayName} - {title}"));
        }

        var category = content as NodeContent;
        if (category != null)
        {
            return View<string>(FormatTitle(category.SeoInformation.Title ?? category.DisplayName));
        }

        var startPage = content as StartPage;
        return startPage != null ?
            View<string>(startPage.Title ?? startPage.Name) :
            View<string>(content.Name);
    }

    private string FormatTitle(string title)
    {
        var format = _contentLoader.Get<StartPage>(ContentReference.StartPage).TitleFormat;
        if (string.IsNullOrWhiteSpace(format) || !format.Contains(FormatPlaceholder))
        {
            return title;
        }
        return format.Replace(FormatPlaceholder, title);
    }

}
