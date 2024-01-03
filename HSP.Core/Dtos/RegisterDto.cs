﻿using HSP.Core.Jwt;

namespace HSP.Core.Dtos;

public class RegisterDto
{
    public string Email { get; set; }
    public string? Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
}
