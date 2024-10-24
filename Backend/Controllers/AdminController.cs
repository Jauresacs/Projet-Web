using Microsoft.AspNetCore.Mvc;
using JobBoard.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JobBoard{

    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase{
        private readonly JobBoardContext _context;

        
        public AdminController(JobBoardContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
public async Task<IActionResult> GetAllUsers(string type)
{
    IQueryable<User> usersQuery = _context.Users;

    if (!string.IsNullOrEmpty(type))
    {
        // Filtrer les utilisateurs en fonction du rôle
        usersQuery = usersQuery.Where(u => u.Role == type);
    }

    var users = await usersQuery.ToListAsync();

    return Ok(users);
}
        [Authorize]
[HttpGet("GetCompanies")]
public async Task<IActionResult> GetCompanies()
{
    var companies = await _context.Entreprises
                                  .Include(e => e.Recruteurs)
                                  .ThenInclude(r => r.User)  // Inclure les utilisateurs associés aux recruteurs
                                  .Select(e => new
                                  {
                                      e.Id,
                                      e.Name,
                                      e.Description,
                                      e.SiteWeb,
                                      e.Adresse,
                                      Recruteurs = e.Recruteurs.Select(r => new
                                      {
                                          r.Id,
                                          r.Role,
                                          r.DateInscription,
                                          User = new
                                          {
                                              r.User.Id,
                                              r.User.Nom,
                                              r.User.Email
                                          }
                                      }).ToList()
                                  })
                                  .ToListAsync();

    return Ok(companies);  // Renvoie un tableau normal sans les métadonnées spéciales
}

[Authorize]
[HttpGet("GetCount")]
public async Task<IActionResult> GetCount(){

    var totalUsers = await _context.Users
                        .CountAsync();
    var totalJobOffers = await _context.Jobs
                            .CountAsync();

    return Ok(new { totalUsers, totalJobOffers});
}

[Authorize]
[HttpDelete("Delete/{id}")]
public async Task<IActionResult> DeleteJob(int id)
{
    var job = await _context.Jobs.FindAsync(id);

    if (job == null)
    {
        return NotFound("L'annonce n'existe pas.");
    }

    _context.Jobs.Remove(job);
    await _context.SaveChangesAsync();

    return Ok("L'annonce a été supprimée avec succès.");
}

[Authorize]
[HttpPost("addJob")]
public async Task<IActionResult> AddJob([FromBody] Job job)
{
    if (ModelState.IsValid)
    {
        // Récupérer l'ID de l'utilisateur connecté via les claims
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        // Vérifier si l'utilisateur existe
        var user = await _context.Users
                                 .Include(u => u.Admin)  // Inclure l'objet Admin si la relation existe
                                 .FirstOrDefaultAsync(u => u.Id == userId);
        
        if (user == null || user.Admin == null)  // S'assurer que l'utilisateur est un administrateur
        {
            return Unauthorized(new { message = "Utilisateur non autorisé à ajouter des annonces." });
        }

        // Ajouter l'annonce avec l'ID du recruteur (admin)
        job.RecruteurId = user.Admin.Id;
        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        return Ok(new { message = "L'annonce a été ajoutée avec succès." });
    }

    return BadRequest(ModelState);
}
[Authorize]
[HttpDelete("delete-company/{id}")]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        // Récupérer l'entreprise par ID
        var company = await _context.Entreprises.Include(c => c.Recruteurs).FirstOrDefaultAsync(c => c.Id == id);

        // Vérification si l'entreprise existe
        if (company == null)
        {
            return NotFound(new { message = "Entreprise non trouvée" });
        }

        // Suppression des recruteurs associés
        _context.Recruteurs.RemoveRange(company.Recruteurs);

        // Suppression de l'entreprise
        _context.Entreprises.Remove(company);
        
        // Sauvegarder les changements dans la base de données
        await _context.SaveChangesAsync();

        return Ok(new { message = "Entreprise supprimée avec succès" });
    }


    [Authorize]
    [HttpDelete("delete-user/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        // Récupérer l'utilisateur par ID
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        // Vérification si l'utilisateur existe
        if (user == null)
        {
            return NotFound(new { message = "Utilisateur non trouvé" });
        }

        // Suppression de l'utilisateur
        _context.Users.Remove(user);

        // Sauvegarder les changements dans la base de données
        await _context.SaveChangesAsync();

        return Ok(new { message = "Utilisateur supprimé avec succès" });
    }






    }
}