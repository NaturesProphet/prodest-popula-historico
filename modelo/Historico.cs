using System;

namespace popMQ
{
    class Historico
    {
        private String JsonRastreio;
        //private String Uuid;
        //private DateTime dataRegistro;
        //private DateTime atualizadoEm;

        public String getJsonRastreio()
        {
            return this.JsonRastreio;
        }
        // public String getUuid()
        // {
        //     return this.Uuid;
        // }
        // public DateTime getDataRegistro()
        // {
        //     return this.dataRegistro;
        // }
        // public DateTime getDataUpdate()
        // {
        //     return this.atualizadoEm;
        // }
        public void setJsonRastreio(String rastreio)
        {
            this.JsonRastreio = rastreio;
        }
    }
}
