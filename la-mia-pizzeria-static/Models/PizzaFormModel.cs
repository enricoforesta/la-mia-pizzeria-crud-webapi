using la_mia_pizzeria_static.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaFormModel
    {
        public Pizza? Pizza { get; set; }
        public List<Category>? Categories { get; set; }

        public List<SelectListItem>? Ingredients { get; set; }
        public List<string>? SelectedIngredients { get; set; }

        public IFormFile? ImageFormFile { get; set; }
        public PizzaFormModel() 
        {
           
        }

        public PizzaFormModel(Pizza pizza, List<Category> category, List<SelectListItem> selectedIngredients) 
        {
            this.Pizza = pizza;
            this.Categories = category;
            this.Ingredients = selectedIngredients;
            this.SelectedIngredients = pizza.Ingredients?.Select(i => i.Id.ToString()).ToList() ?? new List<string>();
        }

        // Ho spostato la funzione qua perchè in PizzaManager sono funzioni collegate al DB.
        public static List<SelectListItem> CreateIngredients()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var ingredientFromDb = PizzaManager.GetAllIngredients();
            foreach (Ingredient ingredient in ingredientFromDb)
            {
                list.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString()));
            }
            return list;
        }
        public byte[] SetImage()
        {
            if (ImageFormFile == null)
                return null;

            using var stream = new MemoryStream();
            this.ImageFormFile?.CopyTo(stream);
            Pizza.ImageFile = stream.ToArray();

            return Pizza.ImageFile;
        }
    }
}
