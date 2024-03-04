using EPiServer.Personalization.Commerce;
using EPiServer.ServiceLocation;
using EPiServer.Web;

namespace QuicksilverCore.Web.Extensions;
public static class CommercePersonalizationExtensions
{
    public static bool ShouldRenderTrackingSection(this HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext, nameof(httpContext));

        var _personalizationClientConfiguration  = ServiceLocator.Current.GetInstance<PersonalizationClientConfiguration>();
        var editMode = ServiceLocator.Current.GetInstance<IContextModeResolver>().CurrentMode == ContextMode.Edit;
        return _personalizationClientConfiguration.TrackingEnabled && !editMode;
    }
}
