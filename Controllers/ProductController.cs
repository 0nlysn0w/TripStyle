using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TripStyle.Models;

namespace TripStyle.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly TripStyleContext _context;

        public ProductController(TripStyleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _context.Products.ToList();
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            Product product = _context.Products.Find(id);
            if  (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public IActionResult Create([FromBody]Product product)
        {
            if (product == null)
            {
                return NoContent();
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtRoute("GetProduct", new { id = product.ProductId}, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Product product)
        {
            var todo = _context.Products.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Name = product.Name;
            todo.Make = product.Make;
            todo.Price = product.Price;
            todo.Stock = product.Stock;
            todo.Size = product.Size;
            todo.Color = product.Color;
            todo.Region = product.Region;
            todo.Season = product.Season;
            todo.Name = product.Name;

            _context.Products.Update(todo);
            _context.SaveChanges();
            return CreatedAtRoute("GetProduct", new { id = todo.ProductId }, todo);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.Products.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Products.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
        // [HttpGet("{gender}/{color}")]
        // public IEnumerable<Product> Get(string Region, string color)
        // {

        //     return _context.Products.Where(product => product.Region == Region && product.Color == color).ToList();
        // }
        [HttpGet("{color}")]
        public IEnumerable<Product> Get(string color)
        {

            return _context.Products.Where(product => product.Color == color).ToList();
        
        }
        [HttpGet("search/{searchterm=string}")]
        public IEnumerable<Product> Getsearch(string searchterm)
        {
            
            
            var result = _context.Products.Where(p=>p.Name == searchterm).ToList();
             
            /* if (searchString != null)
           // {
           // if (searchString.Id.HasValue)
                //result = result.Where(x => x.Id == searchString.Id);
            if (!string.IsNullOrEmpty(searchString.Name))
                result = result.Where(x => x.Name.Contains(searchString.Name));
            /* if (searchString.PriceFrom.HasValue)
                result = result.Where(x => x.Price >= searchString.PriceFrom);
            if (searchString.PriceTo.HasValue)
                result = result.Where(x => x.Price <= searchString.PriceTo);*/
        //}
            return result;     
 
        /*  public async Task<IActionResult> Index(string SelectedName, string searchString)
            {
            // Use LINQ to get list of genres.
            IQueryable<string> nameQuery = from m in _context.Products
                                        
                                            select m.Name;

            var products = from m in _context.Products
                            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
               products  = products.Where(s => s.Name.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(SelectedName))
            {
                products = products.Where(x => x.Name == SelectedName);
            }

            var productVM = new Product();
            productVM.Name = new SelectList(await nameQuery.Distinct().ToListAsync());
            productVM.Category = await products.ToListAsync();
            productVM.Name= searchString;

            return View(productVM);
            
        
            }*/
        }    

    }
 }