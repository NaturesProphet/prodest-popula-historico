using System;

namespace popMQ
{
    static class Ambiente
    {
        public static string getSqlHost()
        {
            string host = Environment.GetEnvironmentVariable("SQL_SERVER_HOST");
            if (host is null) host = "localhost";
            return host;
        }

        public static string getSqlSchema()
        {
            string schema = Environment.GetEnvironmentVariable("SQL_SERVER_SCHEMA");
            if (schema is null) schema = "tempdb";
            return schema;
        }

        public static string getSqlUser()
        {
            String user = Environment.GetEnvironmentVariable("SQL_SERVER_USER");
            if (user is null) user = "SA";
            return user;
        }

        public static String getSqlPassword()
        {
            string pass = Environment.GetEnvironmentVariable("SQL_SERVER_PASSWORD");
            if (pass is null) pass = "Senha@123";
            return pass;
        }

        public static String getRabbitHost()
        {
            string host = Environment.GetEnvironmentVariable("RABBIT_HOST");
            if (host is null) host = "localhost";
            return host;
        }

        public static String getRabbitTopic()
        {
            string topico = Environment.GetEnvironmentVariable("RABBIT_TOPIC");
            if (topico is null) topico = "CETURB";
            return topico;
        }

        public static String getRabbitKey()
        {
            string key = Environment.GetEnvironmentVariable("RABBIT_KEY");
            if (key is null) key = "#";
            return key;
        }
        public static String getRabbitChannelName()
        {
            string canal = Environment.GetEnvironmentVariable("RABBIT_CHANNEL");
            if (canal is null) canal = "realtime.sql";
            return canal;
        }

        public static bool isProduction()
        {
            string env = Environment.GetEnvironmentVariable("NODE_ENV");
            if (env is null) return false;
            if (env.Equals("production")) return true;
            return false;
        }

        public static int getContagem()
        {
            string env = Environment.GetEnvironmentVariable("CONTAGEM_ANUNCIO");
            if (env is null) return 10000;
            try
            {
                int contagem = int.Parse(env);
                return contagem;
            }
            catch (Exception e)
            {
                return 10000;
            }
        }
    }
}
