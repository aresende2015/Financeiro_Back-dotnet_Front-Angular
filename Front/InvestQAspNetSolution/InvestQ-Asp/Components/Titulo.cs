using Microsoft.AspNetCore.Mvc;

namespace InvestQ_Asp.Components
{
    public class Titulo : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
