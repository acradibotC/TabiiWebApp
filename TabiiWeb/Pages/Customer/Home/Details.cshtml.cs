using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tabii.DataAccess.Repository.IRepository;
using Tabii.Models;

namespace TabiiWeb.Pages.Customer.Home
{
    public class DetailsModel : PageModel
    {
		private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public MenuItem MenuItem  { get; set; }
        public int Count { get; set; }
        public void OnGet(int id)
        {
            MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id, includeProperties:"Category,FoodType");
        }
    }
}
