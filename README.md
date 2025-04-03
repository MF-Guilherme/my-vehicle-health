# My Vehicle Health 🚗💨

![.NET Version](https://img.shields.io/badge/.NET-8.0-blue)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

Bem-vindo ao **My Vehicle Health**, uma solução completa para gestão de manutenções veiculares desenvolvida com arquitetura moderna e boas práticas de desenvolvimento.

## ✨ Funcionalidades

### 🚘 Gestão de Veículos
- Cadastro completo de veículos
- Histórico de manutenções por veículo

### 🔧 Gestão de Manutenções
- **Sistema CQRS** para separação clara de responsabilidades
- Registro detalhado de serviços e peças trocadas
- Cálculo automático de custos totais (peças + mão de obra)
- Alertas para próximas manutenções (data e quilometragem)

### 📊 Relatórios Inteligentes
- Filtros avançados por período, custos e oficinas
- Visualização de histórico de manutenções
- Dashboard de custos totais

## 🛠️ Tecnologias e Arquitetura

### 📦 Stack Técnica
- **Backend**: .NET 8 + C#
- **Banco de Dados**: SQL Server + Entity Framework Core
- **Padrão Arquitetural**: CQRS + Clean Architecture
- **Bibliotecas**:
  - MediatR para padrão CQRS
  - FluentValidation para validações robustas
  - AutoMapper para mapeamento de DTOs
- **Documentação**: Swagger/OpenAPI

### 🏗️ Estrutura do Projeto
```
MyVehicleHealth/
├── API/              # Camada de apresentação
├── Application/      # Regras de negócio
│   ├── Commands/     # Comandos de escrita
│   ├── Queries/      # Consultas de leitura
│   └── Dtos/         # Objetos de transferência
├── Domain/           # Entidades e contratos
└── Infrastructure/   # Acesso a dados e serviços externos
```

## 🚀 Como Executar

1. **Pré-requisitos**:
   - .NET 8 SDK
   - SQL Server
   
2. **Configuração**:
```bash
git clone https://github.com/seu-usuario/my-vehicle-health.git
cd my-vehicle-health
dotnet restore
```

3. **Execução**:
```bash
dotnet run --project src/MyVehicleHealth.API
```

Acesse a documentação da API em: `https://localhost:5001/swagger`

## 📅 Roadmap

### ⏳ Em Desenvolvimento
- [ ] Sistema de autenticação com JWT
- [ ] Notificações por e-mail
- [ ] Testes unitários abrangentes

### 🔜 Próximas Features
- [ ] Frontend em Angular/Blazor
- [ ] Relatórios PDF personalizados
- [ ] Integração com APIs de concessionárias

## 🤝 Como Contribuir
Contribuições são bem-vindas! Siga os passos:
1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/awesome-feature`)
3. Commit suas mudanças (`git commit -m 'Add awesome feature'`)
4. Push para a branch (`git push origin feature/awesome-feature`)
5. Abra um Pull Request

---

**Desenvolvido por Guilherme Montenegro**  
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Connect-blue)](https://www.linkedin.com/in/MF-Guilherme)
