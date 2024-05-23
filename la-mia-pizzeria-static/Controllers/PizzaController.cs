using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace la_mia_pizzeria_static.Controllers
{
    [Authorize]
    public class PizzaController : Controller
    {
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public IActionResult Index()
        {

            return View(PizzaManager.GetAllPizze());
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var Pizza = PizzaManager.GetIdPizze(id,true);
            if (Pizza == null) return View("errore");
            return View(Pizza);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            PizzaFormModel model = new PizzaFormModel(new Pizza(), PizzaManager.GetAllCategory(), PizzaFormModel.CreateIngredients());
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.Categories = PizzaManager.GetAllCategory();
                data.Ingredients = PizzaFormModel.CreateIngredients();
                return View("Create", data);
            }

            PizzaManager.InsertPizza(data.Pizza, data.SelectedIngredients);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var pizzaEdit = PizzaManager.GetIdPizze(id);
            if (pizzaEdit == null) return NotFound();
            PizzaFormModel model = new PizzaFormModel(pizzaEdit, PizzaManager.GetAllCategory(), PizzaFormModel.CreateIngredients());

            return View(model);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id,PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.Categories = PizzaManager.GetAllCategory();
                data.Ingredients = PizzaFormModel.CreateIngredients();
                return View("Update", data);
            }
            if(!PizzaManager.UpdatePizza(id, data.Pizza?.Name, data.Pizza?.Description, data.Pizza.Price, data.Pizza?.CategoryId, data.SelectedIngredients)) return NotFound();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (!PizzaManager.DeletePizza(id)) return NotFound();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
