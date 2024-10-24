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
    public class RecruteursController : ControllerBase
    {
        private readonly JobBoardContext _context;

        public RecruteursController(JobBoardContext context)
        {
            _context = context;
        }


        public IActionResult AddJob()
        {
            return Ok();
        }

        [Authorize]
        [HttpPost("SetEntreprise")]
        public async Task<IActionResult> SetEntreprise([FromBody] Entreprise entreprise){
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = _context.Users
                        .Include(u => u.Recruteur)
                        .FirstOrDefault(u => u.Id == userId);
            var entrepriseId = user.Recruteur.EntrepriseId;
            var e = await _context.Entreprises.FindAsync(entrepriseId);
            e.Name = entreprise.Name;
            e.Description = entreprise.Description;
            e.SiteWeb = entreprise.SiteWeb;
            e.Adresse = entreprise.Adresse;

            await _context.SaveChangesAsync();   

            return Ok(new {message = "Information entreprise rajoutée"});
        }

        [Authorize]
        [HttpPut("Edit")]
        public async Task<IActionResult> EditPassword([FromBody] Dictionary<string, string> updates ){

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.Users.FindAsync(int.Parse(userId));
            var userEmail = updates["email"];
            var newPassword = updates["newPassword"];
            var confirmPassword = updates["confirmPassword"];
            user.Email = userEmail;

            var userService = new UserServices(); // Utilise ton service de vérification de mot de passe
             if (newPassword != confirmPassword){
                return BadRequest(new { message = "Le nouveau mot de passe et la confirmation ne correspondent pas." });
                }

             user.PasswordHash = userService.HashPassword(user, newPassword);
             _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Mot de passe modifié avec succès." });

            }
        
        [Authorize]
        [HttpPost("addJob")]
        public async Task<IActionResult> AddJob([FromBody] Job job)
        {
           if (ModelState.IsValid){
             var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = _context.Users
                       .Include(u => u.Recruteur)
                       .FirstOrDefault(u => u.Id == userId);
            var recruteurId = user.Recruteur.Id;
        

            if (recruteurId == null)
            {
                return Unauthorized(new { message = "Recruteur non authentifié" });
            }
            var recruteur = await _context.Recruteurs.FindAsync(recruteurId);
            if (recruteur == null)
            {
                return NotFound(new { message = "Recruteur non trouvé" });
            }

            job.RecruteurId = recruteur.Id;
            _context.Add(job);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Job rajouté avec succès" });
           }
           return BadRequest(ModelState);
        }

        [Authorize] 
        [HttpGet("GetInformations")]
        public IActionResult GetInformations()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var user = _context.Users
                .Include(u => u.Recruteur)
                    .ThenInclude(r => r.Entreprise) // Inclus explicitement l'entreprise du recruteur
                .FirstOrDefault(u => u.Id == userId);

            if (user == null || user.Recruteur == null || user.Recruteur.Entreprise == null)
            {
                return NotFound(new { message = "Recruteur ou entreprise introuvable." });
            }

            return Ok(new
            {
                user.Nom,
                user.Email,
                EntrepriseName = user.Recruteur.Entreprise.Name
            });
        }

        [Authorize]
        [HttpGet("getPostedJobs")]
        public async Task<IActionResult> GetPostedJobs(){

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var recruteur = await _context.Users
        .Include(u => u.Recruteur)
        .ThenInclude(r => r.Jobs)
        .FirstOrDefaultAsync(u => u.Id == userId);

        if (recruteur == null || recruteur.Recruteur == null){
            return NotFound("Recruteur non trouvé");
        }

        var jobs = recruteur.Recruteur.Jobs.Select(j => new {
            j.Id,
            j.Title,
            j.CompanyName,
            j.Location,
            j.Salary,
            j.Category,
            j.JobType,
            j.Description
        }).ToList();

        return Ok(jobs);
    }

    [Authorize]
[HttpDelete("DeleteJob/{jobId}")]
public async Task<IActionResult> DeleteJobOffer(int jobId)
{
    // Récupérer l'utilisateur connecté via le JWT
    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    // Rechercher l'utilisateur avec les informations du recruteur
    var recruteur = await _context.Users
        .Include(u => u.Recruteur)
        .FirstOrDefaultAsync(u => u.Id == userId);

    if (recruteur == null || recruteur.Recruteur == null)
    {
        return NotFound("Recruteur non trouvé");
    }

    // Rechercher l'offre d'emploi avec l'ID donné
    var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == jobId && j.RecruteurId == recruteur.Recruteur.Id);

    if (job == null)
    {
        return NotFound("Offre d'emploi non trouvée ou vous n'avez pas l'autorisation de la supprimer.");
    }

    // Supprimer l'offre d'emploi
    _context.Jobs.Remove(job);
    await _context.SaveChangesAsync();

    return Ok(new { message = "Offre d'emploi supprimée avec succès." });
}

[Authorize]
[HttpPut("updateJob/{jobId}")]
public async Task<IActionResult> UpdateJobOffer(int jobId, [FromBody] Job updatedJob)
{
    // Récupérer l'utilisateur connecté
    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    var recruteur = await _context.Users
        .Include(u => u.Recruteur)
        .FirstOrDefaultAsync(u => u.Id == userId);

    if (recruteur == null || recruteur.Recruteur == null)
    {
        return NotFound("Recruteur non trouvé");
    }

    // Rechercher l'offre existante pour ce recruteur
    var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == jobId && j.RecruteurId == recruteur.Recruteur.Id);

    if (job == null)
    {
        return NotFound("Offre d'emploi non trouvée ou vous n'avez pas l'autorisation de la mettre à jour.");
    }

    // Mise à jour des détails de l'offre
    job.Title = updatedJob.Title;
    job.CompanyName = updatedJob.CompanyName;
    job.Location = updatedJob.Location;
    job.Salary = updatedJob.Salary;
    job.Category = updatedJob.Category;
    job.JobType = updatedJob.JobType;
    job.Description = updatedJob.Description;

    // Sauvegarder les changements
    await _context.SaveChangesAsync();

    return Ok(new { message = "Offre mise à jour avec succès.", job });
}

    }
}
