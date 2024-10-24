using Microsoft.AspNetCore.Mvc;
using JobBoard.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Claims;

namespace JobBoard.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CandidatsController:ControllerBase
    {
        private readonly JobBoardContext _context;

        public CandidatsController(JobBoardContext context)
        {
            _context = context;
        }

        [HttpPost("ApplyJob/{id}")]
        public async Task<IActionResult> ApplyJob(int id, [FromBody] Candidature candidature){

            var nameIdentifierClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (nameIdentifierClaim != null){
                var userId = int.Parse(nameIdentifierClaim);
            var user = _context.Users
                       .Include(u => u.Candidat)
                       .FirstOrDefault(u => u.Id == userId);
            var candidatId = user.Candidat.Id;

            if (candidatId == null)
        {
            return NotFound(new { success = false, message = "Candidat non trouvé." });
        }

        // Vérifier si le job existe
        var job = await _context.Jobs.FindAsync(id);
        if (job == null)
        {
           return NotFound(new { success = false, message = "Job non trouvé." });
        }

        // Vérifier si le candidat a déjà postulé
        var existingApplication = await _context.Candidatures
            .FirstOrDefaultAsync(j => j.CandidatId == candidatId && j.JobId == id);

        if (existingApplication != null)
        {
           return BadRequest(new { success = false, message = "Vous avez déjà postulé à ce job." });
        }

        candidature.CandidatId = candidatId;
        candidature.JobId = id;

        // Ajouter la candidature à la base de données
        _context.Candidatures.Add(candidature);
        await _context.SaveChangesAsync();

            }else{
                var candidat = new Candidat();
                _context.Candidats.Add(candidat);
                await _context.SaveChangesAsync();
                candidature.CandidatId = candidat.Id;
                _context.Candidatures.Add(candidature);
                await _context.SaveChangesAsync();
            }
            
         return Ok(new { success = true, message = "Application submitted successfully." });

        }

      [Authorize]
[HttpGet("GetAllHistorics")]
public async Task<IActionResult> GetAllHistorics()
{
    // Récupère l'utilisateur connecté
    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    // Récupère l'utilisateur avec ses offres favorites et les informations du candidat
    var user = await _context.Users
        .Include(u => u.Candidat)         // Inclure les informations du candidat
        .Include(u => u.FavoriteOffers)   // Inclure les offres favorites
        .FirstOrDefaultAsync(u => u.Id == userId);

    // Vérifie si l'utilisateur est un candidat valide
    if (user == null || user.Candidat == null)
    {
        return NotFound("Candidat non trouvé");
    }

    // Compte le nombre de candidatures pour ce candidat
    var applicationsCount = await _context.Candidatures
        .Where(c => c.CandidatId == user.Candidat.Id)
        .CountAsync();

    // Compte le nombre d'offres favorites
    var favoriteOffersCount = user.FavoriteOffers.Count;

    // Retourne les deux valeurs dans la réponse
    return Ok(new { applicationsCount, favoriteOffersCount });
}

    }
    
}
