﻿namespace Hospital_System.Models.DTOs.User
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public IList<string> Roles { get; set; }
    }
}


