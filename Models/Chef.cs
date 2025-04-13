namespace Assignment1_v2.Models
{
    public class Chef
    {
        // Setting table relationship for Chef
        // Each chef will have their starChefId along with name and ExperienceLevel
        public int StarChefId { get; set; }
        public string Name { get; set; }
        public string ExperienceLevel { get; set; }

        // Each chef belongs to 1 planet 
        public Planet Planet { get; set; }
        // Each Chef can have many dishes 
        public List<Dish> Dishes { get; set; }
    }
}
