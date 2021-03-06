﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace OptionsBind.Controllers
{
    public class HomeController : Controller
    {
        private readonly Class MyClass;

        // IOptionsSnapshot热加载
        public HomeController(IOptionsSnapshot<Class> optionsAccesser)
        {
            MyClass = optionsAccesser.Value;
        }

        public IActionResult Index()
        {
            return View(MyClass);
        }
    }
}