# JustBDD

## Credits

This library is an enhanced copy of a BDD framework that was originally developed Darren Hall [@Darren166](https://github.com/Darren166).
The original design of a BDD syntax that is in the core of JustBDD belongs to him, my implementation just extends it a bit for a more convenient and extendable usage.

## Test example:
``` C#
    public class GetAllCategoriesApi_HappyPath_Should : TestFixtureBase
    {
        [Test]
        public void ReturnAllCategories()
        {
            var existingCategory = new CategoryBuilder()
                .WithRandomValues()
                .Build();

            var expectedResponse = new CategoryResponseItemBuilder()
                .FromDomainModel(existingCategory)
                .Build();

            Given.IHave.LoggedInAs.AValidUser().And.DatabaseHas.Category(existingCategory);
            When.ICall.TheCategoriesApi.GetAllEndpoint();
            Then.TheCall.WillSucceed().And.TheCall.WillHaveAResponseEqualTo(new[] { expectedResponse });
        }
    }
 ```
