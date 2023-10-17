using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tabii.DataAccess.Data;
using Tabii.DataAccess.Repository.IRepository;
using Tabii.Models;

namespace TabiiWeb.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class EditTypeModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public FoodType FoodType { get; set; }
        public EditTypeModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            FoodType = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.FoodType.Update(FoodType);
                _unitOfWork.Save();
                TempData["success"] = "Food Type created successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
