using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using testProject.Models;

namespace testProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            var contacts = _context.Contacts.ToList();
            return View(contacts);
        }

        public IActionResult CreateEditContact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEditContactForm(Contact model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Contacts.Add(model);  // Agrega el nuevo contacto al contexto
                    _context.SaveChanges();  // Guarda los cambios en la base de datos
                    TempData["Message"] = "Contact created successfully!";
                    return RedirectToAction("Contacts");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _logger.LogError(ex, "Error occurred while creating a new contact");
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View("CreateEditContact", model);  // Si hay errores, vuelve a mostrar el formulario con los datos ingresados
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
}
