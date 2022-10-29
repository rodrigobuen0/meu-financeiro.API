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
    public class ContasController : ControllerBase
    {
        private IContasService _contasService;
        private IJwtUtils _jwtUtils;

        public ContasController(IContasService contasService, IJwtUtils jwtUtils)
        {
            _contasService = contasService;
            _jwtUtils = jwtUtils;
        }

        // GET: api/<ContasController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                var contas = _contasService.GetAll((Guid)userId);
                return Ok(contas);
            }
        }

        // GET api/<ContasController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var conta = _contasService.GetById(id, Guid.Parse(Request.Headers["UserId"]));
            return Ok(conta);
        }


        // POST api/<ContasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contas conta)
        {
            var response = await _contasService.Post(conta, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);
        }

        // PUT api/<ContasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Contas conta)
        {
            var response = await _contasService.Put(id, conta, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);

        }

        // DELETE api/<ContasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _contasService.Delete(id, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);
        }
    }
}
