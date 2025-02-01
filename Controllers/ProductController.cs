using Microsoft.AspNetCore.Mvc;
using ReviewApiApp.DataAccessLayer;
using ReviewApiApp.Domain;
using ReviewApiApp.ViewModels;
using System.Numerics;

namespace ReviewApiApp.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductionDataStore dataset;

        public ProductController(ProductionDataStore dataset)
        {
            this.dataset = dataset;
        }

        [HttpGet()]
        public ActionResult<List<Production>> GetAllProductions()
        {
            var productions = dataset.Productions;
            if (productions == null)
                return NotFound();
            return Ok(productions);

        }

        [HttpGet("{ProductionId}")]
        public ActionResult GetProduct(int ProductionId)
        {

            var production = dataset.Productions.FirstOrDefault(p => p.Id == ProductionId);

            if (production == null)
                return NotFound();

            return Ok(production);

        }

        // get production and after it .
        [HttpGet("nextpro/{ProductionId}")]
        public ActionResult<List<Production>> GetProductAndNextProduction(int ProductionId)
        {

            var production = dataset.Productions.FirstOrDefault(p => p.Id == ProductionId);

            var nextpro = dataset.Productions.FirstOrDefault(ne => ne.Id > ProductionId);

            var result = new List<Production>() { production, nextpro };
            if (production == null)
                return NotFound();
            if (nextpro == null)
                return NotFound();
            return Ok(result);

        }


        [HttpPost]

        public ActionResult<Production> CreateProduct(CreationForProductioncs product)
        {

            int NewId = dataset.Productions.Max(p => p.Id);
            Production NewProduct = new Production()
            {
                Name = product.Name,
                Brands = product.Brands,
                Id = ++NewId
            };

            dataset.Productions.Add(NewProduct);
            return Ok(NewProduct);

        }



       

    } 
}
// Important Note: if you use where func will return [] if there is no production But
// FirstOrDefault it will return (Null) no []
