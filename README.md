docker-compose up -d
dotnet ef database update
dotnet run
# ApexInvest - Sistema de Gestão de Investimentos

O ApexInvest é uma plataforma de automação de investimentos que permite a gestão de clientes, definição de carteiras recomendadas (Top Five) e a execução automatizada de ordens de compra baseadas em dados reais da B3.

## 🚀 Funcionalidades Principais

- **Módulo de Clientes:** Cadastro de investidores com validação de aporte mensal mínimo (R$ 100,00).
- **Módulo de Portfólio:** Gestão da "Cesta Top Five", garantindo que exatamente 5 ações componham 100% da estratégia.
- **Módulo de Mercado:** Processador de arquivos COTAHIST da B3 para atualização de preços reais dos ativos.
- **Purchase Engine (Motor de Compra):** Cálculo automatizado da quantidade de ações que cada cliente deve comprar, respeitando o saldo disponível e arredondando para lotes inteiros.
- **Mensageria com Kafka:** Envio de ordens de compra para processamento assíncrono de cálculos fiscais e de imposto de renda.

## 🛠️ Tecnologias e Fundamentos

Este projeto foi construído utilizando os seguintes conceitos e ferramentas:

- **.NET 8 (C#):** Linguagem principal utilizando Top-Level Statements e Programação Orientada a Objetos (POO).
- **Entity Framework Core:** ORM (Object-Relational Mapper) para traduzir as classes C# em tabelas no MySQL.
- **MySQL:** Banco de dados relacional para persistência de dados.
- **Apache Kafka:** Plataforma de streaming de eventos para comunicação entre módulos de forma escalável.
- **Docker & Docker Compose:** Orquestração de containers para facilitar o setup do ambiente de desenvolvimento.

## 📂 Estrutura do Projeto

O projeto segue o padrão de Monólito Modular, onde cada pasta dentro de Modules representa um domínio de negócio isolado:

```
api/
├── Modules/
│   ├── Customers/   # Gestão de investidores
│   ├── Portfolio/   # Gestão de cestas e ações
│   ├── Market/      # Integração com arquivos B3
│   └── Trading/     # Motor de compra e ordens
├── Infrastructure/  # Configurações globais (DB, Mensageria)
└── Migrations/      # Histórico de evolução do banco de dados
```

## ⚙️ Como Executar

### Pré-requisitos

- Docker Desktop instalado.

- SDK do .NET 8.

### Passo a Passo

1. **Subir a Infraestrutura**

	Na raiz do projeto, execute:

	```bash
	docker-compose up -d
	```

	Isso iniciará o MySQL, Kafka e Zookeeper.

2. **Atualizar o Banco de Dados**

	Dentro da pasta `api`, aplique as migrations:

	```bash
	dotnet ef database update
	```

3. **Rodar a Aplicação**

	```bash
	dotnet run
	```

4. **Aceder à Documentação**

	Com a API rodando, abra o seu navegador em http://localhost:5000/swagger para interagir com os endpoints criados.