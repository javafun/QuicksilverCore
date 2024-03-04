using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediachase.Commerce;

namespace QuicksilverCore.Web.Features.Market.Services;
public interface ICurrencyService
{
    IEnumerable<Currency> GetAvailableCurrencies();
    Currency GetCurrentCurrency();
    bool SetCurrentCurrency(string currencyCode);
}
