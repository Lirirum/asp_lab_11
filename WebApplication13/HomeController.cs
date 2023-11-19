
using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication13.Models;

namespace WebApplication13
{
 
 
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View("./Views/Registration.cshtml");

        }

        [HttpPost]
        public IActionResult Create(RegistrationModel  registration)

        {   
            


            if (registration.Date.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError("Date","Дата некоректна");
                
            }

            if (registration.Date.DayOfWeek == DayOfWeek.Saturday || registration.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                ModelState.AddModelError("Date", "Консультація не може бути у вихідні дні");
            }


            if (registration.Date.DayOfWeek == DayOfWeek.Monday && registration.Product== "Основи")
            {

                ModelState.AddModelError("Date", "Консультація по основам не проходять по понеділкам");
            }


            if (ModelState.IsValid)
                return RedirectToAction("Sucsess");

            return View("./Views/Registration.cshtml",registration);

        }

        public IActionResult Sucsess()
        {
            return View("./Views/Sucsess.cshtml");
        }


    }
}
