using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tabii.DataAccess.Repository.IRepository;
using Tabii.Models;
using Tabii.Models.ViewModel;
using System.Linq;
using Tabii.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace TabiiWeb.Pages.Admin.Order
{
    [Authorize(Roles = $"{SD.ManageRole},{SD.KitchenRole}")]
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
                OrderDetailsVM.Add(individual);
            }
        }

        public IActionResult OnPostOrderInProcess(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusInProcess);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderReady(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusReady);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
        public IActionResult OnPostOrderCancel(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCancelled);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
    }
}
