using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.API.Controllers.Repository;
using Bar.Database.Entities;
using Bar.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = scheme)]
    public class ItemController : BaseCrudController<Item, Item, Item, Item>
    {

        private const string scheme = "BasicAuthentication";
        public ItemController(IBaseCrudService<Item, Item, Item, Item> service) : base(service)
        {
        }
    }
}