namespace Settings;

using Microsoft.Extensions.Configuration;
using System;

public class LoginParameters : Parameters
{
    public string url { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string healthworker { get; set; }
    public string healthworkerpassword { get; set; }

    public LoginParameters()
    {
        url = configuration.GetSection("LoginParameters:url").Get<string>();
        username = configuration.GetSection("LoginParameters:username").Get<string>();
        password = configuration.GetSection("LoginParameters:password").Get<string>();

        if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            throw new Exception("Login parameters are not set in appsettings.json file");
    }
}