# GastroSync - Backend

<p align="center">
  <img src="https://i.imgur.com/9bopGVU.png" alt="GastroSync Logo" width="600">
</p>

[Premiado como melhor projeto na feira de projetos da UNIPAM em 2023](https://youtu.be/3Smsd7WIpfQ)

## Introdução

Bem-vindo ao GastroSync Backend, a solução de gerenciamento avançada para estabelecimentos gastronômicos. Meu sistema integra pedidos, divisão de contas, processamento de pagamentos e gestão do estabelecimento, incluindo recursos como ativação de cover e controle da comissão de 10% do garçom, tudo em um único aplicativo.

## Tecnologias Utilizadas

![.NET Core](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dot-net&logoColor=white) 
![Angular](https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white) 
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white) 
![Docker](https://img.shields.io/badge/Docker-2CA5E0?style=for-the-badge&logo=docker&logoColor=white) 
![GitHub Actions](https://img.shields.io/badge/github-2088FF?style=for-the-badge&logo=github-actions&logoColor=white) 
![SonarQube](https://img.shields.io/badge/sonarqube-4E9BCD?style=for-the-badge&logo=sonarqube&logoColor=white) 
![Elastic](https://img.shields.io/badge/Elasticsearch-3cbbb3?style=for-the-badge&logo=elasticsearch&logoColor=white) 
![Kibana](https://img.shields.io/badge/kibana-005571?style=for-the-badge&logo=kibana&logoColor=white)

## Arquitetura

### Arquitetura Onion
O GastroSync adota a arquitetura Onion, garantindo separação clara de responsabilidades e independência de domínio.

### Banco de Dados SQL Server
Utilizei SQL Server, proporcionando um sistema robusto e escalável para gerenciamento de dados.

### Padrões de Projeto
Implementamos padrões como Injeção de Dependência, Singleton e Factory, assegurando eficiência e manutenibilidade.

## Testes

### Testes Unitários e de Integração
Realizamos testes unitários e de integração extensivos, garantindo a robustez do sistema.

## Qualidade de Software

### SonarQube
<p align="center">
<img src="https://i.imgur.com/o1Zw7MD.png" alt="Sonar" width="400">
</p>

O SonarQube é essencial para a análise e manutenção da qualidade do código.

- Análise do SonarCloud: [SonarCloud - GastroSync Backend](https://sonarcloud.io/project/overview?id=Higor-Matos_GastroSyncBackend)

## Logs e Monitoramento

### Elastic + Kibana
<p align="center">
<img src="https://i.imgur.com/MgxCkL0.png" alt="Elastic+Kibana" width="600">
</p>  
Monitoramos e analisamos logs utilizando Elastic e Kibana.

- Visualização dos logs: [Kibana Logs - GastroSync](https://i.imgur.com/9WhW2Hk.png)

## Instalação e Configuração

Certifique-se de que o Docker esteja instalado em sua máquina antes de prosseguir com a instalação e configuração do projeto. Aqui está o passo a passo:

1. Clone o projeto:
   ```bash
   git clone https://github.com/Higor-Matos/GastroSyncBackend.git

2. Navegue para a pasta do projeto:
   ```bash
   cd GastroSyncBackend

3. Inicie o projeto usando o Docker Compose:
   ```bash
   docker-compose up

4. Acesse a documentação após executar o projeto: [Swagger](http://localhost:8080/swagger)

## CI/CD

### GitHub Actions e Vercel
![GitHub Actions](https://i.imgur.com/U8z54yK.png) ![Vercel](https://i.imgur.com/H4pBikz.png)
Nosso processo de CI/CD é eficiente, utilizando GitHub Actions e Vercel.




