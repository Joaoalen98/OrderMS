# Projeto: **API Minimal com MongoDB e RabbitMQ**

## Objetivo:
O projeto é uma API desenvolvida com **ASP.NET Minimal API** que consome mensagens de uma fila **RabbitMQ**, processa esses dados e os persiste em um banco de dados não relacional **MongoDB**. A API segue uma arquitetura simples e é voltada para a escalabilidade e uso eficiente de tecnologias modernas.

## Tecnologias utilizadas:
- **ASP.NET Minimal API**: Framework leve para a construção da API.
- **MongoDB.Driver**: Biblioteca oficial do MongoDB para C# que permite a comunicação com o banco de dados não relacional.
- **RabbitMQ**: Sistema de mensageria usado para receber dados assíncronos.
- **Docker**: Utilizado para containerizar o RabbitMQ e MongoDB.
- **C#**: Linguagem principal para o desenvolvimento da aplicação.

## Funcionalidades:
1. **Recebimento de Mensagens**: A API consome dados de uma fila RabbitMQ.
2. **Persistência de Dados**: Os dados recebidos são salvos em um banco de dados MongoDB.
3. **Recuperação de Dados**: A API oferece um endpoint para obter os pedidos por codigo do cliente.
