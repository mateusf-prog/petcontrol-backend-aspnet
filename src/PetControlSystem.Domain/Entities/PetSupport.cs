namespace PetControlSystem.Domain.Entities
{
    public class PetSupport : Entity
    {
        public string? Name { get; private set; }
        public decimal? SmallDogPrice { get; private set; }
        public decimal? MediumDogPrice { get; private set; }
        public decimal? LargeDogPrice { get; private set; }

        /* EF Relations */
        public List<Appointment>? Appointments { get; private set; }
    }
}
