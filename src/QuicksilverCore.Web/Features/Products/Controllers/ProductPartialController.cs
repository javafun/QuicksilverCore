using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using QuicksilverCore.Web.Features.Products.Services;

namespace QuicksilverCore.Web.Features.Products.Controllers;

[TemplateDescriptor(Inherited = true)]
public class ProductPartialController : PartialContentComponent<ProductContent>
{
    private readonly IProductService _productService;

    public ProductPartialController(IProductService productService)
    {
        _productService = productService;
    }

    protected override IViewComponentResult InvokeComponent(ProductContent currentContent)
    {
        return View(_productService.GetProductTileViewModel(currentContent));
    }
}
