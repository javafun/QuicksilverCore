using EPiServer.SpecializedProperties;
using QuicksilverCore.Web.Features.Cart.ViewModels;
using QuicksilverCore.Web.Features.Start.Pages;

namespace QuicksilverCore.Web.Features.Navigation.ViewModels;
public class NavigationViewModel
{
    public ContentReference CurrentContentLink { get; set; }
    public StartPage StartPage { get; set; }
    public LinkItemCollection UserLinks { get; set; }
    public MiniCartViewModel MiniCart { get; set; }
    public WishListMiniCartViewModel WishListMiniCart { get; set; }
}
