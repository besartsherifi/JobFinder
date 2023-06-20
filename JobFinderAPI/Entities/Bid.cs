﻿using System.ComponentModel.DataAnnotations;

namespace JwtAuthAspNet7WebAPI.Entities;

public class Bid
{
    [Key]
    public int Id { get; set; }
    
    public double Amount { get; set; }
    
    public int WorkId { get; set; }
    public Work Work { get; set; } = null!;
    
    public string? SeekerId { get; set; }
    public ApplicationUser Seeker { get; set; } = null!;

}