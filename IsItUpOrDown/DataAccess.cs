using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace IsItUpOrDown
{
    public class Website
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Timeout { get; set; }
        public int RetryCount { get; set; }
        //
        public IEnumerable<WebsiteUsers> WebsiteUsers { get; set; }
    }

    public class WebsiteUsers
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WebsiteId { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long ChatId { get; set; }
        //
        public IEnumerable<WebsiteUsers> WebsiteUsers { get; set; }
    }
    
    public class DataAccess
    {
        public static void CreateTables()
        {
            using (var connection = new SQLiteConnection(Helper.ConnectionString))
            {
                connection.Open();
                //
                connection.Execute(@"Create table if not exists Websites(
                Id integer primary key autoincrement,
                Name varchar(100) not null,
                Url varchar(100) not null,
                RetryCount integer not null,
                Timeout integer not null);");

                connection.Execute(@"CREATE table if not exists Users(
                Id integer primary key autoincrement,
                Name varchar(100) null,
                Email varchar(100) null,
                ChatId integer not null);");
            }
        }

        public static void InsertSite(Website webstite)
        {
            using (var connection = new SQLiteConnection(Helper.ConnectionString))
            {
                connection.Open();
                //
                connection.Execute("INSERT INTO websites (name, url, timeout, retryCount) VALUES (@name, @url, @timeout, @retryCount)", 
                    new
                    {
                        name = webstite.Name,
                        url = webstite.Url,
                        timeout = webstite.Timeout,
                        retryCount = webstite.RetryCount
                    });
            }
        }
        //
        public static void UpdateSite(Website website)
        {
            using (var connection = new SQLiteConnection(Helper.ConnectionString))
            {
                connection.Open();
                //
                connection.Execute(@"UPDATE websites SET name = @name, 
                url = @url, 
                timeout = @timeout, 
                retryCount = @retryCount 
                WHERE Id = @id", new { id = website.Id, name = website.Name, url = website.Url, timeout = website.Timeout, retryCount = website.RetryCount });
            }
        }

        public static void AddUser(User user)
        {
            using (var connection = new SQLiteConnection(Helper.ConnectionString))
            {
                connection.Open();
                //
                connection.Execute(@"INSERT INTO users (name, email, chatId) 
                                        VALUES (@name, @email, @chatId)", new
                {
                    name = user.Name,
                    email = user.Email,
                    chatId = user.ChatId
                });
            }
        }

        public static List<User> FetchUsers()
        {
            using (var connection = new SQLiteConnection(Helper.ConnectionString))
            {
                connection.Open();
                //
                return connection.Query<User>(@"SELECT * FROM users").ToList();
            }
        }
        
        public static User FetchUserByChatId(long chatId)
        {
            using (var connection = new SQLiteConnection(Helper.ConnectionString))
            {
                connection.Open();
                //
                return connection.Query<User>(@"SELECT * FROM users where chatId = @chatId", new { chatId = chatId}).FirstOrDefault();
            }
        }

        public static List<Website> GetSites()
        {
            using (var connection = new SQLiteConnection(Helper.ConnectionString))
            {
                connection.Open();
                return connection.Query<Website>("SELECT * FROM websites;").ToList();
            }
        }

        public static Website GetSite(int id)
        {
            using (var connection = new SQLiteConnection(Helper.ConnectionString))
            {
                connection.Open();
                return connection.Query<Website>("SELECT * FROM websites where Id = @id;", new { id = id}).FirstOrDefault();
            }
        }
    }
}