using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tabii.DataAccess.Repository.IRepository;
using Tabii.Utilities;
using System.IO;
using System.Linq;

namespace TabiiWeb.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Authorize]
        public IActionResult Get(string? status=null)
        {
            var OrderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties:"ApplicationUser");
            if (status == "cancelled")
            {
                OrderHeaderList = OrderHeaderList.Where(u => u.Status == SD.StatusCancelled || u.Status == SD.StatusRejected).ToList();
            }
            else
            {
                if (status == "completed")
                {
                    OrderHeaderList = OrderHeaderList.Where(u => u.Status == SD.StatusCompleted || u.Status == SD.StatusRefunded).ToList();
                }
                else
                {
                    if (status == "ready")
                    {
                        OrderHeaderList = OrderHeaderList.Where(u => u.Status == SD.StatusReady).ToList();
                    }
                    else
                    {
                        if (status == "inprocess")
                        {
                            OrderHeaderList = OrderHeaderList.Where(u => u.Status == SD.StatusSubmitted || u.Status == SD.StatusInProcess || u.Status == SD.StatusPending).ToList();
                        }
                        else
                        {
                            OrderHeaderList = OrderHeaderList;
                        }
                    }
                }
            }
            return Json(new { data = OrderHeaderList });
        }
        
    }
}
