﻿using Transify.Models.Enums;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Transify.ViewModel.Authenticate;
using Transify.Interfaces;
using Transify.Models.Entities;
using Transify.Data;

namespace Transify.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticateService(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetUserSession(User user)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetInt32("UserId", user.UserId);
            session.SetString("FirstName", user.FirstName);
            session.SetString("UserRole", user.Role.ToString());
            session.SetString("IsAdmin", user.IsAdmin ? "true" : "false");
            session.SetString("BusinessType", user.BusinessType.ToString());

            if (user.CompanyId.HasValue)
            {
                session.SetInt32("CompanyId", user.CompanyId.Value);
            }
        }

        public void ClearUserSession()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Clear();
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserId");
        }

        public async Task<User> RegisterAsync(RegisterViewModel model)
        {
            var user = _mapper.Map<User>(model);
            user.PasswordHash = HashPassword(model.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            SetUserSession(user);

            return user;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _context.Users.AsNoTracking()
                                .SingleOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return null;

            if (user.PasswordHash != HashPassword(password))
                return null;

            SetUserSession(user);

            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> EditUserAsync(int userId, EditUserViewModel model)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            _mapper.Map(model, user);

            if (!string.IsNullOrEmpty(model.Password))
            {
                user.PasswordHash = HashPassword(model.Password);
            }

            if (model.Role.HasValue)
            {
                user.Role = model.Role.Value;
            }

            if (model.BusinessType.HasValue)
            {
                user.BusinessType = model.BusinessType.Value;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            user.IsDeleted = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestoreUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || !user.IsDeleted)
                return false;

            user.IsDeleted = false;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetCurrentUserAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public int? GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32("UserId");
        }

        public int? GetCurrentCompanyId()
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32("CompanyId");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}