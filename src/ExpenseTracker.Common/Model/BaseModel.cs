using System;
using ExpenseTracker.Common.Constants;

namespace ExpenseTracker.Common.Model
{
    public abstract class BaseModel : IBaseModel
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = Constant.StatusActive;

        public virtual IBaseModel Activate()
        {
            Status = Constant.StatusActive;
            return this;
        }

        public virtual IBaseModel Deactivate()
        {
            Status = Constant.StatusInactive;
            return this;
        }

        public bool IsActive() => Status == Constant.StatusActive;

        public virtual void ToggleStatus()
        {
            if (IsActive()) Deactivate();
            else Activate();
        }
    }
}