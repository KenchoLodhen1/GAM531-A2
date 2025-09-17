// matrix.cs
using System;
using System.Text;

namespace MathLibrary
{
    /// <summary>4x4 matrix for common 3D transforms.</summary>
    public class Matrix4x4
    {
        private readonly float[,] data;

        /// <summary>Zero-initialized 4x4.</summary>
        public Matrix4x4()
        {
            data = new float[4, 4];
        }

        /// <summary>Init from a 4x4 array (copied).</summary>
        public Matrix4x4(float[,] values)
        {
            if (values is null || values.GetLength(0) != 4 || values.GetLength(1) != 4)
                throw new ArgumentException("Matrix must be 4x4", nameof(values));

            data = new float[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    data[i, j] = values[i, j];
        }

        /// <summary>Element accessor.</summary>
        public float this[int row, int col]
        {
            get => data[row, col];
            set => data[row, col] = value;
        }

        /// <summary>Identity.</summary>
        public static Matrix4x4 Identity()
        {
            var m = new Matrix4x4();
            for (int i = 0; i < 4; i++) m[i, i] = 1f;
            return m;
        }

        /// <summary>Uniform/non-uniform scale.</summary>
        public static Matrix4x4 Scale(float sx, float sy, float sz)
        {
            var m = Identity();
            m[0, 0] = sx; m[1, 1] = sy; m[2, 2] = sz;
            return m;
        }

        /// <summary>Rotate about X (degrees).</summary>
        public static Matrix4x4 RotationX(float deg)
        {
            float r = DegToRad(deg);
            float c = (float)Math.Cos(r);
            float s = (float)Math.Sin(r);

            var m = Identity();
            m[1, 1] = c; m[1, 2] = -s;
            m[2, 1] = s; m[2, 2] = c;
            return m;
        }

        /// <summary>Rotate about Y (degrees).</summary>
        public static Matrix4x4 RotationY(float deg)
        {
            float r = DegToRad(deg);
            float c = (float)Math.Cos(r);
            float s = (float)Math.Sin(r);

            var m = Identity();
            m[0, 0] = c; m[0, 2] = s;
            m[2, 0] = -s; m[2, 2] = c;
            return m;
        }

        /// <summary>Rotate about Z (degrees).</summary>
        public static Matrix4x4 RotationZ(float deg)
        {
            float r = DegToRad(deg);
            float c = (float)Math.Cos(r);
            float s = (float)Math.Sin(r);

            var m = Identity();
            m[0, 0] = c; m[0, 1] = -s;
            m[1, 0] = s; m[1, 1] = c;
            return m;
        }

        /// <summary>Translate by (x,y,z).</summary>
        public static Matrix4x4 Translation(float x, float y, float z)
        {
            var m = Identity();
            m[0, 3] = x; m[1, 3] = y; m[2, 3] = z;
            return m;
        }

        /// <summary>Matrix product.</summary>
        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
        {
            var r = new Matrix4x4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    float sum = 0f;
                    for (int k = 0; k < 4; k++)
                        sum += a[i, k] * b[k, j];
                    r[i, j] = sum;
                }
            }
            return r;
        }

        /// <summary>Transforms a vector with homogeneous divide.</summary>
        public Vector3D TransformVector(Vector3D v)
        {
            float w = data[3, 0] * v.X + data[3, 1] * v.Y + data[3, 2] * v.Z + data[3, 3];
            if (Math.Abs(w) < 1e-5f) w = 1f; // avoid /0

            return new Vector3D(
                (data[0, 0] * v.X + data[0, 1] * v.Y + data[0, 2] * v.Z + data[0, 3]) / w,
                (data[1, 0] * v.X + data[1, 1] * v.Y + data[1, 2] * v.Z + data[1, 3]) / w,
                (data[2, 0] * v.X + data[2, 1] * v.Y + data[2, 2] * v.Z + data[2, 3]) / w
            );
        }

        /// <summary>Transforms a point (includes translation).</summary>
        public Vector3D TransformPoint(Vector3D p) => TransformVector(p);

        /// <summary>Transforms a direction (ignores translation).</summary>
        public Vector3D TransformDirection(Vector3D d)
        {
            return new Vector3D(
                data[0, 0] * d.X + data[0, 1] * d.Y + data[0, 2] * d.Z,
                data[1, 0] * d.X + data[1, 1] * d.Y + data[1, 2] * d.Z,
                data[2, 0] * d.X + data[2, 1] * d.Y + data[2, 2] * d.Z
            );
        }

        /// <summary>Pretty print.</summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                sb.Append('[');
                for (int j = 0; j < 4; j++)
                {
                    sb.AppendFormat("{0,8:F3}", data[i, j]);
                    if (j < 3) sb.Append(", ");
                }
                sb.AppendLine("]");
            }
            return sb.ToString();
        }

        private static float DegToRad(float deg) => deg * (float)(Math.PI / 180.0);
    }
}
