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
                    X9.OQueRolouNaParada(1, query, dadosConexao, null);
                }
                if (!Ambiente.isProduction()) //modo verboso ativo fora de produção. (spam info)
                {
                    X9.ShowInfo(4, h.getJsonRastreio());
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
                        X9.OQueRolouNaParada(2, this.dadosConexao, null, null);
                    }
                    else
                    {
                        X9.OQueRolouNaParada(3, e.Message, this.dadosConexao, null);
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
                    X9.ShowInfo(5, null);
                }
                catch (Exception e)
                {
                    X9.OQueRolouNaParada(4, e.Message, this.dadosConexao, create);
                }
            }
            else
            {
                X9.ShowInfo(6, null);
            }
        }
    }
}
