using InvestQ_Asp.Models.Acoes;
using InvestQ_Asp.Services.Acoes.IServices;
using InvestQ_Asp.Utils;
using InvestQ_Asp.ViewModels.Acoes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ_Asp.Controllers.Acoes
{
    public class SetorController : Controller
    {
        private readonly ISetorService _setorService;

        public SetorController(ISetorService setorService)
        {
            _setorService = setorService ?? throw new ArgumentNullException(nameof(setorService));
        }

        [Authorize]
        public async Task<IActionResult> SetorLista()
        {
            //ViewData["Data"] = DateTime.Now.ToShortDateString();

            //var setores = await _setorService.FindAllSetores();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var setoresListaViewModel = new SetorListaViewModel();

            setoresListaViewModel.Setores = await _setorService.FindAllSetores(accessToken);

            //var totalSetores = setores.Count();
            //var totalSetores = setoresListaViewModel.Setores.Count();

            //ViewBag.Total = "Total de Setores : ";
            //ViewBag.TotalSetores = totalSetores;

            return View(setoresListaViewModel);
        }

        [Authorize]
        public async Task<IActionResult> SetorCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> SetorCreate(SetorModel model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _setorService.CreateSetor(model, accessToken);

                if (response != null) return RedirectToAction(nameof(SetorLista));
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> SetorUpdate(Guid id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var setor = await _setorService.FindSetorById(id, accessToken);

            if (setor != null) return View(setor);

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> SetorUpdate(SetorModel model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _setorService.UpdateSetor(model, model.Id, accessToken);

                if (response != null) return RedirectToAction(nameof(SetorLista));
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> SetorDelete(Guid id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var setor = await _setorService.FindSetorById(id, accessToken);

            if (setor != null) return View(setor);

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> SetorDelete(SetorModel model)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _setorService.DeleteSetorById(model.Id, accessToken);

            if (response != null) return
                    RedirectToAction(nameof(SetorLista));

            return BadRequest();
        }
    }
}
