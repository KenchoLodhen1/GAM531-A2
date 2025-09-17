// Vector3D.cs
using System;

namespace MathLibrary
{
    /// <summary>3D vector (x,y,z).</summary>
    public class Vector3D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        /// <summary>Create from components.</summary>
        public Vector3D(float x, float y, float z)
        {
            X = x; Y = y; Z = z;
        }

        /// <summary>Vector sum.</summary>
        public static Vector3D operator +(Vector3D a, Vector3D b) =>
            new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        /// <summary>Vector difference.</summary>
        public static Vector3D operator -(Vector3D a, Vector3D b) =>
            new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        /// <summary>Dot product.</summary>
        public static float Dot(Vector3D a, Vector3D b) =>
            a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        /// <summary>Cross product.</summary>
        public static Vector3D Cross(Vector3D a, Vector3D b) =>
            new Vector3D(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );

        /// <summary>Length.</summary>
        public float Magnitude() =>
            (float)Math.Sqrt(X * X + Y * Y + Z * Z);

        /// <summary>Unit vector.</summary>
        public Vector3D Normalize()
        {
            float mag = Magnitude();
            return mag > 0 ? new Vector3D(X / mag, Y / mag, Z / mag) : new Vector3D(0, 0, 0);
        }

        /// <summary>Pretty print.</summary>
        public override string ToString() =>
            $"({X:F2}, {Y:F2}, {Z:F2})";
    }
}
