namespace Assignment1_v2.Models
{
    public class Ingredient
    {
        // Setting table relationship for Ingredient 
        // Each ingredient has its own id, name and a bool if it is an allergenic
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public bool IsAllergenic { get; set; }

        // Each ingredient can belong to many dishes 
        public List<Dish> Dishes { get; set; }
        // the Dish id the ingredient is appart of.
        public int DishID { get; set; }
    }
}
