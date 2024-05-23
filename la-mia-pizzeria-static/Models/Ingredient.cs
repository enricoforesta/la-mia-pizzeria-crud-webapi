using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è necessario")]
        [StringLength(40, ErrorMessage = "Il nome deve avere max 40 caratteri")]
        public string? Name { get; set; }

        public List<Pizza>? Pizzas { get; set; }
        
        public Ingredient() { }

        public Ingredient(int id, string name, List<Pizza> pizzas) 
        {
            this.Id = id;
            this.Name = name;
            this.Pizzas = pizzas;
        }

    }
}
