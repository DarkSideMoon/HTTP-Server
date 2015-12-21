using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HttpServer.Library.Other;

namespace HttpServer.Library.ClientLogic
{
    public class User
    {
        private static List<User> _usersInSystem = new List<User>();
        private static List<User> _banUsers = new List<User>();

        private User _user;

        public User()
        {
        }

        public static User CurrentUser { get; set; }
        public TypeJsonRequest TypeRequest { get; set; }
        public Token MyToken { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }

        public List<User> Users
        {
            get { return _usersInSystem; }
        }

        public User ParseUser(string input)
        {
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string js = lines[13].Trim();
            dynamic json = (JArray)JsonConvert.DeserializeObject(js);

            // 5 items in array
            // Value
            // Name
            // [{"name":"name","value":"Pasha"},
            // {"name":"age","value":"19"},
            // {"name":"email","value":"shark00235@i.ua"},
            // {"name":"phone","value":"+380123122132"},
            // {"name":"password","value":"123456"}]

            // Parse json from registration form
            try
            {
                this._user = new User()
                {
                    Name = json[0]["value"],
                    Age = json[1]["value"],
                    Email = json[2]["value"],
                    Phone = json[3]["value"],
                    Password = json[4]["value"],

                    MyToken = new Token()
                };
                _usersInSystem.Add(this._user);
                CurrentUser = this._user;

                this.TypeRequest = TypeJsonRequest.Registration;
            }
            catch (ArgumentOutOfRangeException)
            {
                // Parse json from log in form
                this._user = new User()
                {
                    Email = json[0]["value"],
                    Password = json[1]["value"],

                    MyToken = new Token()
                };

                CurrentUser = this.FindUserByEmail(this._user.Email);
                this.TypeRequest = TypeJsonRequest.LogIn;
            }
            return this._user;
        }

        public bool LogIn(string input)
        {
            if (this.MyToken.TokenString == input)
            {
                return true;
            }

            foreach (var item in _usersInSystem)
                if (item.Name == input && item.Password == input)
                {
                    item.MyToken = new Token();
                    return true;
                }
            return false;
        }

        public bool IsExist()
        {
            bool res = false;
            //_usersInSystem.ForEach(user =>
            //{
            //});
            // null
            foreach (User user in _usersInSystem)
            {
                if (CurrentUser.Email == user.Email &&
                    CurrentUser.Password == user.Password &&
                     CurrentUser.Phone == user.Phone &&
                      CurrentUser.Name == user.Name)
                    res = true;
            }
            return res;
        }

        public User FindUserByEmail(string email)
        {
            foreach (User user in _usersInSystem)
                if (user.Email == email)
                    return new User()
                    {
                        Name = user.Name,
                        Age = user.Age,
                        Email = email,
                        Password = user.Password,
                        Phone = user.Phone,
                        MyToken = user.MyToken
                    };
            return new User();
        }
    }
}
