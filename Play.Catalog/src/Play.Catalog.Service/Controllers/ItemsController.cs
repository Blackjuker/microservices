using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(), "Item1", "The first item", 9.99m, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Item2", "The second item", 19.99m, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Item3", "The third item", 29.99m, DateTimeOffset.UtcNow)
        };
    }
}