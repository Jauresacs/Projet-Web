using System.Security.Claims;
using JobBoard.Controllers;
using JobBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard{

    [Route("[controller]")]
    [ApiController]
    public class CandidaturesController: ControllerBase
    {
        private readonly JobBoardContext _context;

        public CandidaturesController(JobBoardContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("GetCandidatures")]
        public async Task<IActionResult> GetCandidatures()
        {
             // Récupérer l'ID de l'utilisateur connecté depuis les claims
             var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                {
                     return Unauthorized();
                }

            // Trouver l'utilisateur associé à cet ID
            var user = await _context.Users
                             .Include(u => u.Candidat) // Inclure les informations du candidat si nécessaire
                             .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

            if (user == null || user.Candidat == null)
                {
                    return NotFound(new { message = "Candidat non trouvé" });
                }

            // Récupérer toutes les candidatures associées à ce candidat
            var candidatures = await _context.Candidatures
                                     .Where(c => c.CandidatId == user.Candidat.Id)
                                     .Select(c => c.JobId)
                                     .ToListAsync();

            var jobs = await _context.Jobs
                             .Where(j => candidatures.Contains(j.Id)) // Filtrer les jobs par JobId
                             .ToListAsync();

             var result = jobs.Select(job => new {
                job.Title,
                job.CompanyName,
                job.Location,
                job.Salary,
                job.PostedDate,
                job.Description
            });

            // Renvoyer les candidatures dans la réponse
            return Ok(result);
        }

       [Authorize]
[HttpGet("candidates")]
public async Task<IActionResult> GetCandidates()
{
    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    // Trouver le recruteur connecté
    var recruteur = await _context.Users
        .Include(u => u.Recruteur)
        .FirstOrDefaultAsync(u => u.Id == userId);

    if (recruteur == null || recruteur.Recruteur == null)
    {
        return NotFound("Recruteur non trouvé.");
    }

    // Rechercher les candidats qui ont postulé aux offres du recruteur
    var candidates = await _context.Candidatures
        .Include(c => c.Candidat)  // Inclure les détails du candidat
        .Include(c => c.Job)       // Inclure les détails de l'offre d'emploi
        .Where(c => c.Job.RecruteurId == recruteur.Recruteur.Id)
        .Select(c => new 
        {
            Id = c.Candidat.Id,
            Name = c.ApplicantName,
            Email = c.ApplicantEmail,
            JobTitle = c.Job.Title,
            JobId = c.Job.Id
        })
        .ToListAsync();

    return Ok(candidates);
}

[Authorize]
[HttpGet("GetCandidateDetails/{id}")]
public async Task<IActionResult> GetCandidateDetails(int id)
{
    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    var user = await _context.Users
                             .Include(u => u.Recruteur)
                             .FirstOrDefaultAsync(u => u.Id == userId);

    // Assure-toi que le recruteur est trouvé
    if (user?.Recruteur == null)
    {
        return NotFound("Recruteur not found.");
    }

    var recruteurId = user.Recruteur.Id;
    var candidatures = await _context.Candidatures
        .Include(c => c.Candidat)
        .Include(c => c.Job)
            .ThenInclude(j => j.Recruteur)
        .Where(c => c.Candidat.Id == id && c.Job.RecruteurId == recruteurId)
        .ToListAsync();

    if (candidatures == null || candidatures.Count == 0)
    {
        return NotFound("Candidature not found.");
    }

    var result = candidatures.Select(c => new {
        c.ApplicantEmail,
        c.ApplicantMessage,
        c.ApplicantName,
        c.ApplicantNumber,
        JobDetails = new {
            c.JobId
        }
    });

    return Ok(result);
}


[Authorize]
[HttpGet("GetCandidateCount")]
public async Task<IActionResult> GetCandidateCount()
{
    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    var recruteur = await _context.Users
                                  .Include(u => u.Recruteur)
                                  .FirstOrDefaultAsync(u => u.Id == userId);

    if (recruteur == null || recruteur.Recruteur == null)
    {
        return NotFound("Recruteur non trouvé");
    }

    var candidateCount = await _context.Candidatures
                                       .Where(c => c.Job.RecruteurId == recruteur.Recruteur.Id)
                                       .CountAsync();

    return Ok(new { count = candidateCount });
}


[Authorize]
[HttpGet("GetAllHistorics")]

public async Task<IActionResult> GetAllHistorics(){
    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    var recruteur = await _context.Users
                                  .Include(u => u.Recruteur)
                                  .FirstOrDefaultAsync(u => u.Id == userId);

    if (recruteur == null || recruteur.Recruteur == null)
    {
        return NotFound("Recruteur non trouvé");
    }

     var receivedApplicationsCount = await _context.Candidatures
                                       .Where(c => c.Job.RecruteurId == recruteur.Recruteur.Id)
                                       .CountAsync();

    var postedOffersCount = await _context.Jobs
                                    .Where(j => j.RecruteurId == recruteur.Recruteur.Id)
                                    .CountAsync();
    
    return Ok (new {receivedApplicationsCount, postedOffersCount});
} 




        
        
    }

}