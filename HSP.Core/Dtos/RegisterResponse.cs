﻿using HSP.Core.Jwt;

namespace HSP.Core.Dtos;

public class RegisterResponse
{
    public int Id { get; set; }
    public string Email { get; set; }

    public string FullName { get; set; }
    public TokenResult? Token { get; set; }
}
