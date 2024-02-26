using Capas.FormularioReporte;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Entidades;
using Entidades.Modelos;
using System.Collections.Generic;

namespace Negocio
{
    public static class NegocioParametrosReporte
    {
        private static ModeloParametros _modeloParametro;

        private static List<ModeloParametros> _listaParametrosRango = new List<ModeloParametros>();
        private static List<ModeloParametros> _listaParametrosDiscreto = new List<ModeloParametros>();
        private static List<List<ModeloParametros>> _ambasListas = new List<List<ModeloParametros>>();

        public static List<List<ModeloParametros>> NegocioGetAmbasListas()
        {
            RellenarListasConParametrosRangoDiscreto();
            return _ambasListas;
        }
        private static void RellenarListasConParametrosRangoDiscreto()
        {
            foreach (ParameterFieldDefinition parametro in Global.ReporteCargado.DataDefinition.ParameterFields)
            {
                _modeloParametro = new ModeloParametros();
                _modeloParametro.Parametro = parametro;

                if (NoEsSubreprote())
                {
                    //AsignarNombreDeParametroSinPrefijoSiEsDeRango();
                    AgregarParametroA_ListaRangoO_Discreto();
                }
            }
            _ambasListas.Add(_listaParametrosDiscreto);
            _ambasListas.Add(_listaParametrosRango);
            //BucleParametrosListasRangoDiscreto();
        }
        private static bool NoEsSubreprote()
        {
            return (_modeloParametro.NombreParametroSubreporte == "" || _modeloParametro.NombreParametroSubreporte == null);
        }

        private static void AgregarParametroA_ListaRangoO_Discreto()
        {
            if (ParametroEsRango()) _listaParametrosRango.Add(_modeloParametro);
            else _listaParametrosDiscreto.Add(_modeloParametro);
        }

        private static bool ParametroEsRango()
        {
            return (_modeloParametro.RangoDiscretoParametro is DiscreteOrRangeKind.RangeValue || 
                CondicionesParametros.IgualA_Todo(_modeloParametro.IniFinParametro, _modeloParametro.DesdeHastaParametro));
        }

        public static string AsignarNombreDeParametroSinPrefijoSiEsDeRango() //Se usa para establecer el text del label
        {
            
            if (CondicionesParametros.IgualA_INI_O_FIN(_modeloParametro.IniFinParametro))
            {
                return _modeloParametro.NombreParametro.Substring(0, _modeloParametro.NombreParametro.Length - 3) + ":";
            }
            else if (CondicionesParametros.IgualA_DESDE_O_HASTA(_modeloParametro.DesdeHastaParametro))
            {
                return _modeloParametro.NombreParametro.Substring(5).Replace(" ", "") + ":";
            }

            return _modeloParametro.NombreParametro + ":";
            //_nombreLabel = _nombreParametro + ":";
        }
        
    }
}
