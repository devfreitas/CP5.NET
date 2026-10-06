using JogosApi.Dtos;
using JogosApi.Models;
using JogosApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace JogosApi.Controllers;

[ApiController]
[Route("api/jogos")]
public class JogosController : ControllerBase
{
    private readonly IJogoService _service;

    public JogosController(IJogoService service) => _service = service;

    // POST /api/jogos
    [HttpPost]
    [ProducesResponseType(typeof(Jogo), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] JogoRequest request)
    {
        var jogo = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = jogo.Id }, jogo);
    }

    // GET /api/jogos
    [HttpGet]
    [ProducesResponseType(typeof(List<Jogo>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar() => Ok(await _service.ListarAsync());

    // GET /api/jogos/busca?plataforma=PlayStation 3&precoMaximo=100
    [HttpGet("busca")]
    [ProducesResponseType(typeof(List<Jogo>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Buscar([FromQuery] string? plataforma, [FromQuery] decimal? precoMaximo)
    {
        if (string.IsNullOrWhiteSpace(plataforma))
            return BadRequest(new { erro = "Informe a plataforma." });
        if (precoMaximo is null or < 0)
            return BadRequest(new { erro = "Informe um precoMaximo válido (>= 0)." });

        return Ok(await _service.BuscarAsync(plataforma, precoMaximo.Value));
    }

    // GET /api/jogos/relatorio-estoque
    [HttpGet("relatorio-estoque")]
    [ProducesResponseType(typeof(List<RelatorioEstoqueDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RelatorioEstoque() => Ok(await _service.RelatorioEstoqueAsync());

    // GET /api/jogos/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Jogo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(string id)
    {
        if (!ObjectId.TryParse(id, out _)) return BadRequest(new { erro = "Id inválido." });

        var jogo = await _service.ObterPorIdAsync(id);
        return jogo is null ? NotFound() : Ok(jogo);
    }

    // PUT /api/jogos/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Jogo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(string id, [FromBody] JogoRequest request)
    {
        if (!ObjectId.TryParse(id, out _)) return BadRequest(new { erro = "Id inválido." });

        var jogo = await _service.AtualizarAsync(id, request);
        return jogo is null ? NotFound() : Ok(jogo);
    }

    // DELETE /api/jogos/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remover(string id)
    {
        if (!ObjectId.TryParse(id, out _)) return BadRequest(new { erro = "Id inválido." });

        var removido = await _service.RemoverAsync(id);
        return removido ? NoContent() : NotFound();
    }
}
