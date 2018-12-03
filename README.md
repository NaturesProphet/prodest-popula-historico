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
Default: realtime

RABBIT_KEY                  # Apontar a chave de roteamento a ser usada para ouvir
Default: "#"

NODE_ENV                    # Em produção, setar o valor production
Default: null
```

## Ferramentas de teste
Iniciar o banco docker de teste
```bash
npm run db
depois execute o script de CREATE que está na raiz do repositorio.
```

Parar o banco docker de teste (exclui os dados)
```bash
npm run stopdb
```
