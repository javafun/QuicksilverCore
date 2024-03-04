using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuicksilverCore.Web.Features.Shared.Pages;
public class MailBasePage : PageData
{
    [CultureSpecific]
    [Display(
        Name = "Mail Title",
        Description = "",
        GroupName = SystemTabNames.Content,
        Order = 1)]
    public virtual string MailTitle { get; set; }
}
