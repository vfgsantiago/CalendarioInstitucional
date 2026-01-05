# 🗓️ Sistema de Calendário Institucional

![Badge Status](https://img.shields.io/badge/Status-Concluido-green)
![Badge UI](https://img.shields.io/badge/Interface-Responsive-blue)
![Badge Type](https://img.shields.io/badge/Focus-Organization-purple)

> **Sincronia e visibilidade para todos os eventos da organização.**

Uma plataforma centralizada que elimina a confusão de e-mails e planilhas, oferecendo uma visão clara, categorizada e interativa de todos os compromissos, prazos e eventos corporativos.

---

## 🎯 O Objetivo
Conectar a organização através do tempo. O sistema garante que colaboradores, alunos ou parceiros saibam exatamente *o que* está acontecendo, *quando* e *onde*, enquanto fornece à gestão dados sobre o volume e tipos de atividades realizadas.

---

## 🌟 Funcionalidades por Perfil

### 1. 🌍 Portal Público (Visualização)
Uma interface limpa e intuitiva, desenhada para facilitar a consulta rápida.
* **Múltiplas Visões:** O usuário escolhe como quer ver o tempo:
    * 📅 **Mensal:** Visão macro para planejamento de longo prazo.
    * 📆 **Semanal:** Foco na rotina e horários.
    * 🕒 **Diária:** Detalhamento da agenda do dia.
* **Filtros Dinâmicos:** Checkboxes para filtrar eventos por **Categorias** (ex: *Treinamentos, Feriados, Reuniões Gerais, Prazos de Projetos*).
* **Detalhes Ricos:** Ao clicar em um evento, um modal exibe descrição completa, links de reunião (Teams/Zoom), local físico e anexos.

### 2. ⚙️ Área Administrativa (Gestão)
O back-office onde o calendário ganha vida.
* **Gestão de Conteúdo (CRUD):**
    * Cadastro completo de **Eventos** (com repetição, cores personalizadas e horários).
    * Gerenciamento de **Categorias** (Definição de cores e ícones para facilitar a leitura visual).
* **Dashboard de Indicadores:**
    * Quantidade de eventos por categoria (Gráfico de Pizza).
    * Densidade de eventos por mês (Sazonalidade).
    * Eventos mais visualizados/acessados (se houver tracking de cliques).

---

## 🛠️ Tecnologias Utilizadas

* **Linguagem:** C# (.NET)
* **Backend/Frontend:** ASP.NET Core (MVC & Web API)
* **Banco de Dados:** Oracle PLSQL
* **Estilização:** Bootstrap / CSS3 / AJAX / JQUERY

  ---

## 🛠️ Metodoloias Utilizadas

* **Arquitetura:** Camadas
* **Padrão:** Repository Pattern
  
---

## 🔄 Fluxo de Navegação

```mermaid
graph TD
    Admin((Administrador)) -->|Cadastra| A[Novo Evento]
    Admin -->|Define| B[Categoria / Cor]
    A --> DB[(Banco de Dados)]
    
    User((Usuário Público)) -->|Acessa| C[Visualização Calendário]
    DB --> C
    
    C -->|Filtra| D{Escolha de Visão}
    D -->|Mês/Semana/Dia| E[Renderização Dinâmica]
    E -->|Clica| F[Detalhes do Evento]
    
    DB -->|Dados Aggregados| G[Dashboard Analytics]
    G --> Admin
