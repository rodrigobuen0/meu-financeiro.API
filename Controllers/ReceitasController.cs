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
    public class ReceitasController : ControllerBase
    {
        private IReceitasService _receitasService;
        private IJwtUtils _jwtUtils;

        public ReceitasController(IReceitasService receitasService, IJwtUtils jwtUtils)
        {
            _receitasService = receitasService;
            _jwtUtils = jwtUtils;
        }

        // GET: api/<ReceitasController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                var receitas = _receitasService.GetAll((Guid)userId);
                return Ok(receitas);
            }
        }

        // GET: api/<ReceitasController>
        [HttpGet("ValorReceitasMes")]
        public IActionResult GetAllMes()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                var receitas = _receitasService.GetAllMes((Guid)userId);
                return Ok(receitas);
            }
        }

        // GET api/<ReceitasController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var receita = _receitasService.GetById(id, Guid.Parse(Request.Headers["UserId"]));
            return Ok(receita);
        }


        // POST api/<ReceitasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Receitas receita)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                receita.UserId = (Guid)userId;
                var response = await _receitasService.Post(receita);
                return Ok(response);
            }
        }

        // PUT api/<ReceitasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Receitas receita)
        {
            var response = await _receitasService.Put(id, receita, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);

        }

        // DELETE api/<ReceitasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _receitasService.Delete(id, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);
        }
    }
}
