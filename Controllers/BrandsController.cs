using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReviewApiApp.DataAccessLayer;
using ReviewApiApp.Domain;
using ReviewApiApp.ViewModels;

namespace ReviewApiApp.Controllers
{
    [ApiController]

     [Route("api/products/{productId}/[controller]")]

    public class BrandsController : ControllerBase
    {

        [HttpGet()]
        public ActionResult<List<Brand>> GetBrands(int productId)
        {
            var production = ProductionDataStore.Current.Productions.FirstOrDefault( p => p.Id == productId);
            if (production == null)
                return NotFound();
            return Ok(production.Brands);
        }
        
        
        [HttpGet("{brandId}",Name ="GetBrand")]
        public ActionResult <Brand> GetBrandById(int productId, int brandId)
        {
            var production = ProductionDataStore.Current.Productions.FirstOrDefault( p => p.Id == productId);
            if (production == null)
                return NotFound("production is not available");

           var brand = production.Brands.FirstOrDefault(b => b.Id == brandId);
            if (brand == null)
                return NotFound("The Odered Brand is not here, please write a true id");
            return Ok(brand);
        }




        [HttpPost]
        public ActionResult<List<Brand>> CreateBrand(int productId, BrandForCreation brand)
        {
            var product = ProductionDataStore.Current.Productions.FirstOrDefault(p => p.Id == productId);
            if (product == null) return NotFound();


            int MaxId = ProductionDataStore.Current.Productions.SelectMany(p => p.Brands).Max(i => i.Id);
            var NewProductBrands = new Brand()
            {
                Name = brand.Name,
                Id = ++MaxId
            };

            product.Brands.Add(NewProductBrands);
            return CreatedAtRoute("GetBrand",new { productId = productId, brandId= NewProductBrands.Id},NewProductBrands);
        }


        [HttpPut("{BrandId}")] // Update all object// all fields
         public ActionResult UpdateBrand(int ProductId , int BrandId, BrandForUpdate brand)
        {
            var product = ProductionDataStore.Current.Productions.FirstOrDefault(i => i.Id == ProductId);
            if (product == null) return NotFound();

            var existingbrand = product.Brands.FirstOrDefault( b => b.Id == BrandId);
            if (existingbrand == null) return NotFound();

            // must using here Auto Mapper concept instead of using assignment by this way.
            existingbrand.Name = brand.Name;
            existingbrand.Description = brand.Description;

            return NoContent();
        }

     

        [HttpPatch("{BrandId}")] // Updating some of fields Not All Fields.
        public ActionResult ParitiallyUpdate(int productId, int BrandId, JsonPatchDocument<BrandForUpdate> patchdocument)
        {
            var existinigproduct = ProductionDataStore.Current.Productions.FirstOrDefault( i => i.Id == productId);
            if( existinigproduct == null) return NotFound();

            var existingbrand = existinigproduct.Brands.FirstOrDefault(b => b.Id == BrandId);
            if (existingbrand == null) return NotFound();

            // old values of object
            var BrandToPatch = new BrandForUpdate()
            {
                Name = existingbrand.Name,
                Description = existingbrand.Description,
            };

            patchdocument.ApplyTo(BrandToPatch, ModelState);
            if (!ModelState.IsValid)
                return BadRequest();// 404

            existingbrand.Name = BrandToPatch.Name;
            existingbrand.Description = BrandToPatch.Description;


            return NoContent();
            
        }




        [HttpDelete("{brandId}")]
        public ActionResult DeletBrand(int productId, int brandId)
        {
            var product = ProductionDataStore.Current.Productions.FirstOrDefault(i => i.Id == productId);
            if (product == null) return NotFound();

            var brand = product.Brands.FirstOrDefault(b => b.Id == brandId);
            if (brand == null) return NotFound();

            product.Brands.Remove(brand);
            return NoContent();
        }
    }
}

