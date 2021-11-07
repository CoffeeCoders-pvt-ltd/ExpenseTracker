using System;
using ExpenseTracker.Common.Constants;

namespace ExpenseTracker.Common.Model
{
    public abstract class BaseModel : IBaseModel
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = StatusConstants.StatusActive;

        public virtual IBaseModel Activate()
        {
            Status = StatusConstants.StatusActive;
            return this;
        }

        public virtual IBaseModel Deactivate()
        {
            Status = StatusConstants.StatusInactive;
            return this;
        }

        public bool IsActive() => Status == StatusConstants.StatusActive;

        public virtual void ToggleStatus()
        {
            if (IsActive()) Deactivate();
            else Activate();
        }
    }
}