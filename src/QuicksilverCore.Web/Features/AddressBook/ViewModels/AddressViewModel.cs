using QuicksilverCore.Web.Features.AddressBook.Pages;
using QuicksilverCore.Web.Features.Shared.Models;
using QuicksilverCore.Web.Features.Shared.ViewModels;

namespace QuicksilverCore.Web.Features.AddressBook.ViewModels;

public class AddressViewModel : PageViewModel<AddressBookPage>
{
    public AddressModel Address { get; set; }
}
