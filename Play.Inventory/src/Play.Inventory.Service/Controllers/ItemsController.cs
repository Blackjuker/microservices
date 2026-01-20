using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Common;
using Play.Inventory.Service.Dtos;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<InventoryItem> itemRepository;

        public ItemsController(IRepository<InventoryItem> itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(Guid userId)
        {
            if( userId == Guid.Empty)
            {
                return BadRequest();
            }

            var items = (await itemRepository.GetAllAsync(item => item.UserId == userId))
                            .Select(item => item.AsDto());
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(GrantedItemsDto grantedItemsDto)
        {
            var inventoryItem = await itemRepository.GetAsync(item => item.UserId == grantedItemsDto.UserId && item.CatalogItemId == grantedItemsDto.CatalogItemId);

            if(inventoryItem == null)
            {
                inventoryItem = new InventoryItem
                {
                    CatalogItemId = grantedItemsDto.CatalogItemId,
                    UserId = grantedItemsDto.UserId,
                    Quantity = grantedItemsDto.Quantity,
                    AcquiredDate = DateTimeOffset.UtcNow
                };

                await itemRepository.CreateAsync(inventoryItem);
            }
            else
            {
                inventoryItem.Quantity += grantedItemsDto.Quantity;
                await itemRepository.UpdateAsync(inventoryItem);
            }

            return Ok();
        }
    }
}