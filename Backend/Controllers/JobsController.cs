using Microsoft.AspNetCore.Mvc;
using JobBoard.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Claims;

namespace JobBoard{
    
    [Route("[controller]")]
    [ApiController]
    public class JobsController : ControllerBase {
        private readonly JobBoardContext _context;

             public JobsController(JobBoardContext context)
        {
            _context = context;
        }



        [HttpGet("GetAllOffers")]
        public async Task<IActionResult> GetAllOffers(){
            var jobs = await _context.Jobs.ToListAsync();
            return Ok(jobs);
        }

        [HttpGet("GetJobFilters")]
public async Task<IActionResult> GetJobFilters()
{
    var categories = await _context.Jobs
                                   .Select(j => j.Category)
                                   .Distinct()
                                   .ToListAsync();

    var jobTypes = await _context.Jobs
                                 .Select(j => j.JobType)
                                 .Distinct()
                                 .ToListAsync();

     var company = await _context.Jobs
                                 .Select(j => j.CompanyName)
                                 .Distinct()
                                 .ToListAsync();

    return Ok(new { categories, jobTypes, company });
}


            }
    
}