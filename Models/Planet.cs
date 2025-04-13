namespace Assignment1_v2.Models
{
    public class Planet
    {
        // Table relationships for Planet class 
        // each planet will have its own id, name and galaxy 
        public int PlanetId { get; set; }
        public string Name { get; set; }
        public string Galaxy { get; set; }

        // Planet can have many dishes, this is done by a list. 
        public List<Dish> Dishes { get; set; }
        // Each planet has 1 chef 
        public Chef? Chef { get; set; }
        // each planet can only have 1 star chef id 
        public int? StarChefId { get; set; }
    }
}
