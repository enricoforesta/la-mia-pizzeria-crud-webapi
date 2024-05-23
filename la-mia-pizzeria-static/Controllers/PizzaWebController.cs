using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;

namespace la_mia_pizzeria_static.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaWebController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllPizze(string? name)
        {
            if (name == null) return Ok(PizzaManager.GetAllPizze());
            return Ok(PizzaManager.GetAllPizze().Where(p => p.Name.ToLower().Contains(name.ToLower())));
        }

        [HttpGet]
        public IActionResult GetIdPizza(int id)
        {
            var pizza = PizzaManager.GetIdPizze(id);
            if (pizza == null)
                return NotFound("ERRORE");
            return Ok(pizza);
        }

        [HttpPost]
        
        public IActionResult CreatePost([FromBody] Pizza pizza)
        {
            PizzaManager.InsertPizza(pizza, null);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdatePost(int id, [FromBody] Pizza pizza)
        {
            var oldPizza = PizzaManager.GetIdPizze(id);
            if (oldPizza == null)
                return NotFound("ERRORE");
            PizzaManager.UpdatePizza(id, pizza.Name, pizza.Description,pizza.Price, pizza.CategoryId, null);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeletePost(int id)
        {
            bool found = PizzaManager.DeletePizza(id);
            if (found)
                return Ok();
            return NotFound();
        }

    }
}
