using UnityEngine;

namespace Utils
{
    public class QuadraticFunction
    {
        public float A { get; set; }
        public float B { get; set; }
        public float C { get; set; }

        public QuadraticFunction(float a, float b, float c)
        {
            A = a;
            B = b;
            C = c;
        }

        public float Compute(float x)
        {
            return A * Mathf.Pow(x, 2) + B * x + C;
        }
    }
}
