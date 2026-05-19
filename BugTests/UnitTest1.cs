using Microsoft.VisualStudio.TestTools.UnitTesting;
using BugPro;
namespace BugTests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Bug b = new Bug();
            Assert.AreEqual(StatesOfBug.Opened, b.GetState());
        }
        [TestMethod]
        public void TestMethod2()
        {
            Bug b = new Bug();
            b.Run();
            Assert.AreEqual(StatesOfBug.BeingAnalysed, b.GetState());
        }
        [TestMethod]
        public void TestMethod3()
        {
            Bug b = new Bug();
            b.Run(); b.Accept();
            Assert.AreEqual(StatesOfBug.InProcess, b.GetState());
        }
        [TestMethod]
        public void TestMethod4()
        {
            Bug b = new Bug();
            b.Run(); b.Reject();
            Assert.AreEqual(StatesOfBug.Rejected, b.GetState());
        }
        [TestMethod]
        public void TestMethod5()
        {
            Bug b = new Bug();
            b.Run(); b.WhetherOk();
            Assert.AreEqual(StatesOfBug.Solved, b.GetState());
        }
        [TestMethod]
        public void TestMetho6()
        {
            Bug b = new Bug();
            b.Run(); b.WhetherBad();
            Assert.AreEqual(StatesOfBug.Returning, b.GetState());
        }
        [TestMethod]
        public void TestMethod7()
        {
            Bug b = new Bug();
            b.Run(); b.Do();
            Assert.AreEqual(StatesOfBug.Postponed, b.GetState());
        }
        [TestMethod]
        public void TestMethod8()
        {
            Bug b = new Bug();
            b.Run(); b.Do(); b.Run();
            Assert.AreEqual(StatesOfBug.BeingAnalysed, b.GetState());
        }
        [TestMethod]
        public void TestMethod9()
        {
            Bug b = new Bug();
            b.Run(); b.Accept(); b.Accomplished();
            Assert.AreEqual(StatesOfBug.BeingAnalysed, b.GetState());
        }
        [TestMethod]
        public void TestMethod10()
        {
            Bug b = new Bug();
            b.Run(); b.WhetherBad(); b.Accept();
            Assert.AreEqual(StatesOfBug.InProcess, b.GetState());
        }
    }
}
