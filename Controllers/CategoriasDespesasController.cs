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
    public class CategoriasDespesasController : ControllerBase
    {
        private ICategoriasDespesasService _categoriasDespesasService;
        private IJwtUtils _jwtUtils;

        public CategoriasDespesasController(ICategoriasDespesasService categoriasService, IJwtUtils jwtUtils)
        {
            _categoriasDespesasService = categoriasService;
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
                var categorias = _categoriasDespesasService.GetAll((Guid)userId);
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
                var categoria = _categoriasDespesasService.GetById(id, (Guid)userId);
                return Ok(categoria);
            }
        }


        // POST api/<CategoriasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoriasDespesas categoria)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                categoria.UserId = (Guid)userId;
                var response = await _categoriasDespesasService.Post(categoria);
                return Ok(response);
            }
        }

        // PUT api/<CategoriasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CategoriasDespesas categoria)
        {
            var response = await _categoriasDespesasService.Put(id, categoria, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);
        }

        // DELETE api/<CategoriasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _categoriasDespesasService.Delete(id, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);
        }
    }
}
