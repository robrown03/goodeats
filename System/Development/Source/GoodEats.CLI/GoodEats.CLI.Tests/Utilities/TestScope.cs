using System;
using System.Collections.Generic;
using System.Text;

namespace GoodEats.CLI.Tests.Utilities
{
    public class TestScope<T> : ITestScope<T> where T : class
    {
        public T InstanceUnderTest { get; set; }

    }

    public interface ITestScope<T>
    {
        T InstanceUnderTest { get; set; }
    }
}
