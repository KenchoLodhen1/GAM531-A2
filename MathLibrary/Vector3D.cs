using System;

public class Vector3D
{
    public float X, Y, Z;

    public Vector3D(float x, float y, float z)
    {
        X = x; Y = y; Z = z;
    }

    public static Vector3D operator +(Vector3D a, Vector3D b)
        => new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    public static Vector3D operator -(Vector3D a, Vector3D b)
        => new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public static float Dot(Vector3D a, Vector3D b)
        => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

    public static Vector3D Cross(Vector3D a, Vector3D b)
        => new Vector3D(
            a.Y * b.Z - a.Z * b.Y,
            a.Z * b.X - a.X * b.Z,
            a.X * b.Y - a.Y * b.X
        );

    public override string ToString()
        => $"({X:0.00}, {Y:0.00}, {Z:0.00})";
}
