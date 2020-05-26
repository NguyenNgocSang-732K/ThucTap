using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Entities;
using Services.Models.Interfaces;

namespace Services.Controllers
{
    [Route("api/ballot")]
    [ApiController]
    public class BallotRestController : ControllerBase
    {
        private IBallotRequestRepository ballotRequestRepository;
        public BallotRestController(IBallotRequestRepository _ballotRequestRepository)
        {
            ballotRequestRepository = _ballotRequestRepository;
        }


        [Authorize]
        [HttpGet("getdata/{page}/{rows}")]
        public async Task<IActionResult> GetData(int page, int rows)
        {
            try
            {
                IEnumerable<BallotRequest> list = await ballotRequestRepository.GetData(page, rows);
                int count = await ballotRequestRepository.GetRowCount();
                return Ok(new { data = list, count = count });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Authorize]
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                BallotRequest ballotRequest = await ballotRequestRepository.GetById(id);
                if (ballotRequest == null) { return NotFound(); }
                return Ok(ballotRequest);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("getbyuserid/{userid}")]
        public async Task<IActionResult> GetByUserId(int userid)
        {
            try
            {
                return Ok(await ballotRequestRepository.GetByIdUser(userid));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] BallotRequest ballotRequest)
        {
            try
            {
                return Ok(await ballotRequestRepository.Create(ballotRequest));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("modify")]
        public async Task<IActionResult> Modify([FromBody] BallotRequest ballotRequest)
        {
            try
            {
                return Ok(await ballotRequestRepository.Modify(ballotRequest));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("setstatus")]
        public async Task<IActionResult> SetStatus([FromBody] BallotRequest ballotRequest)
        {
            try
            {
                return Ok(await ballotRequestRepository.SetStatus(ballotRequest.Id, ballotRequest.Status));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                return Ok(await ballotRequestRepository.Remove(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}