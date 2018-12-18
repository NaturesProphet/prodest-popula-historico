[![Commitizen friendly](https://img.shields.io/badge/commitizen-friendly-brightgreen.svg)] (http://commitizen.github.io/cz-cli/) [![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-yellow.svg)](https://conventionalcommits.org)

# popMQ
Serviço de armazenamento de historico dos veículos da CETURB. Versão C#.

## Variaveis de ambiente para o Docker
```bash
SQL_SERVER_HOST             # Apontar para o IP do banco mssql
Default: localhost

SQL_SERVER_SCHEMA           # Apontar o nome do banco
Default: tempdb

SQL_SERVER_USER             # Apontar o usuario do banco
Default: SA

SQL_SERVER_PASSWORD         # Apontar a senha do usuario acima
Default: Senha@123

RABBIT_HOST                 # Apontar o IP do servidor de RabbitMQ
Default: CETURB

RABBIT_TOPIC                # Apontar o nome do topico a ser escutado
Default: CETURB

RABBIT_CHANNEL              # Apontar o nome do canal no tópico
Default: realtime.sql

RABBIT_KEY                  # Apontar a chave de roteamento a ser usada para ouvir
Default: "#"

NODE_ENV                    # Em produção, setar o valor production
Default: null

CONTAGEM_ANUNCIO            # quantidade de mensagens que o app espera para informar seu status de contagem
Default: 10000
```

## Requiriments para produção
para rodar em produção, basta o .Net Core 2.1 instalado, além do banco e do rabbit.

### Requiriments para desenvolvimento
Para rodar localmente em ambiente de teste, é necessário
.Net Core 2.1 (para rodar o app principal)
Node 8+ (para usar os scripts pré configurados)
Docker (para subir os serviços de RabbitMQ e SQLServer)



## Ferramentas de teste
Iniciar o banco docker de teste
```bash
npm run db
```
o app não usa nenhum ORM, mas mesmo assim não precisa rodar o create manualmente, pois em ambientes não-produção eu programei para criar a tabela automaticamente. (aqui o sistema é bruto...)

Iniciar o RabbitMQ de teste
```bash
npm run rabbit
```

Depois de subir o banco e o rabbit, é só startar a aplicação
```bash
dotnet run
```
o dotnet run já baixa as dependencias e executa tudo sozinho.


Para ver funcionando, vc pode executar scripts js que envia uma ou milhares de mensagens de uma vez só para o RabbitMQ.
```bash
npm run send:rabbit         # Envia uma mensagem simples ao Rabbit
npm run sendflood:rabbit    # Envia milhares de mensagens ao rabbit (situação próxima do real)
```

Parar o banco docker de teste (exclui os dados)
```bash
npm run stopdb
```