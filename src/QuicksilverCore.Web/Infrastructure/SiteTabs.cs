using System.ComponentModel.DataAnnotations;
using EPiServer.Security;

namespace QuicksilverCore.Web.Infrastructure;
[GroupDefinitions]
public static class SiteTabs
{
    [Display(Order = 100)]
    [RequiredAccess(AccessLevel.Edit)]
    public const string SiteStructure = "Site structure";

    [Display(Order = 110)]
    [RequiredAccess(AccessLevel.Edit)]
    public const string MailTemplates = "Mail templates";
}
