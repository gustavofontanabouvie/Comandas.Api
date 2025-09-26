# API de Gerenciamento de Comandas

API RESTful desenvolvida para otimizar o sistema de comandas digitais de um restaurante, gerenciando pedidos, mesas e produtos.

Este projeto foi construído como parte dos meus estudos em desenvolvimento back-end com .NET, aplicando os principais conceitos de arquitetura de software.

## ✨ Principais Funcionalidades

-   Cadastro de produtos
-   Gerenciamento de mesas
-   Abertura e fechamento de comandas
-   Adição e remoção de itens em uma comanda

## 🚀 Tecnologias Utilizadas

-   **Linguagem:** C#
-   **Framework:** .NET 8 / ASP.NET Core
-   **Arquitetura:** Clean Architecture
-   **Padrões de Projeto:** Domain-Driven Design (DDD)
-   **ORM:** Entity Framework Core
-   **Banco de Dados:** Microsoft SQL Server
-   **Documentação da API:** Swagger

## 📖 Como Executar o Projeto

```bash
# Clone o repositório
git clone [https://github.com/gustavofontanabouvie/Comandas.Api.git](https://github.com/gustavofontanabouvie/Comandas.Api.git)

# Navegue até a pasta da API
cd Comandas.Api

# Execute o projeto
dotnet watch run
```

## Endpoints da API

Após executar o projeto, a documentação Swagger estará disponível em `https://localhost:<sua_porta>/swagger`.

Lá você poderá ver e testar todos os endpoints disponíveis, como:
- `GET/api/CardapioItens/{id}`
- `POST/api/Comandas`
- `GET/api/Comandas/{id}`

---
