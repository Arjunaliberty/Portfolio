using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    /// <summary>
    /// Контроллер по обработке ошибок
    /// </summary>
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(Error error)
        {
            return View(error);
        }
    }
}