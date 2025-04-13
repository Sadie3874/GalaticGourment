using Assignment1_v2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Assignment1_v2
{
    public class GalacticGourmetContext : DbContext
    {
        // creating the db sets of all the models 
        // DbSets are a collection of entities to be expandable through queries
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Planet> Planets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Retriving the directory and the file of the data base to access
            var projectRoot = Directory.GetCurrentDirectory();
            var dbPath = Path.Combine(projectRoot, "local.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Defining Primary Keys, required feilds and relationships to create the table
            
            // Primary key of Planet = PlanetId
            modelBuilder.Entity<Planet>() 
                .HasKey(p => p.PlanetId);

            // Primary key of Dish = DishId
            modelBuilder.Entity<Dish>() 
                .HasKey(d => d.DishID);

            // Primary key of Chef = StarChefId
            modelBuilder.Entity<Chef>() 
                .HasKey(c => c.StarChefId);

            // Primary key of Ingredient = IngredientId
            modelBuilder.Entity<Ingredient>() 
                .HasKey(i => i.IngredientId);

            // Planet must have a name, it is required 
            modelBuilder.Entity<Planet>() 
                .Property(p => p.Name)
                .IsRequired();

            // Chef must have a name, it is required 
            modelBuilder.Entity<Chef>()  
                .Property(c => c.Name)
                .IsRequired();

            // Dish must have a name, it is required 
            modelBuilder.Entity<Dish>() 
                .Property(d => d.Name)
                .IsRequired();

            // Ingredient must have a name, it is required  
            modelBuilder.Entity<Ingredient>() 
                .Property(i => i.Name)
                .IsRequired();

            // Chef has a one to one relationship with planet 
            modelBuilder.Entity<Chef>() 
                .HasOne(d => d.Planet)
                .WithOne(p => p.Chef)
                .HasForeignKey<Chef>(c => c.StarChefId)
                .IsRequired(false);

            // Planet has a one to one relationship with chef 
            modelBuilder.Entity<Planet>() 
                .HasOne(d => d.Chef)
                .WithOne(p => p.Planet)
                .HasForeignKey<Planet>(c => c.StarChefId)
                .IsRequired(false);

            // Dish has a many to many relationship with Ingredient
            modelBuilder.Entity<Dish>() 
                .HasMany(d => d.Ingredients)
                .WithMany(p => p.Dishes);

            // Planet has a one to many relationship with Dish
            modelBuilder.Entity<Dish>() 
                .HasOne(d => d.Planet)
                .WithMany(p => p.Dishes)
                .HasForeignKey(p => p.PlanetID);

            // Dish has a many to one relationship wiht Planet 
            modelBuilder.Entity<Planet>() 
                .HasMany(d => d.Dishes)
                .WithOne(p => p.Planet)
                .HasForeignKey(p => p.PlanetID);

            // Dish has a one to many relationhip with Chef
            modelBuilder.Entity<Dish>() 
                .HasOne(d => d.Chef)
                .WithMany(p => p.Dishes);

        }
    }
}

