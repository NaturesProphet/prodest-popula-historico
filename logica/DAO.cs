using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace popMQ
{
    class DAO
    {
        private String host;
        private String schema;
        private String user;
        private String pass;
        private String dadosConexao;
        private int errorCount;
        public DAO()
        {
            this.host = Ambiente.getSqlHost();
            this.schema = Ambiente.getSqlSchema();
            this.user = Ambiente.getSqlUser();
            this.pass = Ambiente.getSqlPassword();
            this.errorCount = 0;
            this.dadosConexao = (
                       "Data Source=" + host + ";" + "Initial Catalog=" + schema + ";" +
                       "User id=" + user + ";" + "Password=" + pass + ";"
            );
        }

        public void salvaHistorico(Historico h)
        {
            String query = "INSERT INTO veiculo (dados) VALUES(@dados)";
            try
            {
                SqlConnection conexao = new SqlConnection(this.dadosConexao);
                SqlCommand comando = new SqlCommand(query, conexao);
                comando.Parameters.Add("@dados", SqlDbType.Text);
                comando.Parameters["@dados"].Value = h.getJsonRastreio();
                comando.Connection.Open();
                int rows = comando.ExecuteNonQuery(); //numero de linhas afetadas pela query
                comando.Dispose();
                conexao.Close();
                if (rows <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\n##############################################");
                    Console.WriteLine("[   DAO   ]   Algo inesperado aconteceu. ");
                    Console.Write("Os dados não foram registrados.");
                    Console.WriteLine("[   DAO   ]   Query executada: " + query);
                    Console.WriteLine("[   DAO   ]   String de conexão utilizada:");
                    Console.WriteLine(this.dadosConexao);
                    Console.WriteLine("##############################################\n");
                    Console.ResetColor();
                }
                if (!Ambiente.isProduction()) //modo verboso ativo fora de produção. (spam info)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n-------------------------------------------------------");
                    Console.WriteLine("[   DAO   ]   Informaçẽos armazenadas com sucesso. valor:");
                    Console.WriteLine(h.getJsonRastreio());
                    Console.WriteLine("-------------------------------------------------------\n");
                    Console.ResetColor();
                }

            }
            catch (Exception e)
            {
                if (
                        e.Message.Equals("Invalid object name 'veiculo'.")
                        && !Ambiente.isProduction()
                        && errorCount == 0
                    )
                {
                    errorCount++;
                    CreateTable();   // SYNC DE ORM É PARA OS FRACOS! Aqui o sistema é bruto.
                    salvaHistorico(h); // caberia um goto aqui fácil.. mas vamos de recursão.
                }
                else
                {
                    if (e.Message.Equals("Invalid object name 'veiculo'."))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n##############################################");
                        Console.WriteLine("[   DAO   ]   Erro ao tentar salvar um Historico   ---->>");
                        Console.WriteLine("[   DAO   ]   A TABELA 'veiculo' NÃO EXISTE OU NÃO FOI ENCONTRADA!");
                        Console.WriteLine("[   DAO   ]   String de conexão utilizada: ");
                        Console.WriteLine(this.dadosConexao);
                        Console.WriteLine("##############################################\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\n##############################################");
                        Console.WriteLine("[   DAO   ]   Erro ao tentar salvar um Historico\n");
                        Console.WriteLine(e.Message);
                        Console.WriteLine("[   DAO   ]   String de conexão utilizada: ");
                        Console.WriteLine(this.dadosConexao);
                        Console.WriteLine("##############################################\n");
                        Console.ResetColor();
                    }
                }
            }
        }


        public void CreateTable()
        {
            if (!Ambiente.isProduction())
            {
                String create = "CREATE TABLE " + this.schema + ".dbo.veiculo (" +
                "	dataregistro datetime2(7) DEFAULT (getdate()) NOT NULL," +
                "	atualizadoem datetime2(7) DEFAULT (getdate()) NOT NULL," +
                "	uuid UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID()," +
                "	dados TEXT NOT NULL," +
                "	CONSTRAINT PK_uuid PRIMARY KEY (uuid)" +
                ")";
                try
                {
                    SqlConnection conexao = new SqlConnection(this.dadosConexao);
                    SqlCommand comando = new SqlCommand(create, conexao);
                    comando.Connection.Open();
                    comando.ExecuteNonQuery();
                    comando.Dispose();
                    conexao.Close();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n##############################################");
                    Console.WriteLine("[   DAO   ]   TABELA GERADA AUTOMATICAMENTE");
                    Console.WriteLine("##############################################\n");
                    Console.ResetColor();
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n##############################################");
                    Console.WriteLine("[   DAO   ]   Erro ao tentar CRIAR a tabela veiculo\n");
                    Console.WriteLine(e.Message);
                    Console.WriteLine("[   DAO   ]   String de conexão utilizada: ");
                    Console.WriteLine(this.dadosConexao);
                    Console.WriteLine("[   DAO   ]   Query de CREATE TABLE utilziada: ");
                    Console.WriteLine(create);
                    Console.WriteLine("##############################################\n");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n##############################################");
                Console.WriteLine("[   DAO   ]   NÃO É PERMITIDO CRIAR NOVAS TABELAS EM PRODUÇÃO\n");
                Console.WriteLine("##############################################\n");
                Console.ResetColor();
            }
        }
    }
}
