using System.Security.Claims;
using JobBoard.Controllers;
using JobBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Models{
    public class FavoriteOffer{
    public int UserId { get; set; }
    public User User { get; set; }

    public int JobId { get; set; }
    public Job Job { get; set; }
}

    
}