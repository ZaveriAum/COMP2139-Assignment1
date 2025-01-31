﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using COMP2139_Assignment1.Areas.NorthPole.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace COMP2139_Assignment1.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<NorthPoleUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<NorthPoleUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            _logger.LogInformation("Logout for the loged in user.");
            try
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");
                if (returnUrl != null)
                {
                    return RedirectToPage("~/Index");
                }
                else
                {
                    // This needs to be a redirect so that the browser performs a new
                    // request and the identity for the user gets updated.
                    return RedirectToPage("~/Index");
                }
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToPage("~/Index");
            }
        }
    }
}
