using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseTracker.Common.Model;

namespace ExpenseTracker.Core.Entities
{
    public sealed class User : BaseModel
    {
        // public static User Create(string userName, string password)
        // {
        //     return new User(userName, password);
        // }

        // private User(string username, string password)
        // {
        //     SetUserName(username);
        //     SetPassword(password);
        // }

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }

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

        // public void SetUserName(string userName)
        // {
        //     //validation for the username
        //     if (string.IsNullOrWhiteSpace(userName))
        //     {
        //         //todo: change to custom exception
        //         throw new Exception("Username Not Valid.");
        //     }
        //
        //     Username = userName;
        // }

        // public void SetPassword(string password)
        // {
        //     //validation for the password
        //     if (string.IsNullOrWhiteSpace(password))
        //     {
        //         //todo: change to custom exception
        //         throw new Exception("Username Not Valid.");
        //     }
        //
        //     Password = password;
        // }

        public ICollection<Workspace> Workspaces { get; set; } = new List<Workspace>();

        public bool HasWorkspace => Workspaces.Any();

        private bool HasDefaultWorkspace =>
            Workspaces.Count(a => a.WorkspaceType == Workspace.TypeDefaultWorkspace) == 1;

        public Workspace DefaultWorkspace =>
            (HasDefaultWorkspace
                ? Workspaces.FirstOrDefault(a => a.WorkspaceType == Workspace.TypeDefaultWorkspace)
                : throw new Exception("Default Workspace Not Found")) ??
            throw new Exception("Default Workspace Not Found");

        public void AddWorkspace(Workspace workspace)
        {
            Workspaces.Add(workspace);
        }
    }
}