using JustBDD.Core.Contexts.Stores;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace JustBDD.NUnit.TestProperties;

public static class PropertyBagExtensions
{
    private const string SuiteStoreKey = "SuiteStore";
    private const string FeatureStoreKey = "FeatureStore";
    private const string ScenarioStoreKey = "ScenarioStore";

    public static ContextStore GetSuiteStore(this IPropertyBag propertyBag)
    {
        return (ContextStore)propertyBag.Get(SuiteStoreKey)!;
    }

    public static ContextStore GetSuiteStore(this TestContext.PropertyBagAdapter propertyBag)
    {
        return (ContextStore)propertyBag.Get(SuiteStoreKey)!;
    }

    public static void SetSuiteStore(this IPropertyBag propertyBag, ContextStore suiteStore)
    {
        propertyBag.Set(SuiteStoreKey, suiteStore);
    }

    public static ContextStore GetFeatureStore(this IPropertyBag propertyBag)
    {
        return (ContextStore)propertyBag.Get(FeatureStoreKey)!;
    }

    public static ContextStore GetFeatureStore(this TestContext.PropertyBagAdapter propertyBag)
    {
        return (ContextStore)propertyBag.Get(FeatureStoreKey)!;
    }

    public static void SetFeatureStore(this IPropertyBag propertyBag, ContextStore featureStore)
    {
        propertyBag.Set(FeatureStoreKey, featureStore);
    }

    public static ContextStore GetScenarioStore(this TestContext.PropertyBagAdapter propertyBag)
    {
        return (ContextStore)propertyBag.Get(ScenarioStoreKey)!;
    }

    public static void SetScenarioStore(this IPropertyBag propertyBag, ContextStore featureStore)
    {
        propertyBag.Set(ScenarioStoreKey, featureStore);
    }
}
