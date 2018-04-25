using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestDbMultiTool
{
    [TestClass]
    public class DbConnectionTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void TestNew()
        {
            Moq.Mock<Npgsql.NpgsqlConnection> mock;
        }
    }
}
