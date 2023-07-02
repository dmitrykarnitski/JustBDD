﻿using NUnit.Framework;
using Sample.Api.ComponentTests.Framework.FixtureSupport;
using Sample.Api.ComponentTests.TestHelpers.Builders.Categories.Api;
using Sample.Api.ComponentTests.TestHelpers.Builders.Categories.Domain;

namespace Sample.Api.ComponentTests.Tests.Categories;

[TestFixture]
public class CategoriesApi_GetByIdEndpoint_HappyPath_Should : TestFixtureBase
{
    [Test]
    public void ReturnExistingCategoryById()
    {
        var existingCategory = new CategoryBuilder()
            .WithRandomValues()
            .Build();

        var expectedResponse = new CategoryResponseItemBuilder()
            .FromDomainModel(existingCategory)
            .Build();

        Given.IHave.LoggedInAs.AValidUser().And.Database.HasCategory(existingCategory);
        When.ICall.TheCategoriesApi.GetByIdEndpoint(existingCategory.Id);
        Then.TheCall.WillSucceed().And.TheCall.WillHaveAResponse.EqualTo(expectedResponse);
    }
}
