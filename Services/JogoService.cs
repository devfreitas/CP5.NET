using JogosApi.Dtos;
using JogosApi.Models;
using JogosApi.Repositories;

namespace JogosApi.Services;

public class JogoService : IJogoService
{
    private readonly IJogoRepository _repository;

    public JogoService(IJogoRepository repository) => _repository = repository;

    public Task<List<Jogo>> ListarAsync() => _repository.ListarAsync();

    public Task<Jogo?> ObterPorIdAsync(string id) => _repository.ObterPorIdAsync(id);

    public async Task<Jogo> CriarAsync(JogoRequest request)
    {
        var jogo = Mapear(request);
        await _repository.CriarAsync(jogo);
        return jogo;
    }

    public async Task<Jogo?> AtualizarAsync(string id, JogoRequest request)
    {
        var jogo = Mapear(request);
        var atualizado = await _repository.AtualizarAsync(id, jogo);
        return atualizado ? jogo : null;
    }

    public Task<bool> RemoverAsync(string id) => _repository.RemoverAsync(id);

    public Task<List<Jogo>> BuscarAsync(string plataforma, decimal precoMaximo) =>
        _repository.BuscarPorPlataformaEPrecoAsync(plataforma, precoMaximo);

    public Task<List<RelatorioEstoqueDto>> RelatorioEstoqueAsync() =>
        _repository.RelatorioEstoquePorPlataformaAsync();

    private static Jogo Mapear(JogoRequest r) => new()
    {
        Titulo = r.Titulo.Trim(),
        Plataforma = r.Plataforma.Trim(),
        Genero = r.Genero.Trim(),
        Preco = r.Preco,
        AnoLancamento = r.AnoLancamento,
        Estoque = r.Estoque
    };
}
