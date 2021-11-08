using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Core.Entities.Common;

namespace ExpenseTracker.Web.ViewModels.Workspace
{
    public class WorkspaceEditViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
        public readonly Dictionary<string, string> ColorList = Colors.GetColors;
        public string? Description { get; set; }
    }
}