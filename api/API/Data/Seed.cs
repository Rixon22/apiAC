using System;
using System.Security.Cryptography;
using System.Text.Json;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(AppDbContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var seedUsersData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var seedUsers = JsonSerializer.Deserialize<List<SeedUserDto>>(seedUsersData);

        if (seedUsers == null)
        {
            Console.WriteLine("No users to seed.");
            return;
        }

        using var hmac = new HMACSHA512();

        foreach (var seedUser in seedUsers)
        {
            var user = new AppUser
            {
                Id = seedUser.Id,
                Email = seedUser.Email,
                DisplayName = seedUser.DisplayName,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password")),
                PasswordSalt = hmac.Key,
                Member = new Member
                {
                    Id = seedUser.Id,
                    DisplayName = seedUser.DisplayName,
                    Gender = seedUser.Gender,
                    City = seedUser.City,
                    Country = seedUser.Country,
                    Description = seedUser.Description,
                    BirthDate = seedUser.BirthDate,
                    ImageUrl = seedUser.ImageUrl,
                    LastActive = seedUser.LastActive,
                    Created = seedUser.Created
                }
            };

            user.Member.Photos.Add(new Photo
            {
                ImageUrl = seedUser.ImageUrl!,
                MemberId = seedUser.Id
            });

            context.Users.Add(user);
        }
        await context.SaveChangesAsync();
    }
}
