using Capas.FormularioReporte;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Entidades;
using Entidades.Modelos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Negocio
{
    public static class NegocioParametrosReporte
    {
        private static ModeloParametros _modeloParametro;

        private static List<ModeloParametros> _listaParametrosRango;
        private static List<ModeloParametros> _listaParametrosDiscreto;
        private static List<List<ModeloParametros>> _ambasListas;

        public static List<List<ModeloParametros>> NegocioGetAmbasListas()
        {
            RellenarListasConParametrosRangoDiscreto();
            LlenarAmbasListasConListasDeParametros();
            return _ambasListas;
        }
        private static void RellenarListasConParametrosRangoDiscreto()
        {
            InstanciaY_VaciaListas();

            foreach (ParameterFieldDefinition parametro in Global.ReporteCargado.DataDefinition.ParameterFields)
            {
                _modeloParametro = new ModeloParametros();
                _modeloParametro.Parametro = parametro;

                if (NoEsSubreprote()) AgregarParametroA_ListaRangoO_Discreto();
            }
        }

        private static void InstanciaY_VaciaListas()
        {
            _listaParametrosRango = new List<ModeloParametros>();
            _listaParametrosDiscreto = new List<ModeloParametros>();

            VaciarListas(_listaParametrosRango, _listaParametrosDiscreto);
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

        private static void LlenarAmbasListasConListasDeParametros()
        {
            _ambasListas = new List<List<ModeloParametros>>();
            VaciarListas(_ambasListas);
            _ambasListas.Add(_listaParametrosDiscreto);
            _ambasListas.Add(_listaParametrosRango);
        }

        private static void VaciarListas<T>(params List<T>[] args)
        {
            foreach (List<T> lista in args) lista?.Clear();
        }
    }
}
