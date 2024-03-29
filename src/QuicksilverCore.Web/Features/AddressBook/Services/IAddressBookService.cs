using EPiServer.Commerce.Order;
using EPiServer.Reference.Commerce.Site.Features.AddressBook.ViewModels;
using Mediachase.Commerce.Customers;
using QuicksilverCore.Web.Features.AddressBook.Pages;
using QuicksilverCore.Web.Features.Cart.ViewModels;
using QuicksilverCore.Web.Features.Shared.Models;

namespace QuicksilverCore.Web.Features.AddressBook.Services;
public interface IAddressBookService
{
    AddressCollectionViewModel GetAddressBookViewModel(AddressBookPage addressBookPage);
    IList<AddressModel> List();
    bool CanSave(AddressModel addressModel);
    void Save(AddressModel addressModel);
    void Delete(string addressId);
    void SetPreferredBillingAddress(string addressId);
    void SetPreferredShippingAddress(string addressId);
    CustomerAddress GetPreferredBillingAddress();
    void LoadAddress(AddressModel addressModel);
    void LoadCountriesAndRegionsForAddress(AddressModel addressModel);
    IEnumerable<string> GetRegionsByCountryCode(string countryCode);
    void MapToAddress(AddressModel addressModel, IOrderAddress orderAddress);
    void MapToAddress(AddressModel addressModel, CustomerAddress customerAddress);
    void MapToModel(CustomerAddress customerAddress, AddressModel addressModel);
    IOrderAddress ConvertToAddress(AddressModel addressModel, IOrderGroup orderGroup);
    AddressModel ConvertToModel(IOrderAddress orderAddress);
    IList<AddressModel> MergeAnonymousShippingAddresses(IList<AddressModel> addresses, IEnumerable<CartItemViewModel> cartItems);
    bool UseBillingAddressForShipment();
}
