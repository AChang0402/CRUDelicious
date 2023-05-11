using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;
    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public RedirectToActionResult DefaultRedirect()
    {
        return RedirectToAction("Index");
    }

    [HttpGet("dishes")]
    public IActionResult Index()
    {
        List<Dish> AllDishes = _context.Dishes.ToList();
        return View("Index", AllDishes);
    }

    [HttpGet("dishes/{id}")]
    public IActionResult ShowDish(int id)
    {
        Dish OneDish = _context.Dishes.FirstOrDefault(dish=>dish.DishId == id);
        return View("ShowDish", OneDish);
    }

    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        return View("NewDish");
    }

    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            // return View("NewDish");
            return NewDish();
        }
    }

    [HttpGet("dishes/{id}/edit")]
    public IActionResult EditDish(int id)
    {
        Console.WriteLine(id);
        Dish? OneDish = _context.Dishes.FirstOrDefault(dish=>dish.DishId == id);
        if(OneDish != null)
        {
            return View("EditDish", OneDish);
        }
        return RedirectToAction("Index");
    }

    [HttpPost("dishes/{id}/update")]
    public IActionResult UpdateDish(Dish updatedDish, int id)
    {
        Dish? OneDish = _context.Dishes.FirstOrDefault(dish=>dish.DishId == id);
        if(ModelState.IsValid)
        {
            OneDish.Name = updatedDish.Name;
            OneDish.Chef = updatedDish.Chef;
            OneDish.Tastiness = updatedDish.Tastiness;
            OneDish.Calories = updatedDish.Calories;
            OneDish.Text = updatedDish.Text;
            OneDish.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("ShowDish", new {id=id});
        }
        else
        {
            // return View("EditDish", OneDish);
            return EditDish(id);
        }        
    }

    [HttpPost("dishes/{id}/destroy")]
    public IActionResult DestroyDish(int id)
    {
        Dish? OneDish = _context.Dishes.SingleOrDefault(dish => dish.DishId == id);
        _context.Dishes.Remove(OneDish);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
