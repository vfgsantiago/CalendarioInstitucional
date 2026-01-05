using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CalendarioInstitucional.Repository;
using CalendarioInstitucional.UI.Web.Models;

namespace CalendarioInstitucional.UI.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        #region Repositories
        private readonly EventoREP _repositorioEvento;
        private readonly CategoriaREP _repositorioCategoria;
        #endregion

        #region Constructor
        public HomeController(EventoREP repositorioEvento,
            CategoriaREP repositorioCategoria)
        {
            _repositorioEvento = repositorioEvento;
            _repositorioCategoria = repositorioCategoria;
        }
        #endregion

        #region Methods

        #region Index
        public async Task<IActionResult> Index()
        {
            var viewMOD = new HomeAdminViewMOD();
            viewMOD.QtdEventos = await _repositorioEvento.Contar();
            viewMOD.QtdCategorias = await _repositorioCategoria.Contar();
            viewMOD.QtdEventosMes = await _repositorioEvento.ContarEventosMes();
            viewMOD.QtdEventosDia = await _repositorioEvento.ContarEventosHoje();
            viewMOD.ListaEventosMes = await _repositorioEvento.BuscarEventosMes();
            viewMOD.EventosPorCategoria = await _repositorioEvento.BuscarEventosPorCategoria();
            return View(viewMOD);
        }
        #endregion

        #endregion
    }
}
