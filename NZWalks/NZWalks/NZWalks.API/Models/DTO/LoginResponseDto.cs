﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class LoginResponseDto
    {
        public string JWTToken { get; set; }
    }
}
