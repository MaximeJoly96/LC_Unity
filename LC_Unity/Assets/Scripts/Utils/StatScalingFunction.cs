using UnityEngine;

namespace Utils
{
    public class StatScalingFunction
    {
        public float A { get; set; }
        public float Exponent { get; set; }
        public float B { get; set; }

        public StatScalingFunction(float a, float exponent, float b)
        {
            A = a;
            Exponent = exponent;
            B = b;
        }

        public float Compute(float x)
        {
            return B + A * Mathf.Pow(x, Exponent);
        }
    }
}
