using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Web_proekt.Models;
using Web_rekuperator.Controllers;
using Web_rekuperator.Models;

namespace rekuperatorProekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserContext _userContext;

        public HomeController(ILogger<HomeController> logger, UserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }

        [Authorize]
        [AllowAnonymous]
        public object Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                Users user = _userContext.Users.Include(u => u.models).Where(u => u.Email == HttpContext.User.Identity.Name).First();
                return View(user.models);
            }
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Users user = _userContext.Users.Include(u => u.models).Where(u => u.Email == HttpContext.User.Identity.Name).First();
                if(user.models.Where(i => i.id == id).First() != null)
                {
                    user.models.Remove(user.models.Where(i => i.id == id).First());
                    _userContext.models.Remove(_userContext.models.Where(u => u.id == id).First());
                    _userContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return Content("такого не сущесвуте");
            }
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Add()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        [HttpPost]
        public object Add(Model model)
        {
            if (User.Identity.IsAuthenticated)
            {
                Users user = _userContext.Users.Include(u => u.models).Where(u => u.Email == HttpContext.User.Identity.Name).First();
                user.models.Add(model);
                _userContext.models.Add(model);
                _userContext.SaveChanges();
                return RedirectToAction(nameof(Index), "Home");
            }
            return RedirectToAction(nameof(AccountController.Login), "Account");

        }


        public object ResultData(int id)
        {
            Model data = _userContext.models.Where(u => u.id == id).FirstOrDefault();

            data.Calculate();

            ResultModel result = data.Rachet();

            return View(result);
        }
        public IActionResult GraResultData()
        {
            ViewBag.x = Model.ArrayX;
            ViewBag.y = Model.ArrayY;

            return View();
        }

        public IActionResult OutputData()
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
