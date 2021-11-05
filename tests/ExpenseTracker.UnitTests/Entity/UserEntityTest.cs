using ExpenseTracker.Core.Entities;
using Xunit;

namespace ExpenseTracker.UnitTests.Entity
{
    public class UserEntityTest
    {
        [Fact]
        public void test_protected_property_sets_correct_value()
        {
            var user = new User("FirstName", "LastName", "Admin", "Admin");

            typeof(User).GetProperty(nameof(User.Id))?
                .SetValue(user, 1);
            typeof(User).GetProperty(nameof(User.Username))?
                .SetValue(user, "ad");
            typeof(User).GetProperty(nameof(User.Password))?
                .SetValue(user, "psd");
            Assert.Equal("ad", user.Username);
            Assert.Equal("psd", user.Password);
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public void test_Set_User_Name_sets_correct_value()
        {
            var user = new User("FirstName", "LastName", "Admin", "Admin");
            Assert.Equal("aa", user.Username);
        }

        [Fact]
        public void test_Set_User_pass_sets_correct_value()
        {
            var user = new User("FirstName", "LastName", "Admin", "Admin");
            Assert.Equal("aa", user.Password);
        }
    }
}