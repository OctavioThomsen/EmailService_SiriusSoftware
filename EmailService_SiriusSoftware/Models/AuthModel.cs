﻿namespace EmailService_SiriusSoftware.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
