using LinkShortner.DataStore;
using LinkShortner.DataStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private ApplicationDbContext db;
        private IConfiguration configuration;

        public LinkController(ApplicationDbContext context, IConfiguration configuration)
        {
            db = context;
            this.configuration = configuration;
        }

        [HttpPost]
        public IActionResult ShortenUrl(string url, string path = "")
        {
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest("Url Is Empty");
            }

            if (!url.Contains("http"))
            {
                url = $"http://{url}";
            }

            string domain = configuration.GetSection("ShortUrl").Value;

            if (string.IsNullOrEmpty(path))
            {
                path = Guid.NewGuid().ToString().Substring(0, 6);
            }
            else
            {
                if (db.Urls.Any(u => u.Key == path))
                {
                    return BadRequest("Path exists. Insert another path.");
                }
            }

            Urls newUrl = new Urls()
            {
                Url = url,
                Key = path
            };

            db.Urls.Add(newUrl);
            db.SaveChanges();
            return Ok(domain + "/" + path);


        }



    }
}
