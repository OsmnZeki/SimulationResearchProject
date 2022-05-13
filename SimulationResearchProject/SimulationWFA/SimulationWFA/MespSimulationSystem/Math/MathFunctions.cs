using System.Numerics;
using RenderLibrary.DLL;

namespace MESPSimulationSystem.Math
{
    public static class MathFunctions
    {
        public static double ConvertToRadians(double angle)
        {
            return (System.Math.PI / 180) * angle;
        }

        public static Vector3 Rotate(Mat4 modelMatrix, float degree, Vector3 axisOfRot, Vector3 direction)
        {
            float[] returnArr = { direction.X, direction.Y, direction.Z };
            RenderProgramDLL.Rotate(modelMatrix.matrixAdress, degree,
                new[] { axisOfRot.X, axisOfRot.Y, axisOfRot.Z }, returnArr);

            return new Vector3(returnArr[0], returnArr[1], returnArr[2]);
        }

        public static int RoundToInt(float value)
        {
            int valueCastInt = (int)value;

            float floatingPoint = valueCastInt + 1 - value;

            if (.5f - floatingPoint >=0)
            {
                return valueCastInt + 1;
            }
            else
            {
                return valueCastInt;
            }

        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }
        public static float Clamp(float value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        public static float InverseLerp(float a, float b, float value)
        {
            if (a != b)
                return Clamp((value - a) / (b - a), 0, 1);
            else
                return 0.0f;
        }

    }
}