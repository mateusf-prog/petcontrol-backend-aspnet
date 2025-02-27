﻿using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/products")]
    public class ProductsController : MainController
    {
        public ProductsController() { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(ProductDto productDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductDto>> Update(Guid id, ProductDto productDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProductDto>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
