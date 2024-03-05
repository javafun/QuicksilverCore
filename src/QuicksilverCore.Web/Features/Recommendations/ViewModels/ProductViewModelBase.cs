using EPiServer.Personalization.Commerce.Tracking;

namespace QuicksilverCore.Web.Features.Recommendations.ViewModels;
public abstract class ProductViewModelBase
{
    public IList<string> Images { get; set; }
    public IEnumerable<Recommendation> AlternativeProducts { get; set; }
    public IEnumerable<Recommendation> CrossSellProducts { get; set; }
    public bool SkipTracking { get; set; }
}
