﻿using Microsoft.AspNetCore.Mvc;

namespace E_CommerceASP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
