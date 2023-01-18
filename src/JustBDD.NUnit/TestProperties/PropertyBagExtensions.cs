using JustBDD.Core.Contexts.Stores;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace JustBDD.NUnit.TestProperties;

public static class PropertyBagExtensions
{
    private const string _suiteStoreKey = "SuiteStore";
    private const string _featureStoreKey = "FeatureStore";
    private const string _scenarioStoreKey = "ScenarioStore";

    public static ContextStore GetSuiteStore(this IPropertyBag propertyBag)
    {
        return (ContextStore)propertyBag.Get(_suiteStoreKey)!;
    }

    public static ContextStore GetSuiteStore(this TestContext.PropertyBagAdapter propertyBag)
    {
        return (ContextStore)propertyBag.Get(_suiteStoreKey)!;
    }

    public static void SetSuiteStore(this IPropertyBag propertyBag, ContextStore suiteStore)
    {
        propertyBag.Set(_suiteStoreKey, suiteStore);
    }

    public static ContextStore GetFeatureStore(this IPropertyBag propertyBag)
    {
        return (ContextStore)propertyBag.Get(_featureStoreKey)!;
    }

    public static ContextStore GetFeatureStore(this TestContext.PropertyBagAdapter propertyBag)
    {
        return (ContextStore)propertyBag.Get(_featureStoreKey)!;
    }

    public static void SetFeatureStore(this IPropertyBag propertyBag, ContextStore featureStore)
    {
        propertyBag.Set(_featureStoreKey, featureStore);
    }

    public static ContextStore GetScenarioStore(this TestContext.PropertyBagAdapter propertyBag)
    {
        return (ContextStore)propertyBag.Get(_scenarioStoreKey)!;
    }

    public static void SetScenarioStore(this IPropertyBag propertyBag, ContextStore featureStore)
    {
        propertyBag.Set(_scenarioStoreKey, featureStore);
    }
}
