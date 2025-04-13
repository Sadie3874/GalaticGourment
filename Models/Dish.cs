namespace Assignment1_v2.Models
{
    public class Dish
    {
        // Setting table relationship for Dish 
        // Each dish will have an id, name, spicelevel and the planetid they belong to 
        public int DishID { get; set; }
        public string Name { get; set; }
        public string SpiceLevel { get; set; }
        public int? PlanetID { get; set; }

        // Dishes can have many ingredients but only one chef and plantet
        public List<Ingredient> Ingredients { get; set; }
        public Chef Chef { get; set; }
        public Planet Planet;
    }
}
