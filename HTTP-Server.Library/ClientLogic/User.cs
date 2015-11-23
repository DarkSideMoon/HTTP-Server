using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.ClientLogic
{
    public class User
    {
        private static List<User> _usersOnline = new List<User>();
        private static List<User> _banUsers = new List<User>();

        public User()
        {
            this.LoadOnlineUsers();
            this.LoadBanUsers();
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }

        public void AddBanUser(User user)
        {
            _banUsers.Add(user);
        }

        public void DelBanUser(User user)
        {
            _banUsers.Remove(user);
        }

        public List<User> GetBanUsers()
        {
            return _banUsers;
        }

        // Send push notification on mobile that is new user entered in your server cool and awesome
        public void AddOnlineUser(User user)
        {
            _usersOnline.Add(user);
        }

        public void DelOnlineUser(User user)
        {
            _usersOnline.Remove(user);
        }

        public List<User> GetUsersOnline()
        {
            return _usersOnline;
        }

        private void LoadOnlineUsers()
        {
            this.AddBanUser(new User() { Name = "Pasha", Age = 19, Email = "shark005@i.ua", Phone = "123-4124-214" });
            this.AddBanUser(new User() { Name = "Vlad", Age = 19, Email = "vllad214@i.ua", Phone = "49824-214" });
            this.AddBanUser(new User() { Name = "Sasha", Age = 16, Email = "234saf@i.ua", Phone = "41251" });
            this.AddBanUser(new User() { Name = "Dan", Age = 17, Email = "123fva@i.ua", Phone = "591-251-54" });
            this.AddBanUser(new User() { Name = "Vasya", Age = 18, Email = "wer2@i.ua", Phone = "9582-00-87" });
            this.AddBanUser(new User() { Name = "Invisible man", Age = 19, Email = "agv2@i.ua", Phone = "1240-2856-22" });
        }

        private void LoadBanUsers()
        {
            this.AddBanUser(new User() { Name = "Petro", Age = 30, Email = "sasdlfj@i.ua", Phone = "1242" });
            this.AddBanUser(new User() { Name = "Metro", Age = 30, Email = "slk;jfdi@i.ua", Phone = "49862" });
        }
    }
}
