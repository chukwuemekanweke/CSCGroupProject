﻿using CSCGroupProject.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSCGroupProject.Controllers
{
    public class NavController : Controller
    {
        private ShoppingCartDBContext db = new ShoppingCartDBContext();
                
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = db.Products
                                            .Select(x => x.Category)
                                            .Distinct()
                                            .OrderBy(x => x);
            return PartialView(categories);
        }
	}
}