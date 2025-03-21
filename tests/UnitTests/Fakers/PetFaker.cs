using PetControlSystem.Domain.Entities;

namespace UnitTests.Fakers
{
    public static class PetFaker
    {
        public static Pet GetValidPet() 
        {
            return new Pet("Maya", "Kind and playful", 5, Guid.NewGuid());
        }
    }
}