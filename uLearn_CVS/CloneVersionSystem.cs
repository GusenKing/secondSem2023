using NUnit.Framework;
using System.Collections.Generic;

namespace Clones
{
    public class CloneVersionSystem : ICloneVersionSystem
    {
        private readonly List<Clone> clonesList;

        public CloneVersionSystem()
        {
            clonesList = new List<Clone>();
            clonesList.Add(new Clone());
        }

	    public string Execute(string query)
	    {
		    var splittedQuery = query.Split(' ');
            var cloneID = int.Parse(splittedQuery[1]);

            switch (splittedQuery[0])
            {
                case "check":
                    return clonesList[cloneID - 1].Check();

                case "learn":
                    var programNum = int.Parse(splittedQuery[2]);
                    clonesList[cloneID - 1].Learn(programNum);
                    return null;

                case "rollback":
                    clonesList[cloneID - 1].RollBack();
                    return null;

                case "relearn":
                    clonesList[cloneID - 1].Relearn();
                    return null;

                case "clone":
                    clonesList.Add(new Clone(clonesList[cloneID - 1]));
                    return null;
            }
            return null;
        }
    }

    public class MyStackItem
    {
        public readonly int Value;
        public readonly MyStackItem Previous;

        public MyStackItem(int value, MyStackItem previous)
        {
            Value = value;
            Previous = previous;
        }
    }

    public class MyStack
    {
        private MyStackItem last;
        public MyStack() { }

        public MyStack(MyStack stack)
        {
            last = stack.last;
        }

        public void Push(int value)
        {
            last = new MyStackItem(value, last);
        }

        public int Peek()
        {
            return last.Value;
        }

        public int Pop()
        {
            var value = last.Value;
            last = last.Previous;
            return value;
        }

        public bool IsEmpty()
        {
            return last == null;
        }

        public void Clear()
        {
            last = null;
        }
    }

    public class Clone
    {
        private readonly MyStack learnedPrograms;
        private readonly MyStack rollbackHistory;

        public Clone()
        {
            learnedPrograms = new MyStack();
            rollbackHistory = new MyStack();
        }

        public Clone(Clone other)
        {
            learnedPrograms = new MyStack(other.learnedPrograms);
            rollbackHistory = new MyStack(other.rollbackHistory);
        }

        public void Learn(int pi)
        {
            learnedPrograms.Push(pi);
            rollbackHistory.Clear();
        }

        public void RollBack()
        {
            var rolledBackProgram = learnedPrograms.Pop();
            rollbackHistory.Push(rolledBackProgram);
        }

        public void Relearn()
        {
            var relearningProgram = rollbackHistory.Pop();
            learnedPrograms.Push(relearningProgram);
        }

        public string Check()
        {
            return learnedPrograms.IsEmpty() ? "basic" : learnedPrograms.Peek().ToString();
        }
    }
}