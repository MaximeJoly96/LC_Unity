﻿using Engine.Events;
using System.Collections.Generic;

namespace Engine.GameProgression
{
    public class ControlVariable : IRunnable
    {
        public enum Operator { Set, Add, Sub, Mul, Div, Mod }
        public enum OperandType { Constant, Variable, Random }

        public Operator Operation { get; set; }
        public OperandType Operand { get; set; }
        public List<int> Values { get; private set; }

        public void Run()
        {

        }

        public void AddValue(int value)
        {
            if (Values == null)
                Values = new List<int>();

            Values.Add(value);
        }
    }
}
