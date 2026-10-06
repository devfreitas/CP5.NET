using JogosApi.Dtos;
using JogosApi.Models;

namespace JogosApi.Services;

public interface IJogoService
{
    Task<List<Jogo>> ListarAsync();
    Task<Jogo?> ObterPorIdAsync(string id);
    Task<Jogo> CriarAsync(JogoRequest request);
    Task<Jogo?> AtualizarAsync(string id, JogoRequest request);
    Task<bool> RemoverAsync(string id);
    Task<List<Jogo>> BuscarAsync(string plataforma, decimal precoMaximo);
    Task<List<RelatorioEstoqueDto>> RelatorioEstoqueAsync();
}
