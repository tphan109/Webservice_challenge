using Microsoft.VisualStudio.TestTools.UnitTesting;
using Webservice_challenge.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webservice_challenge.Controllers.Tests
{
    [TestClass()]
    public class FibonacciControllerTests
    {
        [TestMethod()]
        public void FibonacciTest_OK()
        {
            FibonacciController fibonacci = new FibonacciController();
            Assert.AreEqual(fibonacci.Fibonacci(5), 5);
        }
        [TestMethod()]
        public void FibonacciTest_KO()
        {
            FibonacciController fibonacci = new FibonacciController();
            Assert.AreEqual(fibonacci.Fibonacci(5), 4);
        }
        [TestMethod()]
        public void FibonacciTest_OK1()
        {
            FibonacciController fibonacci = new FibonacciController();
            Assert.AreEqual(fibonacci.Fibonacci(0), -1);
        }
        [TestMethod()]
        public void FibonacciTest_OK2()
        {
            FibonacciController fibonacci = new FibonacciController();
            Assert.AreEqual(fibonacci.Fibonacci(101), -1);
        }
    }
}