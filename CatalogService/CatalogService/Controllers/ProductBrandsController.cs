using CatalogService.Errors;
using Core.Entities;
using Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Controllers
{
 
    public class ProductBrandsController : BaseApiController
    {
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;

        public ProductBrandsController(IGenericRepository<ProductBrand> productBrandRepository)
        {
            _productBrandRepository = productBrandRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<Product>>> GetProductBrands()
        {
            return Ok(await _productBrandRepository.GetAllAsync());
        }
        [HttpGet("{productBrandID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<Product>>> GetProductBrandByID(int productBrandID)
        {
            return Ok(await _productBrandRepository.GetByIDAsync(productBrandID));
        }
    }
}
