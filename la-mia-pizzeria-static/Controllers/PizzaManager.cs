using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks.Dataflow;

namespace la_mia_pizzeria_static.Controllers
{
    public static class PizzaManager
    {
        public static List<Pizza> GetAllPizze()
        {
            using PizzaContext _dbContext = new PizzaContext();

            var pizza = _dbContext.Pizza?.ToList();
            return pizza;
        }
        public static List<Category> GetAllCategory()
        {
            using PizzaContext _dbContext = new PizzaContext();

            var category = _dbContext.Category?.ToList();
            return category;
        }
        public static List<Ingredient> GetAllIngredients()
        {
            using PizzaContext _dbContext = new PizzaContext();

            var ingredient = _dbContext.Ingredient?.ToList();
            return ingredient;
        }

        

        public static Pizza GetIdPizze(int id, bool includeReferences = true)
        {
            using PizzaContext _dbContext = new PizzaContext();
            if(includeReferences)
            {
                var pizzaInclude = _dbContext.Pizza?.Where(p => p.Id == id).Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault();
                return pizzaInclude;
            }
            var pizza = _dbContext.Pizza?.FirstOrDefault(p => p.Id == id);
            return pizza;
        }

        public static void InsertPizza(Pizza data, List<string> selectedIngredient = null)
        {
            using PizzaContext db = new PizzaContext();
            if(selectedIngredient != null)
            {
                data.Ingredients = new List<Ingredient>();
                foreach(var ingredientId in selectedIngredient)
                {
                    int id = int.Parse(ingredientId);
                    var ingredient = db.Ingredient.FirstOrDefault(p => p.Id == id);
                    data.Ingredients.Add(ingredient);
                }
            }
            db.Pizza?.Add(data);
            db.SaveChanges();
        }
        public static bool UpdatePizza(int id, string name, string description, float price, int? categoryId, List<string> selectedIngredients)
        {
            using PizzaContext db = new PizzaContext();
            var pizza = db.Pizza?.Where(p => p.Id == id).Include(p => p.Ingredients).FirstOrDefault();
            if (pizza == null) return false;
            pizza.Name = name;
            pizza.Description = description;
            pizza.Price = price;
            pizza.CategoryId = categoryId;
            pizza.Ingredients.Clear();
            if(selectedIngredients != null)
            {
                foreach (var ingredientId in selectedIngredients)
                {
                    if (int.TryParse(ingredientId, out int parsedIngredientId))
                    {
                        var ingredientFromDb = db.Ingredient.FirstOrDefault(p => p.Id == parsedIngredientId);
                        if (ingredientFromDb != null)
                        {
                            pizza.Ingredients.Add(ingredientFromDb);
                        }
                    }
                }
            }
            db.SaveChanges();
            return true;
        }

        public static bool DeletePizza(int id)
        {
            using PizzaContext db = new PizzaContext();
            var pizza = db.Pizza?.Find(id);
            if (pizza == null) return false;
            db.Pizza.Remove(pizza);
            db.SaveChanges();
            return true;
        }
    }
}
