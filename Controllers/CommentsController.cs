using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using confusionresturant.Entities;
using confusionresturant.Models;
using confusionresturant.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace confusionresturant.Controllers
{
    [Route("api/dishes/{id}/comments")]
    public class CommentController : Controller
    {
        private ICrRepository _repo;
        private IMapper _mapper;
        private ILogger<CommentController> _logger;
        private UserManager<CrUser> _userMgr;

        public CommentController(ICrRepository repo,
                                IMapper mapper,
                                ILogger<CommentController> logger,
                                UserManager<CrUser> userMgr)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
            _userMgr = userMgr;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(int id, [FromBody] CommentModel model)
        {
            try
            {
                var dish = _repo.GetDish(id);
                var comment = _mapper.Map<Comment>(model);
                comment.Dish = dish;
                var crUser = await _userMgr.FindByNameAsync(this.User.Identity.Name);
                if (crUser != null)
                {
                    comment.User = crUser;
                    _repo.Add(comment);
                    if (await _repo.SaveAllAsync())
                    {
                        var url = Url.Link("CommentGet", new { id = model.CommentId });
                        return Created(url, _mapper.Map<CommentModel>(comment));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"an error accured while posting comment {ex}");
            }
            return BadRequest("Could not post comment");
        }
    }
}