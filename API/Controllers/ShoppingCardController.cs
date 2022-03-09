using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ShoppingCardController : BaseApiController
    {
        private readonly IShoppingCardRepository _shoppingCardRepository;
        private readonly IMapper _mapper;

        public ShoppingCardController(IShoppingCardRepository shoppingCardRepository, IMapper mapper)
        {
            _shoppingCardRepository = shoppingCardRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerShoppingCard>> GetBasketById(string id)
        {
            var basket = await _shoppingCardRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerShoppingCard(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerShoppingCard>> UpdateBasket(CustomerShoppingCardDto card)
        {
            var customerShoppingCard = _mapper.Map<CustomerShoppingCardDto, CustomerShoppingCard>(card);

            var updatedBasket = await _shoppingCardRepository.UpdateBasketAsync(customerShoppingCard);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _shoppingCardRepository.DeleteBasketAsync(id);
        }
    }
}
