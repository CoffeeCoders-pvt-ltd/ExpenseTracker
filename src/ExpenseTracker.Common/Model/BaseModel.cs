using System;

namespace ExpenseTracker.Common.Model
{
    public abstract class BaseModel : IBaseModel
    {
        public const string StatusActive = "Active";
        public const string StatusInactive = "Inactive";
        public const string StatusDeleted = "Deleted";

        public static readonly string[] StatusList = new[] { StatusActive, StatusInactive, StatusDeleted };
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public string Status { get; set; } = StatusActive;

        public virtual IBaseModel Activate()
        {
            Status = StatusActive;
            return this;
        }

        public virtual IBaseModel Deactivate()
        {
            Status = StatusInactive;
            return this;
        }

        public bool IsActive() => Status == StatusActive;

        public virtual void ToggleStatus()
        {
            if (IsActive()) Deactivate();
            else Activate();
        }
    }
}