using meu_financeiro.API.Authorization;
using meu_financeiro.API.Entities;
using meu_financeiro.API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace meu_financeiro.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasReceitasController : ControllerBase
    {
        private ICategoriasReceitasService _categoriasReceitasService;
        private IJwtUtils _jwtUtils;

        public CategoriasReceitasController(ICategoriasReceitasService categoriasService, IJwtUtils jwtUtils)
        {
            _categoriasReceitasService = categoriasService;
            _jwtUtils = jwtUtils;
        }

        // GET: api/<CategoriasController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                var categorias = _categoriasReceitasService.GetAll((Guid)userId);
                return Ok(categorias);
            }
        }

        // GET api/<CategoriasController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                var categoria = _categoriasReceitasService.GetById(id, (Guid)userId);
                return Ok(categoria);
            }
        }


        // POST api/<CategoriasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoriasReceitas categoria)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                categoria.UserId = (Guid)userId;
                var response = await _categoriasReceitasService.Post(categoria);
                return Ok(response);
            }
        }

        // PUT api/<CategoriasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CategoriasReceitas categoria)
        {
            var response = await _categoriasReceitasService.Put(id, categoria, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);

        }

        // DELETE api/<CategoriasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _categoriasReceitasService.Delete(id, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);
        }
    }
}
