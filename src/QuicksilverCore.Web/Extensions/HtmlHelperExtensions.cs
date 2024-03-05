using Mediachase.Commerce;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QuicksilverCore.Web.Extensions;
public static class HtmlHelperExtensions
{
    public static IHtmlContent RenderMoney(this IHtmlHelper htmlHelper, Money money)
    {
        var moneySpan = money.ToString().Replace(money.Currency.Format.CurrencySymbol, "<span class=\"product-price__currency-marker\">" + money.Currency.Format.CurrencySymbol + "</span>");
        return new HtmlString(moneySpan);
    }
}
