using System.ComponentModel;
using EPiServer.Framework.Localization;

namespace QuicksilverCore.Web.Infrastructure.Attributes;
public class LocalizedDisplayAttribute : DisplayNameAttribute
{
    public LocalizedDisplayAttribute(string displayNameKey)
        : base(displayNameKey)
    {
    }

    public override string DisplayName
    {
        get
        {
            var s = LocalizationService.Current.GetString(base.DisplayName);
            return string.IsNullOrWhiteSpace(s) ? base.DisplayName : s;
        }
    }
}
