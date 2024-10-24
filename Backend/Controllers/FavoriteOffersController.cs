using System.Security.Claims;
using JobBoard.Controllers;
using JobBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard{
    
[ApiController]
[Route("[controller]")]
public class FavoriteOffersController : ControllerBase
{
    private readonly JobBoardContext _context;

    public FavoriteOffersController(JobBoardContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpPost("addfavorite/{jobId}")]
    public async Task<IActionResult> AddFavorite(int jobId)
    {
        // Récupérer l'utilisateur connecté à partir du token JWT
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        // Vérifier si l'offre existe
        var job = await _context.Jobs.FindAsync(jobId);
        if (job == null)
        {
            return NotFound("Job not found.");
        }

        // Vérifier si l'offre est déjà dans les favoris de cet utilisateur
        var existingFavorite = await _context.FavoriteOffers
            .FirstOrDefaultAsync(f => f.UserId == userId && f.JobId == jobId);

        if (existingFavorite != null)
        {
            return BadRequest("Offer is already in favorites.");
        }

        // Ajouter l'offre aux favoris
        var favoriteOffer = new FavoriteOffer
        {
            UserId = userId,
            JobId = jobId
        };

        _context.FavoriteOffers.Add(favoriteOffer);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Offer added to favorites." });
    }

    [Authorize]
[HttpGet("favorite-offers")]
public async Task<IActionResult> GetFavoriteOffers()
{
    // Récupérer l'utilisateur actuellement connecté
    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    // Rechercher les offres favorites pour cet utilisateur
    var user = await _context.Users
        .Include(u => u.FavoriteOffers)
        .ThenInclude(f => f.Job)  // Inclure les détails du job
        .FirstOrDefaultAsync(u => u.Id == userId);

    if (user == null)
    {
        return NotFound("User not found.");
    }

    // Retourner la liste des offres favorites
    var favoriteOffers = user.FavoriteOffers.Select(f => new
    {
        f.Job.Id,
        f.Job.Title,
        f.Job.CompanyName,
        f.Job.Location,
        f.Job.Salary,
        f.Job.PostedDate
    }).ToList();

    return Ok(new { offers = favoriteOffers });
}

[Authorize]
[HttpDelete("remove-favorite/{jobId}")]
public async Task<IActionResult> RemoveFavoriteOffer(int jobId)
{
    // Récupérer l'utilisateur actuellement connecté
    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    // Rechercher l'utilisateur et son offre favorite associée
    var user = await _context.Users
        .Include(u => u.FavoriteOffers)
        .FirstOrDefaultAsync(u => u.Id == userId);

    if (user == null)
    {
        return NotFound("User not found.");
    }

    // Rechercher l'offre favorite à supprimer
    var favoriteOffer = user.FavoriteOffers.FirstOrDefault(f => f.JobId == jobId);

    if (favoriteOffer == null)
    {
        return NotFound("Favorite offer not found.");
    }

    // Supprimer l'offre favorite
    user.FavoriteOffers.Remove(favoriteOffer);
    await _context.SaveChangesAsync();

    return Ok(new { message = "Offer removed from favorites." });
}



}
}