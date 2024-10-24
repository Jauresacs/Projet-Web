
using Microsoft.AspNetCore.Mvc;
using JobBoard.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobBoard.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly JobBoardContext _context;

        public UsersController(JobBoardContext context)
        {
            _context = context;
        }

        //Hachage du mot de passe 
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                 var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                 if (existingUser!= null){
                    return BadRequest( new { message = "Cet email est déjà enregistré."});
                 }
                // Hachage du mot de passe
                var userService = new UserServices();
                user.PasswordHash = userService.HashPassword(user, user.PasswordHash);

                // Ajout de l'utilisateur
                _context.Add(user);
                await _context.SaveChangesAsync();
                var tokenHandler = new JwtSecurityTokenHandler(); 
                var key = Encoding.UTF8.GetBytes("t8Zul9YJGeGTuXcApJnphyMsqpI8mfht");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = "http://localhost:5027",
                    Audience = "http://localhost:5027",
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var entreprise = new Entreprise
                {
                    Name = "Entreprise à compléter",
                    Description = "Description à compléter",
                    SiteWeb = "N/A",
                    Adresse = "Adresse à compléter"
                };
                _context.Entreprises.Add(entreprise);
                await _context.SaveChangesAsync();

                if (user.Role == "recruiter")
                {
                    var recruteur = new Recruteur
                    {
                        UserId = user.Id,
                        EntrepriseId = entreprise.Id,
                    };
                    _context.Add(recruteur);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var candidat = new Candidat
                    {
                        UserId = user.Id
                    };
                    _context.Add(candidat);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { Token = tokenString , Role = user.Role, Email = user.Email, Name = user.Nom});
            }

            return BadRequest(ModelState); // Renvoie les erreurs de validation en JSON
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginRequest.Email);

            if (user == null)
            {
                return Unauthorized(new { message = "Email ou mot de passe incorrect" });
            }

            var userService = new UserServices();
            if (userService.VerifyPassword(user, user.PasswordHash, loginRequest.Password))
            {
                var tokenHandler = new JwtSecurityTokenHandler(); 
                var key = Encoding.UTF8.GetBytes("t8Zul9YJGeGTuXcApJnphyMsqpI8mfht");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = "http://localhost:5027",
                    Audience = "http://localhost:5027",
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString , Role = user.Role, Email = user.Email, Name = user.Nom});

            }
            else
            {
                return Unauthorized(new { message = "Email ou mot de passe incorrect" });
            }
        }

        [Authorize]
        [HttpPut("EditInformation")]
        public async Task<IActionResult> EditInformation([FromBody] Dictionary<string, string> updates){
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.Users.FindAsync(int.Parse(userId));

            if (updates.ContainsKey("nom")) user.Nom = updates["nom"];
            if (updates.ContainsKey("email")) user.Email = updates["email"];

            await _context.SaveChangesAsync();

            return Ok(new {message = "Profil mis a jour"});
        }

        [Authorize]
        [HttpPut("EditPassword")]
        public async Task<IActionResult> EditPassword([FromBody] Dictionary<string, string> updates ){

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.Users.FindAsync(int.Parse(userId));
            var currentPassword = updates["oldPassword"];
            var newPassword = updates["newPassword"];
            var confirmPassword = updates["confirmPassword"];

            var userService = new UserServices(); // Utilise ton service de vérification de mot de passe
            if (!userService.VerifyPassword(user, user.PasswordHash, currentPassword)){
                    return BadRequest(new { message = "Le mot de passe actuel est incorrect." });
                }
             if (newPassword != confirmPassword){
                return BadRequest(new { message = "Le nouveau mot de passe et la confirmation ne correspondent pas." });
                }

             user.PasswordHash = userService.HashPassword(user, newPassword);
             _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Mot de passe modifié avec succès." });

            } 



        [Authorize]
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] User user)
        {
            // Vérifie que l'utilisateur est bien celui qu'il prétend être
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null || id != int.Parse(userId))
            {
                return Unauthorized(new { message = "Vous n'êtes pas autorisé à modifier cet utilisateur." });
            }

            // Vérifie que l'ID de l'utilisateur passé dans le corps de la requête correspond à celui de l'URL
            if (id != int.Parse(userId))
            {
                return BadRequest(new { message = "ID incorrect" });
            }

            // Récupère l'utilisateur existant dans la base de données
            var userInDb = await _context.Users.FindAsync(id);
            if (userInDb == null)
            {
                return NotFound(new { message = "Utilisateur non trouvé" });
            }

            // Met à jour les champs modifiables
            userInDb.Nom = user.Nom ?? userInDb.Nom; // Met à jour le nom seulement si une nouvelle valeur est fournie
            userInDb.Email = user.Email ?? userInDb.Email; // Met à jour l'email seulement si une nouvelle valeur est fournie

            // Si un nouveau mot de passe est fourni, le hacher et le mettre à jour
            if (!string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                var userService = new UserServices();
                userInDb.PasswordHash = userService.HashPassword(user, user.PasswordHash);
            }

            // Sauvegarde les changements dans la base de données
            try
            {
                _context.Users.Update(userInDb);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Informations mises à jour avec succès", user = userInDb });
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { message = "Erreur lors de la mise à jour des informations. Veuillez réessayer." });
            }
        }

        [Authorize]
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(){

             var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

             var user = await _context.Users.FindAsync(userId);
             if (user == null){
                return NotFound(new {message = "Error"});
             }
            return Ok(new {user.Email , user.Nom});
        }

    }
}