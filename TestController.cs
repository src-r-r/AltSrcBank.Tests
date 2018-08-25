using AltSrcBank.Models;
using ASBCLI;
using ASBCLI.Financial;
using ASBLib.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AltSrcBank.Tests
{
    [TestFixture()]
    public class TestController
    {
        [Test()]
        public void TestUserCreation()
        {
			User user = new User("user@host.com", "password");
			Controller controller = new Controller("MyTest");
			controller.userAdd(user);
			var users = controller.getAllUsers();
			Assert.AreEqual(users.Count, 1);
        }

		[Test()]
        public void TestLogin()
		{
			string email = "user@host.com", password = "password";
            User user = new User(email, password);
            Controller controller = new Controller("MyTest");
            controller.userAdd(user);
			string res = controller.userAuthenticate(email, password);
			Assert.AreEqual(res, email);
			res = controller.userAuthenticate(email, "notpassword");
			Assert.IsNull(res);
		}

        [Test()]
        public void TestTransactions()
		{
            string email = "user@host.com", password = "password";
            User user = new User(email, password);
            Controller controller = new Controller("MyTest");
            controller.userAdd(user);
			Deposit deposit = new Deposit(1.25F);
			controller.AccountAddTransaction(user, deposit);
			Assert.AreEqual(user.GetBalance(), 1.25F);
			controller.AccountAddTransaction(user, new Withdrawal(0.50F));
            Assert.AreEqual(user.GetBalance(), 0.75F);
			controller.AccountAddTransaction(user, new Withdrawal(1.00F));
            Assert.AreEqual(user.GetBalance(), -0.25F);
		}
    }
}
