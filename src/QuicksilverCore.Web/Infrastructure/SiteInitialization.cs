using EPiServer.Commerce.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using Mediachase.Commerce;
using QuicksilverCore.Web.Features.Market.Services;
using QuicksilverCore.Web.Infrastructure.Facades;

namespace QuicksilverCore.Web.Infrastructure;
[ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
public class SiteInitialization : IConfigurableModule
{
    public void ConfigureContainer(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<ICurrentMarket, CurrentMarket>();
        context.Services.AddTransient<IsInEditModeAccessor>(locator => () => locator.GetInstance<IContextModeResolver>().CurrentMode == ContextMode.Edit);
    }

    public void Initialize(InitializationEngine context)
    {
        CatalogRouteHelper.MapDefaultHierarchialRouter(false);

    }

    public void Uninitialize(InitializationEngine context)
    {
        //Add uninitialization logic
    }
}