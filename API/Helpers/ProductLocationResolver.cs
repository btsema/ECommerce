using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class ProductLocationResolver : IValueResolver<Product, ProductDetailsDto, string>
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductLocationResolver(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(Product source, ProductDetailsDto destination, string destMember, ResolutionContext context)
        {
            var currentSchema = _httpContextAccessor.HttpContext.Request.Scheme;
            var currentUrl = _httpContextAccessor.HttpContext.Request.Host.Value;

            if (!string.IsNullOrEmpty(source.ImageDestination))
            {
                return currentSchema + "://" + currentUrl + _config["ImageUrl"] + source.ImageDestination;
            }

            return null;
        }
    }
}
