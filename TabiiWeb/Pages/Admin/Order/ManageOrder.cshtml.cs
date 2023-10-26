using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tabii.DataAccess.Repository.IRepository;
using Tabii.Models;
using Tabii.Models.ViewModel;
using System.Linq;
using Tabii.Utilities;

namespace TabiiWeb.Pages.Admin.Order
{
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public List<OrderDetailsVM> OrderDetailsVM { get; set; }
        public ManageOrderModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            OrderDetailsVM = new();

            List<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(u=>u.Status == SD.StatusSubmitted ||  u.Status == SD.StatusInProcess).ToList();

            foreach(OrderHeader item in orderHeaders) 
            {
                OrderDetailsVM individual = new OrderDetailsVM()
                {
                    OrderHeader = item,
                    OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == item.Id).ToList()
                };
            }
        }
    }
}
