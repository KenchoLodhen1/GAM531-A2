using System;

public class Matrix3x3
{
    public float[,] M = new float[3, 3];

    public Matrix3x3(float[,] values)
    {
        M = values;
    }

    public static Matrix3x3 Identity()
    {
        return new Matrix3x3(new float[,] {
            {1,0,0},
            {0,1,0},
            {0,0,1}
        });
    }

    public static Matrix3x3 Scale(float sx, float sy, float sz)
    {
        return new Matrix3x3(new float[,] {
            {sx,0,0},
            {0,sy,0},
            {0,0,sz}
        });
    }

    public static Matrix3x3 RotateX(float angleDeg)
    {
        float r = MathF.PI * angleDeg / 180f;
        float c = MathF.Cos(r);
        float s = MathF.Sin(r);

        return new Matrix3x3(new float[,] {
            {1, 0, 0},
            {0, c,-s},
            {0, s, c}
        });
    }

    public static Matrix3x3 Multiply(Matrix3x3 a, Matrix3x3 b)
    {
        float[,] r = new float[3, 3];
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                r[i, j] = a.M[i, 0] * b.M[0, j] + a.M[i, 1] * b.M[1, j] + a.M[i, 2] * b.M[2, j];

        return new Matrix3x3(r);
    }

    public Vector3D MultiplyVector(Vector3D v)
    {
        float x = M[0, 0] * v.X + M[0, 1] * v.Y + M[0, 2] * v.Z;
        float y = M[1, 0] * v.X + M[1, 1] * v.Y + M[1, 2] * v.Z;
        float z = M[2, 0] * v.X + M[2, 1] * v.Y + M[2, 2] * v.Z;
        return new Vector3D(x, y, z);
    }

    public override string ToString()
    {
        return $"[{M[0, 0]:0.00} {M[0, 1]:0.00} {M[0, 2]:0.00}]\n" +
               $"[{M[1, 0]:0.00} {M[1, 1]:0.00} {M[1, 2]:0.00}]\n" +
               $"[{M[2, 0]:0.00} {M[2, 1]:0.00} {M[2, 2]:0.00}]";
    }
}
