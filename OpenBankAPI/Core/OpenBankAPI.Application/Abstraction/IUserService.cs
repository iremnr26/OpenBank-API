﻿using System;
using Domain.Entities;
using Domain.Entities.Identity;
using OpenBankAPI.Application.DTOs.User;

namespace OpenBankAPI.Application.Abstraction
{
	public interface IUserService
	{
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<List<ListUser>> GetAllUsersAsync(int page, int size);
        int TotalUsersCount { get; }
        Task AssignRoleToUserAsnyc(string userId, string[] roles);
        Task<string[]> GetRolesToUserAsync(string userIdOrName);
        Task<bool> HasRolePermissionToEndpointAsync(string name, string code);
    }
}

