using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace HttpServer.Library.ClientLogic
{
    public class User
    {
        private static List<User> _usersInSystem = new List<User>();
        private static List<User> _banUsers = new List<User>();


        public User()
        {
        }

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

        public User RegistrationUser(string input)
        {
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string js = lines[13].Trim();

            string resultJs = js.Remove(0, 1).Remove(js.Length - 2, 1);
            dynamic json = (JArray)JsonConvert.DeserializeObject(js);

            // 5 items in array 
            // Value 
            // Name 
            // [{"name":"name","value":"Pasha"},
            // {"name":"age","value":"19"},
            // {"name":"email","value":"shark00235@i.ua"},
            // {"name":"phone","value":"+380123122132"},
            // {"name":"password","value":"123456"}]

            User user = new User()
            {
                Name = json[0]["value"],
                Age = json[1]["value"],
                Email = json[2]["value"],
                Phone = json[3]["value"],
                Password = json[4]["value"],
                
                MyToken = new Token()
            };
            _usersInSystem.Add(user);

            return user;
        }

        public bool LogIn(string input)
        {
            if(this.MyToken.TokenString == input)
                return true;

            foreach (var item in _usersInSystem)
                if (item.Name == input && item.Password == input)
                {
                    item.MyToken = new Token();
                    return true;
                }
            return false;
        }
    }
}
