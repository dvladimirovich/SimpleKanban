using Microsoft.AspNetCore.Mvc;
using SimpleKanban.DB.Abstract;
using SimpleKanban.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleKanban.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private IRepository<Card> context;

        public HomeController(IRepository<Card> context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Card>> Get()
        {
            return await context.GetAsync();
        }

        // GET: /Home/Cards/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = await context.GetAsync();
            return View(query.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Card card)
        {
            card.Start = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Hour, DateTime.Today.Minute, 0);
            await context.CreateAsync(card);
            return Ok(card);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody]Card card)
        {
            Card entity = await context.FindByIdAsync(card.Id);
            if (entity == null)
            {
                return NotFound();
            }
            await context.UpdateAsync(card);
            return Ok(card);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Card card = await context.FindByIdAsync(Convert.ToInt32(id));
            if (card == null)
            {
                return NotFound();
            }
            await context.RemoveAsync(card);
            return Ok(card);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
