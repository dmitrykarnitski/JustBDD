using JustBDD.Core.Contexts.Stores;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace JustBDD.NUnit.TestProperties;

public static class PropertyBagExtensions
{
    private const string SuiteStoreKey = "SuiteStore";
    private const string FeatureStoreKey = "FeatureStore";
    private const string ScenarioStoreKey = "ScenarioStore";

    public static IContextStore? GetSuiteStore(this TestContext testContext)
    {
        return (IContextStore?)testContext.Test.Properties.Get(SuiteStoreKey);
    }

    internal static void SetSuiteStore(this IPropertyBag propertyBag, IContextStore suiteStore)
    {
        propertyBag.Set(SuiteStoreKey, suiteStore);
    }

    public static IContextStore? GetFeatureStore(this TestContext testContext)
    {
        return (IContextStore?)testContext.Test.Properties.Get(FeatureStoreKey);
    }

    internal static void SetFeatureStore(this IPropertyBag propertyBag, IContextStore featureStore)
    {
        propertyBag.Set(FeatureStoreKey, featureStore);
    }

    public static IContextStore? GetScenarioStore(this TestContext testContext)
    {
        return (IContextStore?)testContext.Test.Properties.Get(ScenarioStoreKey);
    }

    internal static void SetScenarioStore(this IPropertyBag propertyBag, IContextStore featureStore)
    {
        propertyBag.Set(ScenarioStoreKey, featureStore);
    }
}
