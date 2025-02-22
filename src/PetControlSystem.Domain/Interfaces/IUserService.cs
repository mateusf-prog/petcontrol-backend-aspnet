﻿using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task Add(User user);
        Task Login(User user);
        Task Update(User user);
        Task UpdatePassword(User user, string newPassword);
    }
}
