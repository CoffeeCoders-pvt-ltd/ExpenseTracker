using System;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Common.Model;

namespace ExpenseTracker.Core.Entities
{
    [Table("workspace", Schema = "core")]
    public class Workspace : BaseModel
    {
        public const string TypeDefaultWorkspace = "DEFAULT_WORKSPACE";
        public const string TypeNormalWorkspace = "NORMAL_WORKSPACE";


        public string Token { get; protected set; } = Guid.NewGuid().ToString();

        public string WorkSpaceName { get; protected set; }
        public string Color { get; protected set; }
        public string? Description { get; set; }

        public User User { get; protected set; }
        public long UserId { get; protected set; }

        public string WorkspaceType { get; protected set; }

        protected Workspace()
        {
        }

        public static Workspace Create(User user, string workspaceName, string color)
        {
            return new Workspace(user, workspaceName, color);
        }

        private Workspace(User user, string workSpaceName, string color)
        {
            ChangeName(workSpaceName);
            ChangeColor(color);
            AssignUser(user);
        }

        public void Update(string workspaceName, string color, string? description)
        {
            WorkSpaceName = workspaceName;
            Color = color;
            Description = description;
        }

        public virtual void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Invalid Workspace name.");
            WorkSpaceName = char.ToUpper(name[0]) + name[1..];
        }


        public virtual void ChangeColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color)) throw new Exception("Invalid Workspace color.");
            // todo more validation for color
            Color = color;
        }

        public virtual void SetAsDefaultWorkspace() => WorkspaceType = TypeDefaultWorkspace;

        public virtual bool IsDefault => WorkspaceType == TypeDefaultWorkspace;

        public virtual void SetAsNormalWorkspace() => WorkspaceType = TypeNormalWorkspace;

        public virtual void AssignUser(User user)
        {
            User = user;
            UserId = user.Id;
            User.AddWorkspace(this);
        }
    }
}