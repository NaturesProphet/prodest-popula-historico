using System;

/**
Fiz esta classe para obter um código mais limpo no programa principal.
O objetivo é apenas printar erros e informações no terminal.
*/
namespace popMQ
{
    static class X9
    {

        public static void ShowInfo(int NumeroMagico, String msg)
        {
            switch (NumeroMagico)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n[  POP-MQ  ]   Serviço inicializado em modo de produção!");
                    Console.ResetColor();
                    break;

                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n[  POP-MQ  ]   Serviço inicializado em modo de Desenvolvimento!");
                    Console.WriteLine("[  POP-MQ  ]   Aguardando a chegada dos dados...\n");
                    Console.ResetColor();
                    break;

                case 3:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine("[  POP-MQ  ]   Recebendo dados: " + msg);
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.ResetColor();
                    break;

                case 4:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n-------------------------------------------------------");
                    Console.WriteLine("[   DAO   ]   Informaçẽos armazenadas com sucesso. valor:");
                    Console.WriteLine(msg);
                    Console.WriteLine("-------------------------------------------------------\n");
                    Console.ResetColor();
                    break;

                case 5:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n##############################################");
                    Console.WriteLine("[   DAO   ]   TABELA GERADA AUTOMATICAMENTE");
                    Console.WriteLine("##############################################\n");
                    Console.ResetColor();
                    break;

                case 6:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n##############################################");
                    Console.WriteLine("[   DAO   ]   NÃO É PERMITIDO CRIAR NOVAS TABELAS EM PRODUÇÃO\n");
                    Console.WriteLine("##############################################\n");
                    Console.ResetColor();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n##############################################");
                    Console.WriteLine("[   X9  ]   MENSAGEM DE INFORMACAO NAO ESPECIFICADA\n");
                    Console.WriteLine("##############################################\n");
                    Console.ResetColor();
                    break;
            }
        }


        public static void OQueRolouNaParada(Exception e, int NumeroMagico)
        {
            switch (NumeroMagico)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n##########################################################");
                    Console.WriteLine("[ POP-MQ  ]   Erro ao tentar se conectar com o RabbitMQ");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine("[ ERROR:  ]   " + e.Message);
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine("[ POP-MQ  ]   Dados da conexão listados a seguir:\n");
                    Console.WriteLine("[ POP-MQ  ]   Host: " + Ambiente.getRabbitHost());
                    Console.WriteLine("[ POP-MQ  ]   Topico: " + Ambiente.getRabbitTopic());
                    Console.WriteLine("[ POP-MQ  ]   Chave de Rota: " + Ambiente.getRabbitKey());
                    Console.WriteLine("[ POP-MQ  ]   Canal: " + Ambiente.getRabbitChannelName());
                    Console.WriteLine("##########################################################");
                    Console.ResetColor();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n##############################################");
                    Console.WriteLine("[   X9  ]   ERRO NÃO ESPECIFICADO\n");
                    Console.WriteLine("##############################################\n");
                    Console.ResetColor();
                    break;
            }
        }
        public static void OQueRolouNaParada(int NumeroMagico, String msg1, String msg2, String msg3)
        {
            switch (NumeroMagico)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\n##############################################");
                    Console.WriteLine("[   DAO   ]   Algo inesperado aconteceu. ");
                    Console.Write("Os dados não foram registrados.");
                    Console.WriteLine("[   DAO   ]   Query executada: " + msg1);
                    Console.WriteLine("[   DAO   ]   String de conexão utilizada:");
                    Console.WriteLine(msg2);
                    Console.WriteLine("##############################################\n");
                    Console.ResetColor();
                    break;

                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n##############################################");
                    Console.WriteLine("[   DAO   ]   Erro ao tentar salvar um Historico   ---->>");
                    Console.WriteLine("[   DAO   ]   A TABELA 'veiculo' NÃO EXISTE OU NÃO FOI ENCONTRADA!");
                    Console.WriteLine("[   DAO   ]   String de conexão utilizada: ");
                    Console.WriteLine(msg1);
                    Console.WriteLine("##############################################\n");
                    Console.ResetColor();
                    break;

                case 3:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n##############################################");
                    Console.WriteLine("[   DAO   ]   Erro ao tentar salvar um Historico\n");
                    Console.WriteLine(msg1);
                    Console.WriteLine("[   DAO   ]   String de conexão utilizada: ");
                    Console.WriteLine(msg2);
                    Console.WriteLine("##############################################\n");
                    Console.ResetColor();
                    break;

                case 4:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n##############################################");
                    Console.WriteLine("[   DAO   ]   Erro ao tentar CRIAR a tabela veiculo\n");
                    Console.WriteLine(msg1);
                    Console.WriteLine("[   DAO   ]   String de conexão utilizada: ");
                    Console.WriteLine(msg2);
                    Console.WriteLine("[   DAO   ]   Query de CREATE TABLE utilziada: ");
                    Console.WriteLine(msg3);
                    Console.WriteLine("##############################################\n");
                    Console.ResetColor();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n##############################################");
                    Console.WriteLine("[   X9  ]   ERRO NÃO ESPECIFICADO\n");
                    Console.WriteLine("##############################################\n");
                    Console.ResetColor();
                    break;
            }
        }
    }
}
