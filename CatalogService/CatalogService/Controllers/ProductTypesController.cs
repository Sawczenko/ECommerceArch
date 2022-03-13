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
    public class ProductTypesController : BaseApiController
    {
        private readonly IGenericRepository<ProductType> _productTypeRepository;

        public ProductTypesController(IGenericRepository<ProductType> productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepository.GetAllAsync());
        }
        [HttpGet("{productTypeID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductType>> GetProductTypeByID(int productTypeID)
        {
            return Ok(await _productTypeRepository.GetByIDAsync(productTypeID));
        }
    }
}
