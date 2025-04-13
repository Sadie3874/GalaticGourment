using System.Numerics;
using Assignment1_v2.Models;

namespace Assignment1_v2
{
    public class Program
    {
        // Given for the assignment.
        public static void Main(string[] args)
        {
            using (var context = new GalacticGourmetContext())
            {
                // Ensure database is created
                context.Database.EnsureCreated();

                if (!context.Dishes.Any())
                    CreateData(context);

                // Create an instance of Queries
                var queries = new Queries(context);

                // Run the program loop
                RunQueriesLoop(queries);
            }
        }
        static void RunQueriesLoop(Queries queries)
        {
            while (true)
            {
                Console.WriteLine("\nSelect a query to run:");
                Console.WriteLine("1. List all dishes along with their originating planet and chef");
                Console.WriteLine("2. Find all dishes that use a specific ingredient");
                Console.WriteLine("3. Get all allergenic ingredients in dishes from a specific planet");
                Console.WriteLine("4. Count the number of dishes each chef specializes in");
                Console.WriteLine("5. List chefs who specialize in dishes with a specific spice level");
                Console.WriteLine("0. Exit");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        queries.ListAllDishesWithPlanetAndChef();
                        break;
                    case "2":
                        Console.Write("Enter ingredient name: ");
                        var ingredientName = Console.ReadLine();
                        queries.FindDishesByIngredient(ingredientName);
                        break;
                    case "3":
                        Console.Write("Enter planet name: ");
                        var planetName = Console.ReadLine();
                        queries.GetAllergenicIngredientsByPlanet(planetName);
                        break;
                    case "4":
                        queries.CountDishesPerChef();
                        break;
                    case "5":
                        Console.Write("Enter spice level (Mild, Medium, Hot): ");
                        var spiceLevel = Console.ReadLine();
                        queries.ListChefsBySpiceLevel(spiceLevel);
                        break;
                    case "0":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid input. Please select a valid option.");
                        break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void CreateData(GalacticGourmetContext context)
        {
            // Create Planets
            var planet1 = new Planet { Name = "Arrakis", Galaxy = "Milky Way" };
            var planet2 = new Planet { Name = "Pandora", Galaxy = "Alpha Centauri" };

            // Create Chefs
            var chef1 = new Chef { Name = "Gordon", ExperienceLevel = "Expert", Planet = planet1 };
            var chef2 = new Chef { Name = "Jamie", ExperienceLevel = "Intermediate", Planet = planet2 };

            planet1.Chef = chef1;
            planet2.Chef = chef2;

            // Create Ingredients
            var ingredient1 = new Ingredient { Name = "Spice Melange", IsAllergenic = true };
            var ingredient2 = new Ingredient { Name = "Unobtanium", IsAllergenic = false };
            var ingredient3 = new Ingredient { Name = "Vibranium", IsAllergenic = true };

            // Create Dishes
            var dish1 = new Dish
            {
                Name = "Spice Stew",
                SpiceLevel = "Hot",
                Planet = planet1,
                Chef = chef1,
                Ingredients = new List<Ingredient> { ingredient1 }
            };

            var dish2 = new Dish
            {
                Name = "Pandoran Salad",
                SpiceLevel = "Mild",
                Planet = planet2,
                Chef = chef2,
                Ingredients = new List<Ingredient> { ingredient2 }
            };

            var dish3 = new Dish
            {
                Name = "Vibranium Pie",
                SpiceLevel = "Medium",
                Planet = planet1,
                Chef = chef1,
                Ingredients = new List<Ingredient> { ingredient3 }
            };

            // Add data to context
            context.Planets.AddRange(planet1, planet2);
            context.Chefs.AddRange(chef1, chef2);
            context.Ingredients.AddRange(ingredient1, ingredient2, ingredient3);
            context.Dishes.AddRange(dish1, dish2, dish3);

            context.SaveChanges();
        }
    }
    
}
