using System.Collections.Generic;
using GameProgression;
using System;

namespace Engine.GameProgression
{
    public class ControlVariable : PersistentData
    {
        public enum Operator { Set, Add, Sub, Mul, Div, Mod }
        public enum OperandType { Constant, Variable, Random }

        public Operator Operation { get; set; }
        public OperandType Operand { get; set; }
        public List<int> Values { get; private set; }

        public override void Run()
        {
            PersistentDataHolder holder = PersistentDataHolder.Instance;
            int value = GetValue();

            int currentValue = holder.HasKey(Key) ? (int)holder.GetData(Key) : 0;

            switch (Operation)
            {
                case Operator.Add:
                    holder.StoreData(Key, currentValue + value);
                    break;
                case Operator.Sub:
                    holder.StoreData(Key, currentValue - value);
                    break;
                case Operator.Mul:
                    holder.StoreData(Key, currentValue * value);
                    break;
                case Operator.Div:
                    holder.StoreData(Key, currentValue / value);
                    break;
                case Operator.Mod:
                    holder.StoreData(Key, currentValue % value);
                    break;
                case Operator.Set:
                    holder.StoreData(Key, value);
                    break;
            }

            Finished.Invoke();
        }

        public void AddValue(int value)
        {
            if (Values == null)
                Values = new List<int>();

            Values.Add(value);
        }

        private int GetValue()
        {
            switch(Operand)
            {
                case OperandType.Constant:
                    return Values[0];
                case OperandType.Random:
                    Random r = new Random();
                    return r.Next(Values[0], Values[1] + 1); // Bounds are inclusive
                case OperandType.Variable:
                    return (int)PersistentDataHolder.Instance.GetData(Source);
                default:
                    throw new InvalidOperationException("Provided operand does not exist.");
            }
        }
    }
}
