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
        public string? Description { get; protected set; }
        public string? Icon { get; protected set; }

        public virtual User User { get; protected set; }
        public long UserId { get; protected set; }

        public string WorkspaceType { get; protected set; }

        protected Workspace()
        {
        }

        public Workspace(User user, string workSpaceName, string color, string? icon)
        {
            ChangeName(workSpaceName);
            ChangeColor(color);
            AssignUser(user);
            Icon = icon;
        }

        public void Update(string workspaceName, string color, string? description, string? icon)
        {
            WorkSpaceName = workspaceName;
            Color = color;
            Description = description;
            Icon = icon;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Invalid Workspace name.");
            WorkSpaceName = char.ToUpper(name[0]) + name.Substring(1);
        }


        public void ChangeColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color)) throw new Exception("Invalid Workspace color.");
            // todo more validation for color
            Color = color;
        }

        public void SetAsDefaultWorkspace() => WorkspaceType = TypeDefaultWorkspace;

        public bool IsDefault => WorkspaceType == TypeDefaultWorkspace;

        public void SetAsNormalWorkspace() => WorkspaceType = TypeNormalWorkspace;

        private void AssignUser(User user)
        {
            User = user;
            User.AddWorkspace(this);
        }
    }
}