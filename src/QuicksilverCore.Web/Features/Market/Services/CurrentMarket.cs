using Mediachase.Commerce;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Markets;
using QuicksilverCore.Web.Features.Market.Models;

namespace QuicksilverCore.Web.Features.Market.Services;
public class CurrentMarket : ICurrentMarket
{
    private const string MarketCookie = "MarketId";
    private static readonly MarketId DefaultMarketId = new MarketId("US");
    private readonly IMarketService _marketService;
    private readonly ICookieService _cookieService;

    public CurrentMarket(IMarketService marketService, ICookieService cookieService)
    {
        _marketService = marketService;
        _cookieService = cookieService;
    }

    public IMarket GetCurrentMarket()
    {
        var currentMarket = _cookieService.Get(MarketCookie);
        if (string.IsNullOrEmpty(currentMarket))
        {
            currentMarket = DefaultMarketId.Value;
        }

        return GetMarket(new MarketId(currentMarket));
    }

    public void SetCurrentMarket(MarketId marketId)
    {
        var market = GetMarket(marketId);
        SiteContext.Current.Currency = market.DefaultCurrency;
        _cookieService.Set(MarketCookie, marketId.Value);
    }

    private IMarket GetMarket(MarketId marketId)
    {
        return _marketService.GetMarket(marketId) ?? _marketService.GetMarket(DefaultMarketId) ?? new EmptyMarket();
    }
}
