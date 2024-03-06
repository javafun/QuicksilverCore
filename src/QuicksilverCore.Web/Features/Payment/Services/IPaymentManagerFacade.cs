using Mediachase.Commerce.Orders.Dto;

namespace QuicksilverCore.Web.Features.Payment.Services;
public interface IPaymentManagerFacade
{
    PaymentMethodDto GetPaymentMethodBySystemName(string name, string languageId);

    PaymentMethodDto GetPaymentMethodsByMarket(string marketId);

    void SavePaymentMethod(PaymentMethodDto dto);
}
