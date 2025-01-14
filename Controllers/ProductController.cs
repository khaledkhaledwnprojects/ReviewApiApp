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


        [HttpGet()]
        public ActionResult<List<Production>> GetAllProductions()
        {
            var productions = ProductionDataStore.Current.Productions;
            if (productions == null)
                return NotFound();
            return Ok(productions);

        }

        [HttpGet("{ProductionId}")]
        public ActionResult GetProduct(int ProductionId)
        {

            var production = ProductionDataStore.Current.Productions.FirstOrDefault(p => p.Id == ProductionId);

            if (production == null)
                return NotFound();

            return Ok(production);

        }

        // get production and after it .
        [HttpGet("nextpro/{ProductionId}")]
        public ActionResult<List<Production>> GetProductAndNextProduction(int ProductionId)
        {

            var production = ProductionDataStore.Current.Productions.FirstOrDefault(p => p.Id == ProductionId);

            var nextpro = ProductionDataStore.Current.Productions.FirstOrDefault(ne => ne.Id > ProductionId);

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

            int NewId = ProductionDataStore.Current.Productions.Max(p => p.Id);
            Production NewProduct = new Production()
            {
                Name = product.Name,
                Brands = product.Brands,
                Id = ++NewId
            };

            ProductionDataStore.Current.Productions.Add(NewProduct);
            return Ok(NewProduct);

        }



       

    } 
}
// Important Note: if you use where func will return [] if there is no production But
// FirstOrDefault it will return (Null) no []
