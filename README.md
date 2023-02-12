# JustBDD

## Credits

This library is an enhanced copy of a BDD framework that was originally developed Darren Hall [@Darren166](https://github.com/Darren166).
The original design of a BDD syntax that is in the core of JustBDD belongs to him, my implementation just extends it a bit for a more convenient and extendable usage.

## Description

Comparison to other libraries:
- Specflow. Specflow is a great BDD library to use for complex specification-first development scenarios. JustBDD is intended to be used when there is no BDD specification available at the beginnging.
- LightBDD. LightBDD is also a great library, but it does not facilitate the structure of the tests as well does not facilitate code reusability.
- BDDfy. Almost the same as LightBDD.
- to be added...

Pros:
- Simple. Subjectively, it is way easier to write human-readable scenarios compared to Specflow
- Lightweight. It can be fully forked and redesigned for a specific project if needed. There is not too much code in there.
- Maintainability. The library forces to have a structure for test step methods.
- Code reusability. Allows ultimate reusability of tests steps which facilitates TDD.
- Readability. Test body looks like human readable text, so test can be used for onboarding new people on the project.

Cons:
- Custom. Anyways, the library is something custom. Solution: in such case - just copy it fully and adjust for your needs.
- No parallelization. There are some static things that block parallelization support. Solution: there is no solution for now, however - the library use case is high-level API tests, so parallelization there most-likely may cause random tests failures or complex data setup.
- No asyn/await support out of the box. Solution: no solution here for now, but in the future integration with LightBDD may help to resolve this issue.

Future plans:
- Provide more samples
- Provide integration with LightBDD for prettier output
- Provide more out-of-box helpers, e.g. logging, authentication, etc.

## Test example:

See samples folder for an example of usage. The example is not a guidelines, but just one of many possible ways to use the library.

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
