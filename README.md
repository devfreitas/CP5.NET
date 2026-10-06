# Catálogo de Jogos — .NET 8 + MongoDB

## Como executar
1. Suba o MongoDB:
   `docker run -d -p 27017:27017 --name mongo-local mongo`
   (ou `docker compose up -d`)
2. Rode a API: `dotnet run`
3. Abra o Swagger: `http://localhost:<porta>/swagger` (a porta aparece no console).

A conexão é configurada em `appsettings.json` (seção `MongoDbSettings`).

## Estrutura
- `Controllers/JogosController` – endpoints e status HTTP
- `Services/JogoService` – regras e mapeamento DTO → entidade
- `Repositories/JogoRepository` – acesso ao Mongo (CRUD, `Builders<Jogo>.Filter`, `Aggregate`)
- `Models/Jogo` – entidade com `[BsonId]`, `[BsonRepresentation]`, `[BsonElement]`
- `Settings/MongoDbSettings` – Options Pattern

## Endpoints
| Verbo | Rota | Retornos |
|---|---|---|
| POST | /api/jogos | 201, 400 |
| GET | /api/jogos | 200 |
| GET | /api/jogos/{id} | 200, 400, 404 |
| PUT | /api/jogos/{id} | 200, 400, 404 |
| DELETE | /api/jogos/{id} | 204, 400, 404 |
| GET | /api/jogos/busca?plataforma=&precoMaximo= | 200, 400 |
| GET | /api/jogos/relatorio-estoque | 200 |
