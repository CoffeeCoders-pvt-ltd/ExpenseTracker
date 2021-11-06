using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using ExpenseTracker.Common.Model;

namespace ExpenseTracker.Core.Entities
{
    [Table("user", Schema = "core")]
    public class User : BaseModel
    {
        // protected User() { }

        // public static User Create(string userName, string password)
        // {
        //     return new User(userName, password);
        // }
        // private User(string username, string password)
        // {
        //     SetUserName(username);
        //     SetPassword(password);
        // }

        protected User()
        {
        }

        public User(string firstName, string lastName, string username, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
        }

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Username { get; protected set; }

        public string Password { get; protected set; }
        // public virtual void SetUserName(string userName)
        // {
        //     //validation for the username
        //     if (string.IsNullOrWhiteSpace(userName))
        //     {
        //         //todo: change to custom exception
        //         throw new Exception("Username Not Valid.");
        //     }
        //     Username = userName;
        // }

        // [JsonIgnore]


        // public virtual void SetPassword(string password)
        // {
        //     //validation for the password
        //     if (string.IsNullOrWhiteSpace(password))
        //     {
        //         //todo: change to custom exception
        //         throw new Exception("Username Not Valid.");
        //     }
        //     Password = password;
        // }

        public virtual List<Workspace> Workspaces { get; set; } = new List<Workspace>();

        public virtual bool HasWorkspace => Workspaces.Any();

        public virtual bool HasDefaultWorkspace =>
            Workspaces.Count(a => a.WorkspaceType == Workspace.TypeDefaultWorkspace) == 1;

        public virtual Workspace DefaultWorkspace =>
            (HasDefaultWorkspace
                ? Workspaces.FirstOrDefault(a => a.WorkspaceType == Workspace.TypeDefaultWorkspace)
                : throw new Exception("Default Workspace Not Found")) ??
            throw new Exception("Default Workspace Not Found");

        public virtual void AddWorkspace(Workspace workspace)
        {
            Workspaces.Add(workspace);
        }
    }
}