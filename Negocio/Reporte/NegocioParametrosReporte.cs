using Datos;
using Entidades;
using System;

namespace Negocio
{ 
    public static class NegocioParametrosReporte
    {   
        public static string NegocioConsultaParametros(string tabla, string columna, bool minimoMaximo)
        {
            return new ConsultasParametros(tabla, columna, minimoMaximo).ResultadoQuery;
        }
        
    }
}
