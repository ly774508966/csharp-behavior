using System;

namespace BehaviorTree.Synchronous
{
    public class SelectorFailException : Exception
    {
        public SelectorFailException(string msg) : base(msg) { }
    }
}
