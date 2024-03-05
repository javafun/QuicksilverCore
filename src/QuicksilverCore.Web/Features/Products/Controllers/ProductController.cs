using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using QuicksilverCore.Web.Extensions;
using QuicksilverCore.Web.Features.Products.Models;
using QuicksilverCore.Web.Features.Products.ViewModelFactories;
using QuicksilverCore.Web.Infrastructure.Facades;

namespace QuicksilverCore.Web.Features.Products.Controllers;
public class ProductController : ContentController<FashionProduct>
{
    private readonly bool _isInEditMode;
    private readonly CatalogEntryViewModelFactory _viewModelFactory;

    public ProductController(IsInEditModeAccessor isInEditModeAccessor, CatalogEntryViewModelFactory viewModelFactory)
    {
        _isInEditMode = isInEditModeAccessor();
        _viewModelFactory = viewModelFactory;
    }

    [HttpGet]
    public ActionResult Index(FashionProduct currentContent, string entryCode = "", bool useQuickview = false, bool skipTracking = false)
    {
        var viewModel = _viewModelFactory.Create(currentContent, entryCode);
        viewModel.SkipTracking = skipTracking;

        if (_isInEditMode && viewModel.Variant == null)
        {
            var emptyViewName = "ProductWithoutEntries";
            return !Request.IsAjaxRequest() ? (ActionResult)View(emptyViewName, viewModel) : PartialView(emptyViewName, viewModel);
        }

        if (viewModel.Variant == null)
        {
            return NotFound();
        }

        if (useQuickview)
        {
            return PartialView("_Quickview", viewModel);
        }
        return Request.IsAjaxRequest() ? PartialView(viewModel) : (ActionResult)View(viewModel);
    }

    [HttpPost]
    public ActionResult SelectVariant(FashionProduct currentContent, string color, string size, bool useQuickview = false)
    {
        var variant = _viewModelFactory.SelectVariant(currentContent, color, size);
        if (variant != null)
        {
            return RedirectToAction("Index", new { entryCode = variant.Code, useQuickview, skipTracking = true });
        }

        return NotFound();
    }
}
