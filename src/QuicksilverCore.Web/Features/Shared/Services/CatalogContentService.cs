using System.Globalization;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Filters;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Catalog.Objects;
using QuicksilverCore.Web.Features.Market.Services;
using QuicksilverCore.Web.Features.Products.Models;


namespace QuicksilverCore.Web.Features.Shared.Services;
[ServiceConfiguration(typeof(CatalogContentService), Lifecycle = ServiceInstanceScope.Singleton)]
public class CatalogContentService
{
    private readonly IContentLoader _contentLoader;
    private readonly IRelationRepository _relationRepository;
    private readonly ICurrentMarket _currentMarket;
    private readonly LanguageResolver _languageResolver;
    private readonly ReferenceConverter _referenceConverter;
    private readonly LanguageService _languageService;
    private readonly IPublishedStateAssessor _publishedStateAssessor;

    public CatalogContentService(
        IContentLoader contentLoader,
        IRelationRepository relationRepository,
        ICurrentMarket currentMarket,
        LanguageResolver languageResolver,
        ReferenceConverter referenceConverter,
        LanguageService languageService,
        IPublishedStateAssessor publishedStateAssessor)
    {
        _contentLoader = contentLoader;
        _relationRepository = relationRepository;
        _currentMarket = currentMarket;
        _languageResolver = languageResolver;
        _referenceConverter = referenceConverter;
        _languageService = languageService;
        _publishedStateAssessor = publishedStateAssessor;
    }

    public virtual IEnumerable<T> GetVariants<T>(PackageContent currentContent) where T : VariationContent
    {
        return GetVariants<T>(currentContent.GetEntries(_relationRepository), _languageResolver.GetPreferredCulture());
    }

    public virtual IEnumerable<T> GetVariants<T>(FashionBundle currentContent) where T : VariationContent
    {
        return GetVariants<T>(currentContent.GetEntries(_relationRepository), _languageResolver.GetPreferredCulture());
    }

    public virtual IEnumerable<T> GetVariants<T>(ProductContent currentContent) where T : VariationContent
    {
        return GetVariants<T>(currentContent.GetVariants(_relationRepository), _languageResolver.GetPreferredCulture());
    }

    public virtual T GetFirstVariant<T>(ProductContent currentContent) where T : VariationContent
    {
        var firstVariantLink = currentContent.GetVariants(_relationRepository).FirstOrDefault();
        return ContentReference.IsNullOrEmpty(firstVariantLink) ? null : _contentLoader.Get<T>(firstVariantLink);
    }

    public virtual IEnumerable<T> GetVariants<T>(IEnumerable<ContentReference> contentLinks, CultureInfo cultureInfo) where T : VariationContent
    {
        return _contentLoader
            .GetItems(contentLinks, cultureInfo)
            .OfType<T>()
            .Where(v => v.IsAvailableInCurrentMarket(_currentMarket) &&  _publishedStateAssessor.IsPublished(v));
    }

    public virtual IEnumerable<T> GetAllVariants<T>(IEnumerable<FashionProduct> products, string language) where T : VariationContent
    {
        return products
            .SelectMany(x => _contentLoader.GetItems(x.GetVariants(_relationRepository), CultureInfo.GetCultureInfo(language))
            .OfType<T>());
    }

    public virtual IEnumerable<T> GetAllVariants<T>(FashionProduct product, string language) where T : VariationContent
    {
        return _contentLoader
            .GetItems(product.GetVariants(_relationRepository), CultureInfo.GetCultureInfo(language))
            .OfType<T>();
    }

    public virtual IEnumerable<T> GetSiblingVariants<T>(string code) where T : VariationContent
    {
        var productRelations = _relationRepository.GetParents<ProductVariation>(_referenceConverter.GetContentLink(code));
        var siblingsRelations = _relationRepository.GetChildren<ProductVariation>(productRelations.First().Parent);
        return GetVariants<T>(siblingsRelations.Select(x => x.Child), _languageResolver.GetPreferredCulture());
    }

    public virtual T GetParentProduct<T>(EntryContentBase entry) where T : ProductContent
    {
        return Get<T>(entry.GetParentProducts(_relationRepository).SingleOrDefault());
    }

    public virtual T Get<T>(ContentReference contentLink) where T : CatalogContentBase
    {
        return _contentLoader.Get<T>(contentLink, _languageService.GetCurrentLanguage());
    }

    public virtual T Get<T>(string code) where T : EntryContentBase
    {
        return _contentLoader.Get<T>(_referenceConverter.GetContentLink(code), _languageService.GetCurrentLanguage());
    }

    public virtual T Get<T>(string code, string language) where T : EntryContentBase
    {
        return _contentLoader.Get<T>(_referenceConverter.GetContentLink(code), CultureInfo.GetCultureInfo(language));
    }

    public virtual bool TryGet<T>(string code, out T product) where T : EntryContentBase
    {
        return _contentLoader.TryGet(_referenceConverter.GetContentLink(code), out product);
    }

    public virtual IEnumerable<T> GetItems<T>(IEnumerable<string> codes) where T : EntryContentBase
    {
        return _contentLoader.GetItems(
            codes
                .Select(x => _referenceConverter.GetContentLink(x))
                .Where(r => !ContentReference.IsNullOrEmpty(r)),
            _languageResolver.GetPreferredCulture()).OfType<T>();
    }

    public virtual IEnumerable<FashionProduct> GetProducts(EntryContentBase entryContent, string language)
    {
        switch (entryContent.ClassTypeId)
        {
            case EntryType.Package:
                return _contentLoader.GetItems(((PackageContent)entryContent).GetEntries(), CultureInfo.GetCultureInfo(language)).OfType<FashionProduct>();

            case EntryType.Bundle:
                return _contentLoader.GetItems(((BundleContent)entryContent).GetEntries(), CultureInfo.GetCultureInfo(language)).OfType<FashionProduct>();

            case EntryType.Product:
                return new[] { entryContent as FashionProduct };
        }

        return Enumerable.Empty<FashionProduct>();
    }

    public virtual string GetTopCategoryName(EntryContentBase content)
    {
        var parent = _contentLoader.Get<CatalogContentBase>(content.ParentLink);
        var catalog = parent as CatalogContent;
        if (catalog != null)
        {
            return catalog.Name;
        }

        var node = parent as NodeContent;
        return node != null ? GetTopCategory(node).DisplayName : String.Empty;
    }

    private NodeContent GetTopCategory(NodeContent node)
    {
        var parentNode = _contentLoader.Get<CatalogContentBase>(node.ParentLink) as NodeContent;
        return parentNode != null ? GetTopCategory(parentNode) : node;
    }
}
