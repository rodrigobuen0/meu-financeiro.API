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
    public class TransferenciasController : ControllerBase
    {
        private ITransferenciasService _transferenciasService;
        private IJwtUtils _jwtUtils;

        public TransferenciasController(ITransferenciasService transferenciasService, IJwtUtils jwtUtils)
        {
            _transferenciasService = transferenciasService;
            _jwtUtils = jwtUtils;
        }

        // GET: api/<TransferenciasController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                var transferencias = _transferenciasService.GetAll((Guid)userId);
                return Ok(transferencias);
            }
        }

        [HttpGet("TransferenciasMes")]
        public IActionResult GetAllMes()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                var transferencias = _transferenciasService.GetAllMes((Guid)userId);
                return Ok(transferencias);
            }
        }

        // GET api/<TransferenciasController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var transferencia = _transferenciasService.GetById(id, Guid.Parse(Request.Headers["UserId"]));
            return Ok(transferencia);
        }


        // POST api/<TransferenciasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Transferencias transferencia)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                transferencia.UserId = (Guid)userId;
                var response = await _transferenciasService.Post(transferencia);
                return Ok(response);
            }
        }

        // PUT api/<TransferenciasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Transferencias transferencia)
        {
            var response = await _transferenciasService.Put(id, transferencia, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);

        }

        // DELETE api/<TransferenciasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _transferenciasService.Delete(id, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);
        }
    }
}
