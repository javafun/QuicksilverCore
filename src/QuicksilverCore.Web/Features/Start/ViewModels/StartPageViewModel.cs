using EPiServer.Personalization.Commerce.Tracking;
using QuicksilverCore.Web.Features.Start.Pages;

namespace QuicksilverCore.Web.Features.Start.ViewModels;
public class StartPageViewModel
{
    public StartPage StartPage { get; set; }
    public IEnumerable<PromotionViewModel> Promotions { get; set; }
    public IEnumerable<Recommendation> Recommendations { get; set; }
}
