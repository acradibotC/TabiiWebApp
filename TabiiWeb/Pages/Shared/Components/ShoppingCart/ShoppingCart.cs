using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tabii.DataAccess.Repository.IRepository;
using Tabii.Utilities;

namespace TabiiWeb.Pages.Shared.Components.ShoppingCart
{
    public class ShoppingCart : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCart(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int count = 0;
            if (claim != null)
            {
                //user is logged in 
                if (HttpContext.Session.GetInt32(SD.SessionCart) != null)
                {
                    return View(HttpContext.Session.GetString(SD.SessionCart));
                }
                else
                {
                    count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
                    HttpContext.Session.SetInt32(SD.SessionCart, count);
                    return View(count);
                }
            }
            else
            {
                HttpContext.Session.Clear();
                //user has not logged in
                return View(count);
            }
        }
    }
}
