using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Core.Entities.Common;

namespace ExpenseTracker.Web.ViewModels.Workspace
{
    public class WorkspaceEditViewModel
    {
        [Required]
        [DisplayName("Workspace Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Color")]
        public string Color { get; set; }
        public readonly Dictionary<string, string> ColorList = Colors.GetColors;
        [DisplayName("Description")]
        public string? Description { get; set; }

        [DisplayName("Icon")]
        public string Icon { get; set; }
    }
}