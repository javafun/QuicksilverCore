using EPiServer.Framework.Localization;
using EPiServer.SpecializedProperties;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;
using QuicksilverCore.Web.Features.Cart.Services;
using QuicksilverCore.Web.Features.Cart.ViewModelFactories;
using QuicksilverCore.Web.Features.Navigation.ViewModels;
using QuicksilverCore.Web.Features.Start.Pages;

namespace QuicksilverCore.Web.Features.Navigation.Components;
public class Navigation : ViewComponent
{
    private readonly IContentLoader _contentLoader;
    private readonly ICartService _cartService;
    private readonly LocalizationService _localizationService;
    private readonly CartViewModelFactory _cartViewModelFactory;
    private readonly LinkGenerator _linkGenerator;
    private readonly IUrlResolver _urlResolver;

    public Navigation(IContentLoader contentLoader,
                        ICartService cartService,
                        LocalizationService localizationService,
                        CartViewModelFactory cartViewModelFactory,
                        LinkGenerator linkGenerator,
                        IUrlResolver urlResolver)
    {
        _contentLoader = contentLoader;
        _cartService = cartService;
        _localizationService = localizationService;
        _cartViewModelFactory = cartViewModelFactory;
        _linkGenerator = linkGenerator;
        _urlResolver = urlResolver;
    }

    public IViewComponentResult Invoke(IContent currentContent)
    {
        var cart = _cartService.LoadCart(_cartService.DefaultCartName);

        var wishlist = _cartService.LoadCart(_cartService.DefaultWishListName);
        var startPage = _contentLoader.Get<StartPage>(ContentReference.StartPage);

        var viewModel = new NavigationViewModel
        {
            StartPage = startPage,
            CurrentContentLink = currentContent?.ContentLink,
            UserLinks = new LinkItemCollection(),
            MiniCart = _cartViewModelFactory.CreateMiniCartViewModel(cart),
            WishListMiniCart = _cartViewModelFactory.CreateWishListMiniCartViewModel(wishlist)
        };

        if (HttpContext.User.Identity.IsAuthenticated)
        {
            var rightMenuItems = startPage.RightMenu;
            if (rightMenuItems != null)
            {
                viewModel.UserLinks.AddRange(rightMenuItems);
            }
            
            viewModel.UserLinks.Add(new LinkItem
            {
                
                Href = _linkGenerator.GetPathByAction("SignOut", "Login"),
            Text = _localizationService.GetString("/Header/Account/SignOut")
            });
        }
        else
        {
            viewModel.UserLinks.Add(new LinkItem
            {
                Href = _linkGenerator.GetPathByAction("Index", "Login", new { returnUrl = currentContent != null ? _urlResolver.GetUrl(currentContent.ContentLink) : "/" }),
                Text = _localizationService.GetString("/Header/Account/SignIn")
            });
        }

        return View(viewModel);
    }
}
