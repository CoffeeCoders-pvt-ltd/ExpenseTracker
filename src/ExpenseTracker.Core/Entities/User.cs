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