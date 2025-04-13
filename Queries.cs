using Assignment1_v2.Models;
using Microsoft.EntityFrameworkCore;
namespace Assignment1_v2
{
    public class Queries
    {
        // creating a class to reference the Context in file and not have it be passed through as a variable 
        public GalacticGourmetContext Context { get; set; }    
        public Queries(GalacticGourmetContext context) 
        {
            Context = context;
        }

        // Listing all dishes along with their originating planet and chef.
        public void ListAllDishesWithPlanetAndChef()
        {
            var dishWithOriginPlanet = Context.Dishes
                .Include(d => d.Planet) // adding planet and chef to the table
                .Include(d => d.Chef)
                .Select(n => new // creating new object with specific inputs 
                {
                    Name = n.Name,
                    Planet = n.Planet.Name,
                    Chef = n.Chef.Name
                }).ToList(); // making everything into a list to print

            // for loop to print the list 
            foreach (var dishes in dishWithOriginPlanet) 
            {
                // printing the values of the selected table
                Console.WriteLine($"Dish: {dishes.Name}, Planet: {dishes.Planet}, Chef: {dishes.Chef}");
            }
        }

        // Find all dishes that use a specific ingredient
        public void FindDishesByIngredient(string ingredientName)
        {
            var findIgredients = Context.Dishes
                .Where(d => d.Ingredients.Any(i => i.Name == ingredientName)) // checking for dishes with the ingredient name
                .Select(n => n.Name).ToList(); // selecting the name to use as our value also making it a list. 

            foreach (var ingredients in findIgredients) // printing the list 
            {
                Console.WriteLine($"Dishes contating: {ingredientName} \n- {ingredients}"); 
            }
        }

        // Get all ingredients that are allergenic in dishes from a specific planet.
        public void GetAllergenicIngredientsByPlanet(string planetName) 
        {
            var allergenicIngredient = Context.Ingredients
                .Where(i => i.IsAllergenic) // checking if ingredient allergic is true 
                .Where(i => i.Dishes.Any(d => d.Planet.Name == planetName)).ToList(); // adding dishes to check planet name for each dish 
            
            Console.WriteLine($"Allergenic ingredients from planet: {planetName} ");
            foreach (var allergenic in allergenicIngredient) // priting the list 
            {
                Console.WriteLine($"- {allergenic.Name}");
            }
        }

        // Count the number of dishes each chef specializes in.
        public void CountDishesPerChef() 
        {
            var ChefsDishSpecialty = Context.Dishes
                .Where(d => d.Chef.Dishes.Count() > 0) // checking to see if chef dish is not 0 
                .Select(n => new 
                {
                    Dish = n.Chef.Dishes.Count(), // creating a new object and counting the amount dishes chef has 
                    Chef = n.Chef.Name // adding the chef name to the new table 
                }).ToList();

            Console.WriteLine("Number of dishes chef specializes in: ");
            foreach (var chef in ChefsDishSpecialty)
            {
                
                Console.WriteLine($"Chef: {chef.Chef}, Dish: {chef.Dish}"); // printing out the list 
            }
        }

        // List chefs who specialize in dishes with a specific spice level.
        public void ListChefsBySpiceLevel(string spiceLevel)
        {
            // adding dishes to check the spice level of the dish is the == to the spice level variable 
            var ChefsWithSpiceLevel = Context.Chefs 
                .Where(c => c.Dishes.Any(d => d.SpiceLevel == spiceLevel)).ToList(); // making it into a list

            Console.WriteLine($"Number of dishes chef specializes in spice level {spiceLevel}: ");
            foreach (var ChefspiceLevel in ChefsWithSpiceLevel)
            {
                Console.WriteLine($"- {ChefspiceLevel.Name}"); // printing the list 
            }
        }
    }
}
