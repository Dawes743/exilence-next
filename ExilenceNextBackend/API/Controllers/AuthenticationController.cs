﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Entities;
using Shared.Enums;
using Shared.Interfaces;
using Shared.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IAccountService _accountService;
        private IMapper _mapper;
        private string _secret;
        private string _clientId;
        private string _clientSecret;

        public AuthenticationController(IMapper mapper, IAccountService accountRepository, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _mapper = mapper;
            _accountService = accountRepository;
            _secret = configuration.GetSection("Settings")["Secret"];
            _httpClientFactory = httpClientFactory;
            _clientId = configuration.GetSection("OAuth2")["ClientId"];
            _clientSecret = configuration.GetSection("OAuth2")["ClientSecret"];
        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Token([FromBody]AccountModel accountModel)
        {
            var account = await _accountService.GetAccount(accountModel.Name);

            if (account == null)
            {
                account = await _accountService.AddAccount(accountModel);
            }
            else
            {
                account = await _accountService.EditAccount(accountModel);
            }

            var token = AuthHelper.GenerateToken(_secret, account);

            return Ok(token);
        }

        [HttpGet]
        [Route("oauth2")]
        public async Task<IActionResult> OAuth2(string code)
        {
            string uri = "https://www.pathofexile.com/oauth/token";

            var client = _httpClientFactory.CreateClient();
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });
            var response = await client.PostAsync(uri, data);
            var contents = await response.Content.ReadAsStringAsync();

            return Ok(contents);
        }
    }
}