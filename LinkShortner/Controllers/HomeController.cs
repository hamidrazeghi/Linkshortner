using LinkShortner.DataStore;
using LinkShortner.DataStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortner.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }
   

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("url is not valid");
            }

            Urls url = db.Urls.FirstOrDefault(c => c.Key == id);
            if (url == null)
            {
                return BadRequest("Url is not valid Or Expired");
            }

            return Redirect(url.Url);

        }

    }
}
