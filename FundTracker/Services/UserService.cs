﻿using FundTracker.Abstraction;
using FundTracker.Model;
using FundTracker.Services.Interface;

namespace FundTracker.Services
{
    public class UserService : UserBase, IUser
    {
        private List<User> _users;

        public const string SeedUsername = "admin";
        public const string SeedPassword = "password";

        public UserService()
        {
            _users = LoadUsers();

            if (!_users.Any())
            {
                _users.Add(new User { UserName = SeedUsername, Password = SeedPassword });
                SaveUsers(_users);
            }

        }
        public bool Login(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }

            //check if the username and password match any user in the list 
            return _users.Any(u => u.UserName == user.UserName && u.Password == user.Password);
            //throw new NotImplementedException();
        }
    }
}
