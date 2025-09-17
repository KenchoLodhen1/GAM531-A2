// Program.cs
using System;
using MathLibrary;

namespace VectorMatrixDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Vector & Matrix Demo\n");

            ShowVectorOps();
            ShowMatrixOps();
            ShowTransforms();

            Console.WriteLine("\nDemo complete. Press any key...");
            Console.ReadKey();
        }

        static void ShowVectorOps()
        {
            Console.WriteLine("== Vector Ops ==");

            var v1 = new Vector3D(3, 4, 5);
            var v2 = new Vector3D(1, 2, 3);

            Console.WriteLine($"v1 = {v1}");
            Console.WriteLine($"v2 = {v2}");
            Console.WriteLine($"v1 + v2 = {v1 + v2}");
            Console.WriteLine($"v1 - v2 = {v1 - v2}");
            Console.WriteLine($"dot(v1,v2) = {Vector3D.Dot(v1, v2):F2}");
            Console.WriteLine($"cross(v1,v2) = {Vector3D.Cross(v1, v2)}");
            Console.WriteLine($"|v1| = {v1.Magnitude():F2}");
            Console.WriteLine($"normalized v1 = {v1.Normalize()}");
            Console.WriteLine();
        }

        static void ShowMatrixOps()
        {
            Console.WriteLine("== Matrix Ops ==");

            var id = Matrix4x4.Identity();
            var scale = Matrix4x4.Scale(2, 3, 4);
            var rot = Matrix4x4.RotationZ(45);
            var trans = Matrix4x4.Translation(5, 1, -2);

            Console.WriteLine("Identity:\n" + id);
            Console.WriteLine("Scale (2,3,4):\n" + scale);
            Console.WriteLine("Rotate Z 45°:\n" + rot);
            Console.WriteLine("Translate (5,1,-2):\n" + trans);

            var combo = trans * rot * scale;
            Console.WriteLine("Combined (T*R*S):\n" + combo);
            Console.WriteLine();
        }

        static void ShowTransforms()
        {
            Console.WriteLine("== Transforms ==");

            var v = new Vector3D(1, 0, 0);

            var scale = Matrix4x4.Scale(2, 2, 2);
            var rotY = Matrix4x4.RotationY(90);
            var trans = Matrix4x4.Translation(3, 0, 0);

            Console.WriteLine($"v = {v}");
            Console.WriteLine("Scaled: " + scale.TransformDirection(v));
            Console.WriteLine("RotY 90°: " + rotY.TransformDirection(v));
            Console.WriteLine("Translated point: " + trans.TransformPoint(v));
        }
    }
}
