using System;

namespace BehaviorTree.Synchronous
{
    class DecoratorException : Exception
    {
        public DecoratorException(string msg) : base(msg) { }
    }
}
