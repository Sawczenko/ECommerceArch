using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CatalogService.Helpers;
using Core.Specifications;
using Infrastructure.Data.Repositories.Interfaces;
using CatalogService.Errors;
using AutoMapper;
using CatalogService.Dtos;

namespace CatalogService.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductsSpecificationParameters productsSpecificationParameters)
        {
            var specification = new ProductWithTypesAndBrandsSpecification(productsSpecificationParameters);

            var countSpecification = new ProductWithFilterForCountSpecification(productsSpecificationParameters);

            var totalItems = await _productRepository.CountAsync(countSpecification);
            var products = await _productRepository.ListAsync(specification);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productsSpecificationParameters.PageIndex, productsSpecificationParameters.PageSize, totalItems, data));
        }

        [HttpGet("{productID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductByID(int productID)
        {
            var specification = new ProductWithTypesAndBrandsSpecification(productID);
            var product = await _productRepository.GetEntityWithSpecification(specification);

            if (product is null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }
    }
}
