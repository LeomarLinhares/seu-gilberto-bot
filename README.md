# Telegram Bot em C#

## Descrição

Este projeto é um bot para Telegram desenvolvido em C#. Ele utiliza a API do Telegram para interagir com usuários e realizar diversas funcionalidades automatizadas. O objetivo principal do bot é gerenciar uma temporada de Cartola FC entre amigos em um grupo de Telegram.

<!--
## Funcionalidades

- [Funcionalidade 1: Descrição breve]
- [Funcionalidade 2: Descrição breve]
- [Funcionalidade 3: Descrição breve]
-->
## Pré-requisitos

Antes de começar, certifique-se de ter o seguinte instalado:

- [.NET 6 ou versão superior](https://dotnet.microsoft.com/download)
- Token do bot do Telegram (criado via [BotFather](https://core.telegram.org/bots#botfather))

## Configuração

1. Clone o repositório:

   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
   ```

2. Configure o arquivo de variáveis de ambiente ou `appsettings.json` com o token do bot:

   **appsettings.json**
   Crie um arquivo `appsettings.json` na raiz do projeto:
   ```json
   {
     "TelegramBot": {
       "Token": "seu-token-aqui"
     }
   }
   ```

3. Restaure as dependências do projeto:

   ```bash
   dotnet restore
   ```

4. Compile e execute o projeto:

   ```bash
   dotnet run
   ```

## Uso

Após iniciar o bot, ele estará disponível para interação no Telegram. Pesquise pelo nome do bot ou use o link fornecido pelo BotFather para começar.

## Dependências

Este projeto utiliza as seguintes bibliotecas:

- [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot): Biblioteca oficial para integração com a API do Telegram.

## Contribuindo

Contribuições são bem-vindas! Siga as etapas abaixo para contribuir:

1. Fork este repositório.
2. Crie uma branch para sua feature:
   ```bash
   git checkout -b minha-feature
   ```
3. Faça commit das suas alterações:
   ```bash
   git commit -m "Minha nova feature"
   ```
4. Envie as alterações para sua branch:
   ```bash
   git push origin minha-feature
   ```
5. Abra um Pull Request.

## Licença

Este projeto está licenciado sob a [Licença MIT](LICENSE).

## Contato

Leomar Linhares - contato@leomarlinhares.com

Sinta-se à vontade para entrar em contato se tiver dúvidas ou sugestões!
