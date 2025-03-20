using Bogus;

namespace UnitTests.Fakers
{
    public static class CreateUsingPrivateConstructor
    {
        public static Faker<T> UsePrivateConstructor<T>(this Faker<T> faker) where T : class
        {
            return faker.CustomInstantiator(f => Activator.CreateInstance(typeof(T), nonPublic: true) as T);
        }        
    }
}