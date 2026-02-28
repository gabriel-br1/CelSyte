using System;
using CelSyte.Data;
using CelSyte.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Konscious.Security.Cryptography;
using System.Text;

namespace CelSyte.Service
{
    public class UserService
    {

        private readonly ProtectedLocalStorage _protectedLocalStorage;

        private readonly string _celsyteIdentityKey = "celsyteIdentity";

        private const int SaltLength = 16; 

        public UserService(ProtectedLocalStorage protectedLocalStorage)
        {
            _protectedLocalStorage = protectedLocalStorage;
        }

        public User? FindUserInDatabase(string email, string password, CelSyteContext context)
        {
            List<User> users = fetchUsers(context);
            string hashedRequestPassword = hashPassword(password);

            foreach (User user in users)
            {
                if(user.Email.Equals(email) && user.Password.Equals(hashedRequestPassword))
                {
                    return user;
                }
            }
            return null;
        }

        public string hashPassword(string password)
        {
            using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                hasher.Salt = generateSalt(SaltLength);
                hasher.DegreeOfParallelism = 8;
                hasher.MemorySize = 65536;
                hasher.Iterations = 4;
                byte[] bytes = hasher.GetBytes(32);
                return Encoding.UTF8.GetString(bytes);
            }
        }

        private static byte[] generateSalt(int length)
        {
            byte[] salt = new byte[length];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public async Task PersistUserToBrowserAsync(User user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            await _protectedLocalStorage.SetAsync(_celsyteIdentityKey, userJson);
        }

        public async Task<User?> FetchUserFromBrowserAsync()
        {
            try
            {
                var storedUserJson = await _protectedLocalStorage.GetAsync<string>(_celsyteIdentityKey);

                if (storedUserJson.Success && !string.IsNullOrEmpty(storedUserJson.Value))
                {
                    User user = JsonConvert.DeserializeObject<User>(storedUserJson.Value);

                    return user;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task ClearBrowserDataAsync()
        {
            await _protectedLocalStorage.DeleteAsync(_celsyteIdentityKey);
        }

        private List<User> fetchUsers(CelSyteContext context)
        {

            using (context)
            {
                string connString = context.Database.GetConnectionString();
                
                string query = "SELECT * FROM \"User\"";
                List<User> allUsers = new List<User>();

                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    using var cmd = new SqlCommand(query, connection);
                    using var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        allUsers.Add(new User()
                        {
                            Id = (int)reader["Id"],
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString()
                        });
                    }

                    connection.Close();
                }
                return allUsers;
            }
        }

    }
}