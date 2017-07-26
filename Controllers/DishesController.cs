using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using confusionresturant.Entities;
using confusionresturant.Models;
using confusionresturant.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace confusionresturant.Controllers
{
    [Route("api/[controller]")]
    public class DishesController : Controller
    {
        private readonly ICrRepository _repo;
        private readonly IMapper _mapper;
     

        public DishesController(ICrRepository repo, IMapper mapper )
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet("")]
        [Authorize]
        public IActionResult Get()
        {
            var dishes = _repo.GetDishes();
            return Ok(Mapper.Map<IEnumerable<DishModel>>(dishes));
        }

        [HttpGet("{id:int}", Name = "DishGet")]
        public IActionResult Get(int id)
        {
            try
            {
                var dish = _repo.GetDishWithComments(id);
                if (dish == null) return NotFound($"Dish of {id} was not found");
                return Ok(dish);
            }
            catch (Exception)
            { }
            return BadRequest("Could not found Dish");
        }
        [HttpGet("{category}")]
        public IActionResult GetCategory(string category)
        {
            try
            {
                var dish = _repo.GetDishByCategory(category);
                return Ok(Mapper.Map<IEnumerable<DishModel>>(dish));
            }
            catch (Exception)
            {
            }
            return BadRequest("Couldn't found dish");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Dish model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                _repo.Add(model);
                if (await _repo.SaveAllAsync())
                {
                    var newUri = Url.Link("DishGet", new { id = model.DishId });
                    return Created(newUri, model);
                }
            }
            catch (Exception )
            {
            }
            return BadRequest("Could not post Dish");
        }
        [HttpPut("{id}")]
         [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] DishModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var oldDish = _repo.GetDish(id);
                if (oldDish == null) return NotFound($"Couldn't find a dish of {id}");
                _mapper.Map(model, oldDish);

                if (await _repo.SaveAllAsync())
                {
                    return Ok(_mapper.Map<DishModel>(oldDish));
                }
            }
            catch (Exception)
            {
            }
            return BadRequest("Could not update dish");
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldDish = _repo.GetDish(id);
                if (oldDish == null) return NotFound($"Could not found Dish of id {id}");
                _repo.Delete(oldDish);
                if (await _repo.SaveAllAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
            }
            return BadRequest("Could not Delete Dish");
        }
    }
}


