using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuicksilverCore.Web.Infrastructure.Attributes;

namespace QuicksilverCore.Web.Features.Shared.ViewModels;
public class CountryRegionViewModel
{
    public IEnumerable<string> RegionOptions { get; set; }

    [LocalizedDisplay("/Shared/Address/Form/Label/CountryRegion")]
    public string Region { get; set; }
}
