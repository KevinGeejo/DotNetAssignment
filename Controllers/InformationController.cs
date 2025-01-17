﻿using Microsoft.AspNetCore.Mvc;
using PersonalAPI.Helper;
using PersonalAPI.Models;
using PersonalAPI.DAO;

namespace PersonalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        public readonly IPersonalDao _personalInfoDao;

        public InformationController(IPersonalDao personalInfoDao)
        {
            _personalInfoDao = personalInfoDao;
        }


        // GET: PersonalInfoController
        [HttpGet]
        public async Task<ActionResult<List<PersonalInformationData>>> Index()
        {
            var baseUri = $"{Request.Scheme}://{Request.Host}/";
            List<PersonalInformationData> players = await _personalInfoDao.GetAllPersonalInfo(baseUri);
            if (players != null)
            {
                return Ok(players);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<int>> UploadFile([FromForm] InsertPersonalDetails personal)
        {
            if (personal != null)
            {
                string imageName = new UploadHandler().Upload(personal.ImageFile);
                Console.Write(imageName);
                int res = await _personalInfoDao.InsertPersonalDetails(personal, imageName);
                if (res > 0)
                {
                    return Ok(res);
                }
                return BadRequest("Failed to add player");
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
