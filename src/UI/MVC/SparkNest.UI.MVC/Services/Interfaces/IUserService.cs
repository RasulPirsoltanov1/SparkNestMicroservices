﻿using SparkNest.UI.MVC.Models;

namespace SparkNest.UI.MVC.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserVM> GetUser();
    }
}
