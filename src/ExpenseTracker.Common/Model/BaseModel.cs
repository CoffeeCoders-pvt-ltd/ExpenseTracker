using System;
using ExpenseTracker.Common.Constants;

namespace ExpenseTracker.Common.Model
{
    public abstract class BaseModel
    {
        public long Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; } = DateTime.Now;
        public string Status { get; protected set; } = StatusConstants.StatusActive;

        public virtual BaseModel Activate()
        {
            Status = StatusConstants.StatusActive;
            return this;
        }

        public virtual BaseModel Deactivate()
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