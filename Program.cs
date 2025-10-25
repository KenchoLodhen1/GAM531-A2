using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Vector Operations ===");
        Vector3D v1 = new Vector3D(2, 3, 4);
        Vector3D v2 = new Vector3D(1, 5, 2);

        Console.WriteLine($"v1 = {v1}");
        Console.WriteLine($"v2 = {v2}");
        Console.WriteLine($"v1 + v2 = {v1 + v2}");
        Console.WriteLine($"v1 - v2 = {v1 - v2}");
        Console.WriteLine($"Dot(v1, v2) = {Vector3D.Dot(v1, v2)}");
        Console.WriteLine($"Cross(v1, v2) = {Vector3D.Cross(v1, v2)}");

        Console.WriteLine("\n=== Matrix Operations ===");
        var identity = Matrix3x3.Identity();
        var scale = Matrix3x3.Scale(2, 0.5f, 1);
        var rotation = Matrix3x3.RotateX(45);

        Console.WriteLine("Identity:\n" + identity);
        Console.WriteLine("Scale:\n" + scale);
        Console.WriteLine("Rotation X(45°):\n" + rotation);

        var combined = Matrix3x3.Multiply(rotation, scale);
        Console.WriteLine("Combined = Rotation * Scale:\n" + combined);

        Vector3D transformed = combined.MultiplyVector(v1);
        Console.WriteLine($"\nTransform v1 = {v1} → {transformed}");
    }
}
