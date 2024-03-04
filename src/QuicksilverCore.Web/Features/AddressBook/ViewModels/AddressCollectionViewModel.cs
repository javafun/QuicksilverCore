

using QuicksilverCore.Web.Features.AddressBook.Pages;
using QuicksilverCore.Web.Features.Shared.Models;
using QuicksilverCore.Web.Features.Shared.ViewModels;

namespace EPiServer.Reference.Commerce.Site.Features.AddressBook.ViewModels;

public class AddressCollectionViewModel : PageViewModel<AddressBookPage>
{
    public IEnumerable<AddressModel> Addresses { get; set; }
}
