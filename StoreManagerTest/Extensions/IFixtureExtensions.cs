using AutoFixture;

namespace StoreManagerTest.Extensions;

public static class IFixtureExtensions
{
    public static IFixture FixCircularReference(this IFixture fixture)
    {
        fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        return fixture;
    }
}
