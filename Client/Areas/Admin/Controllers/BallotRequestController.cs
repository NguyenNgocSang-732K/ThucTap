using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Models.Entities;
using Client.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supports;

namespace Client.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/ballotrequest")]
    public class BallotRequestController : Controller
    {
        private IBallotRequestRepository ballotRequestRepository;
        public BallotRequestController(IBallotRequestRepository _ballotRequestRepository)
        {
            ballotRequestRepository = _ballotRequestRepository;
        }

        [Authorize(Roles = "1", AuthenticationSchemes = "SCHEME_ADMIN")]
        [Route("index")]
        [Route("")]
        public IActionResult Index()
        {
            string strPage = HttpContext.Request.Query["page"].ToString();
            int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            Dictionary<int, List<BallotRequest>> dic = ballotRequestRepository.GetData(page, ConstantCuaSang.size, CookieCuaSang.Get(HttpContext, "token"));
            ViewBag.Count = dic.Keys.ElementAt(0);
            ViewBag.Data = dic.Values.ElementAt(0);
            return View();
        }

        [Authorize(Roles = "1", AuthenticationSchemes = "SCHEME_ADMIN")]
        [HttpGet("detail/{id}")]
        public IActionResult Details(int id)
        {
            BallotRequest ballotRequest = ballotRequestRepository.GetById(id, CookieCuaSang.Get(HttpContext, "token"));
            return View(ballotRequest);
        }

        [Authorize(Roles = "1", AuthenticationSchemes = "SCHEME_ADMIN")]
        [HttpPost("setstatus")]
        public IActionResult SetStatus(BallotRequest ballotRequest)
        {
            ballotRequest.Status = 1;
            return Json(ballotRequestRepository.SetStatus(ballotRequest, CookieCuaSang.Get(HttpContext, "token")) ? "200" : "500");
            //set status thành đã duyệt return 200
        }

        [Authorize(Roles = "1", AuthenticationSchemes = "SCHEME_ADMIN")]
        [HttpPost("remove")]
        public IActionResult Remove(BallotRequest ballotRequest)
        {
            return Json(ballotRequestRepository.Remove(ballotRequest.Id, CookieCuaSang.Get(HttpContext, "token")) ? "200" : "500");
            //set active = false return 200;
        }
    }
}