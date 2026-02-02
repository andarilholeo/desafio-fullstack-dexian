# 🎓 Sistema de Gestão Escolar - Desafio Dexian

Sistema fullstack para gerenciamento de escolas e alunos, desenvolvido como desafio técnico.

## 📋 Índice

- [Tecnologias](#-tecnologias)
- [Arquitetura](#-arquitetura)
- [Pré-requisitos](#-pré-requisitos)
- [Como Executar](#-como-executar)
- [Funcionalidades](#-funcionalidades)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [API Endpoints](#-api-endpoints)
- [Credenciais de Acesso](#-credenciais-de-acesso)

## 🚀 Tecnologias

### Backend
- **.NET 9** - Framework principal
- **ASP.NET Core Web API** - API RESTful
- **JWT (JSON Web Token)** - Autenticação
- **Clean Architecture** - Padrão arquitetural
- **Swagger/OpenAPI** - Documentação da API

### Frontend
- **Angular 21** - Framework SPA
- **Angular Material** - Componentes UI (Material Design 3)
- **TypeScript** - Linguagem principal

## 🏗 Arquitetura

### Backend - Clean Architecture

```
src/
├── DesafioDexian.API/           # Camada de apresentação (Controllers, Program.cs)
├── DesafioDexian.Application/   # Camada de aplicação (DTOs, Services, Interfaces)
├── DesafioDexian.Domain/        # Camada de domínio (Entities, Interfaces)
└── DesafioDexian.Infrastructure/# Camada de infraestrutura (Repositories, Data)
```

### Frontend - Feature-based Structure

```
client/desafio-dexian-app/src/app/
├── core/                        # Serviços, guards, interceptors, models
│   ├── guards/                  # Auth guard
│   ├── interceptors/            # HTTP interceptor (JWT)
│   ├── models/                  # Interfaces/tipos
│   └── services/                # Serviços (auth, aluno, escola)
├── features/                    # Módulos de funcionalidades
│   ├── alunos/                  # CRUD de alunos
│   ├── escolas/                 # CRUD de escolas
│   └── login/                   # Autenticação
└── shared/                      # Componentes compartilhados
    └── components/layout/       # Layout principal com sidenav
```

## 📦 Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js 20+](https://nodejs.org/)
- [npm 10+](https://www.npmjs.com/) ou [pnpm](https://pnpm.io/)

## 🎯 Como Executar

### 1. Backend (.NET API)

```bash
# Na raiz do projeto
cd src/DesafioDexian.API

# Restaurar dependências e executar
dotnet run
```

A API estará disponível em:
- **HTTP:** http://localhost:5000
- **HTTPS:** https://localhost:5001
- **Swagger:** https://localhost:5001/swagger

### 2. Frontend (Angular)

```bash
# Na pasta do cliente
cd client/desafio-dexian-app

# Instalar dependências
npm install

# Executar em modo desenvolvimento
npm start
```

A aplicação estará disponível em: **http://localhost:4200**

### 3. Executar Ambos Simultaneamente

Abra dois terminais:

**Terminal 1 - Backend:**
```bash
cd src/DesafioDexian.API && dotnet run
```

**Terminal 2 - Frontend:**
```bash
cd client/desafio-dexian-app && npm start
```

## ✨ Funcionalidades

### Autenticação
- Login com usuário e senha
- Token JWT com expiração de 2 horas
- Proteção de rotas autenticadas

### Gestão de Escolas
- Listagem com busca/filtro
- Cadastro de novas escolas
- Edição de escolas existentes
- Exclusão de escolas

### Gestão de Alunos
- Listagem com busca/filtro
- Cadastro com validação de CPF
- Vinculação com escola
- Edição e exclusão

### Interface
- Design responsivo (Desktop e Mobile)
- Tema visual com tons marrons/bege
- Menu lateral (Desktop) / Dropdown (Mobile)
- Feedback visual com snackbars

## 🔌 API Endpoints

### Autenticação
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/auth/login` | Realiza login |

### Escolas
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/escolas` | Lista todas as escolas |
| GET | `/api/escolas/{id}` | Obtém escola por ID |
| POST | `/api/escolas` | Cria nova escola |
| PUT | `/api/escolas/{id}` | Atualiza escola |
| DELETE | `/api/escolas/{id}` | Remove escola |

### Alunos
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/alunos` | Lista todos os alunos |
| GET | `/api/alunos/{id}` | Obtém aluno por ID |
| GET | `/api/alunos/escola/{escolaId}` | Lista alunos por escola |
| POST | `/api/alunos` | Cria novo aluno |
| PUT | `/api/alunos/{id}` | Atualiza aluno |
| DELETE | `/api/alunos/{id}` | Remove aluno |

## 🔐 Credenciais de Acesso

| Usuário | Senha |
|---------|-------|
| admin | admin123 |
| TESTE | 123 |

> ⚠️ **Nota:** Os dados são armazenados em memória (InMemoryDataStore). Ao reiniciar a API, os dados voltam ao estado inicial.

