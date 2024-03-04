using System.Globalization;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using Mediachase.Commerce;

namespace QuicksilverCore.Web.Features.Market.Services;

[ServiceConfiguration]
public class LanguageService : IUpdateCurrentLanguage
{
    private const string LanguageCookie = "Language";
    private readonly ICurrentMarket _currentMarket;
    private readonly ICookieService _cookieService;
    private readonly IUpdateCurrentLanguage _defaultUpdateCurrentLanguage;

    public LanguageService(ICurrentMarket currentMarket, ICookieService cookieService, IUpdateCurrentLanguage defaultUpdateCurrentLanguage)
    {
        _currentMarket = currentMarket;
        _cookieService = cookieService;
        _defaultUpdateCurrentLanguage = defaultUpdateCurrentLanguage;
    }

    public virtual IEnumerable<CultureInfo> GetAvailableLanguages()
    {
        return CurrentMarket.Languages;
    }

    public virtual CultureInfo GetCurrentLanguage()
    {
        CultureInfo cultureInfo;
        return TryGetLanguage(_cookieService.Get(LanguageCookie), out cultureInfo)
            ? cultureInfo
            : CurrentMarket.DefaultLanguage;
    }

    public void UpdateLanguage(string languageId)
    {
        var chosenLanguage = languageId;
        var cookieLanguage = _cookieService.Get(LanguageCookie);

        if (string.IsNullOrEmpty(chosenLanguage))
        {
            if (cookieLanguage != null)
            {
                chosenLanguage = cookieLanguage;
            }
            else
            {
                var currentMarket = _currentMarket.GetCurrentMarket();
                if (currentMarket?.DefaultLanguage != null)
                {
                    chosenLanguage = currentMarket.DefaultLanguage.Name;
                }
            }
        }

        _defaultUpdateCurrentLanguage.UpdateLanguage(chosenLanguage);

        if (cookieLanguage == null || cookieLanguage != chosenLanguage)
        {
            _cookieService.Set(LanguageCookie, chosenLanguage);
        }
    }

    public void UpdateReplacementLanguage(IContent currentContent, string requestedLanguage)
    {
        _defaultUpdateCurrentLanguage.UpdateReplacementLanguage(currentContent, requestedLanguage);
    }

    private bool TryGetLanguage(string language, out CultureInfo cultureInfo)
    {
        cultureInfo = null;

        if (language == null)
        {
            return false;
        }

        try
        {
            var culture = CultureInfo.GetCultureInfo(language);
            cultureInfo = GetAvailableLanguages().FirstOrDefault(c => c.Name == culture.Name);
            return cultureInfo != null;
        }
        catch (CultureNotFoundException)
        {
            return false;
        }
    }

    public void SetRoutedContent(IContent currentContent, string requestedLanguage)
    {
        var chosenLanguage = requestedLanguage;
        var cookieLanguage = _cookieService.Get(LanguageCookie);

        if (string.IsNullOrEmpty(chosenLanguage))
        {
            if (cookieLanguage != null)
            {
                chosenLanguage = cookieLanguage;
            }
            else
            {
                var currentMarket = _currentMarket.GetCurrentMarket();
                if (currentMarket != null && currentMarket.DefaultLanguage != null)
                {
                    chosenLanguage = currentMarket.DefaultLanguage.Name;
                }
            }
        }

        _defaultUpdateCurrentLanguage.SetRoutedContent(currentContent, chosenLanguage);

        if (cookieLanguage == null || cookieLanguage != chosenLanguage)
        {
            _cookieService.Set(LanguageCookie, chosenLanguage);
        }
    }

    private IMarket CurrentMarket => _currentMarket.GetCurrentMarket();
}
