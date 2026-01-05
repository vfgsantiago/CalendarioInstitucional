using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CalendarioInstitucional.Model;
using CalendarioInstitucional.Repository;
using CalendarioInstitucional.UI.Web.Models;

namespace CalendarioInstitucional.UI.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        #region Repositories
        private readonly EventoREP _repositorioEvento;
        private readonly CategoriaREP _repositorioCategoria;
        #endregion

        #region Constructor
        public HomeController(
            EventoREP repositorioEvento,
            CategoriaREP repositorioCategoria)
        {
            _repositorioEvento = repositorioEvento;
            _repositorioCategoria = repositorioCategoria;
        }
        #endregion

        #region Methods

        #region Index
        public async Task<IActionResult> Index(
            int? ano,
            int? mes,
            DateTime? data,
            CalendarioViewTipo tipo = CalendarioViewTipo.Mensal,
            bool somenteComEventos = false,
            string? categorias = null)
        {
            DateTime dataBase = data ??
                new DateTime(
                    ano ?? DateTime.Today.Year,
                    mes ?? DateTime.Today.Month,
                    1);

            var eventos = await _repositorioEvento.BuscarCalendario(dataBase, categorias);
            var listaCategorias = await _repositorioCategoria.Buscar();

            var dias = new List<CalendarioDiaViewMOD>();

            #region VISÃO SEMANAL
            if (tipo == CalendarioViewTipo.Semanal)
            {
                var inicioSemana = dataBase.AddDays(-(int)dataBase.DayOfWeek + 1);
                var fimSemana = inicioSemana.AddDays(6);

                for (var d = inicioSemana; d <= fimSemana; d = d.AddDays(1))
                {
                    dias.Add(new CalendarioDiaViewMOD
                    {
                        Data = d,
                        Eventos = eventos
                            .Where(e => e.DtInicioEvento!.Value.Date <= d &&
                                        e.DtFimEvento!.Value.Date >= d)
                            .ToList()
                    });
                }

                if (somenteComEventos)
                {
                    dias = dias.Where(d => d.Eventos.Any()).ToList();
                }

                return View(BuildViewModel(
                    dataBase,
                    tipo,
                    eventos,
                    listaCategorias,
                    dias,
                    somenteComEventos,
                    inicioSemana,
                    fimSemana));
            }
            #endregion

            #region VISÃO DIÁRIA
            if (tipo == CalendarioViewTipo.Diaria)
            {
                var eventosDoDia = eventos
                    .Where(e => e.DtInicioEvento!.Value.Date <= dataBase &&
                                e.DtFimEvento!.Value.Date >= dataBase)
                    .ToList();

                if (!somenteComEventos || eventosDoDia.Any())
                {
                    dias.Add(new CalendarioDiaViewMOD
                    {
                        Data = dataBase,
                        Eventos = eventosDoDia
                    });
                }

                return View(BuildViewModel(
                    dataBase,
                    tipo,
                    eventos,
                    listaCategorias,
                    dias,
                    somenteComEventos));
            }
            #endregion

            #region VISÃO MENSAL
            int totalDias = DateTime.DaysInMonth(dataBase.Year, dataBase.Month);

            for (int d = 1; d <= totalDias; d++)
            {
                var dia = new DateTime(dataBase.Year, dataBase.Month, d);

                var eventosDoDia = eventos
                    .Where(e => e.DtInicioEvento!.Value.Date <= dia &&
                                e.DtFimEvento!.Value.Date >= dia)
                    .ToList();

                dias.Add(new CalendarioDiaViewMOD
                {
                    Data = dia,
                    Eventos = eventosDoDia
                });
            }

            if (somenteComEventos)
            {
                dias = dias.Where(d => d.Eventos.Any()).ToList();
            }

            return View(BuildViewModel(
                dataBase,
                tipo,
                eventos,
                listaCategorias,
                dias,
                somenteComEventos));
            #endregion
        }
        #endregion

        #region BuildViewModel
        private CalendarioViewMOD BuildViewModel(
            DateTime dataBase,
            CalendarioViewTipo tipo,
            List<EventoMOD> eventos,
            List<CategoriaMOD> categorias,
            List<CalendarioDiaViewMOD> dias,
            bool somenteComEventos,
            DateTime? semanaInicio = null,
            DateTime? semanaFim = null)
        {
            return new CalendarioViewMOD
            {
                Ano = dataBase.Year,
                Mes = dataBase.Month,
                DiaSelecionado = dataBase.ToString("dddd, dd MMM yyyy"),
                MesDescricao = dataBase.ToString("MMMM yyyy"),
                Tipo = tipo,
                SomenteComEventos = somenteComEventos,
                Eventos = eventos,
                Categorias = categorias,
                DiasDoMes = dias,
                SemanaInicio = semanaInicio ?? dataBase,
                SemanaFim = semanaFim ?? dataBase,
                ProximosEventos = eventos
                    .Where(e => e.DtInicioEvento >= DateTime.Today &&
                                e.DtInicioEvento <= DateTime.Today.AddDays(10))
                    .OrderBy(e => e.DtInicioEvento)
                    .Take(10)
                    .ToList()
            };
        }
        #endregion

        #region Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewMOD
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
        #endregion

        #endregion
    }
}