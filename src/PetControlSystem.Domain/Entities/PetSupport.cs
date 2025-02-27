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

        public PetSupport() { }

        public PetSupport(string? name, decimal? smallDogPrice, decimal? mediumDogPrice, decimal? largeDogPrice, List<Appointment>? appointments)
        {
            Name = name;
            SmallDogPrice = smallDogPrice;
            MediumDogPrice = mediumDogPrice;
            LargeDogPrice = largeDogPrice;
            Appointments = appointments;
        }
    }
}
