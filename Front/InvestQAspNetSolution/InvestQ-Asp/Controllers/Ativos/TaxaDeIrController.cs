using InvestQ_Asp.Models.Acoes;
using InvestQ_Asp.Models.Ativos;
using InvestQ_Asp.Services.Ativos.IServices;
using InvestQ_Asp.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvestQ_Asp.Areas.Ativos.Controllers
{
    public class TaxaDeIrController : Controller
    {
        private readonly ITaxaDeIrService _taxaDeIService;

        public TaxaDeIrController(ITaxaDeIrService taxaDeIrService)
        {
            _taxaDeIService = taxaDeIrService ?? throw new ArgumentNullException(nameof(taxaDeIrService));
        }

        [Authorize]
        public async Task<IActionResult> TaxaDeIrLista()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var taxasDeIrLista = await _taxaDeIService.FindAllTaxaDeIr(accessToken);
            
            return View(taxasDeIrLista);
        }

        [Authorize]
        public async Task<IActionResult> TaxaDeIrCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> TaxaDeIrCreate(TaxaDeIrModel model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _taxaDeIService.CreateTaxaDeIr(model, accessToken);

                if (response != null) return RedirectToAction(nameof(TaxaDeIrLista));
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> TaxaDeIrUpdate(Guid id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var taxaDeIr = await _taxaDeIService.FindTaxaDeIrById(id, accessToken);

            if (taxaDeIr != null) return View(taxaDeIr);

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> TaxaDeIrUpdate(TaxaDeIrModel model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _taxaDeIService.UpdateTaxaDeIr(model, model.Id, accessToken);

                if (response != null) return RedirectToAction(nameof(TaxaDeIrLista));
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> TaxaDeIrDelete(Guid id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var taxaDeIr = await _taxaDeIService.FindTaxaDeIrById(id, accessToken);

            if (taxaDeIr != null) return View(taxaDeIr);

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> TaxaDeIrDelete(TaxaDeIrModel model)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _taxaDeIService.DeleteTaxaDeIrById(model.Id, accessToken);

            if (response != null) return
                    RedirectToAction(nameof(TaxaDeIrLista));

            return BadRequest();
        }
    }
}
