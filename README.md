# My vehicle health

Bem-vindo ao repositório do **My vehicle health**, uma aplicação Web API desenvolvida com .NET 8 para organizar e acompanhar manutenções veiculares. Este projeto foi criado para registrar informações detalhadas sobre veículos, oficinas mecânicas, manutenções e serviços realizados, oferecendo relatórios e alertas para melhor gestão.

---

## Funcionalidades

- Cadastro de Veículos:

    - Registro básico de veículos com nome.

- Cadastro de Oficinas Mecânicas:

    - Nome da empresa.
    - Nome do mecânico responsável.
    - Telefone de contato.

- Registro de Manutenções:

    - Relacionadas a um veículo e uma oficina.
    - Registro de custos totais calculados automaticamente.

- Registro de Serviços:

    - Detalhamento de serviços realizados em cada manutenção.
    - Campos opcionais para próxima troca (data e quilometragem).
    - Registro de custos de peças e mão de obra.

- Relatórios e Alertas:

    - Listagem de manutenções próximas e vencidas.
    - Filtro por serviços, custos e oficinas.

---

## Tecnologias Utilizadas

- **Linguagem**: C# (.NET 8)
- **Framework**: ASP.NET Web API
- **Banco de Dados**: SQL Server (com EF Core para mapeamento)
- **Testes**: xUnit
- **Documentação**: Swagger/OpenAPI
- **IDE**: JetBrains Rider

---

## Tarefas Futuras
- Implementação de autenticação e autorização
- Implementação de notificações por e-mail para alertas
- Implementação de interface de usuário com Blazor (Ou algum outro framework frontend)

---