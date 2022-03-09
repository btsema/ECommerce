using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrandInfo> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrandInfo> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _productsRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }

        [HttpGet("all-products")]
        public async Task<ActionResult<Pagination<ProductDetailsDto>>> GetAllProducts(
            [FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totolItems = await _productsRepo.CountAsync(countSpec);

            var products = await _productsRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDetailsDto>>(products);

            return Ok(new Pagination<ProductDetailsDto>(productParams.PageIndex, productParams.PageSize,
                totolItems, data));
        }


        [HttpGet("{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiGlobalResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDetailsDto>> GetProductByProductId(int product_id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(product_id);

            var product = await _productsRepo.GetEntityWithSpec(spec);

            if (product == null)
            {
                return NotFound(new ApiGlobalResponse(404));
            }

            return _mapper.Map<Product, ProductDetailsDto>(product);
        }


        [HttpGet("product-brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrandInfo>>> GetAllProductBrands()
        {
            var brands = await _productBrandRepo.ListAllAsync();

            return Ok(brands);
        }


        [HttpGet("product-types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAvailableProductTypes()
        {
            var types = await _productTypeRepo.ListAllAsync();

            return Ok(types);
        }
    }
}
