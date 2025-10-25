using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class Game : GameWindow
{
    private int _vao, _vbo, _ebo, _shaderProgram;
    private float _angle = 0f;

    private readonly float[] _vertices =
    {
        -0.5f, -0.5f,
         0.5f, -0.5f,
         0.5f,  0.5f,
        -0.5f,  0.5f
    };

    private readonly uint[] _indices = { 0, 1, 2, 2, 3, 0 };

    private readonly string _vertexShaderSource = @"
    #version 330 core
    layout (location = 0) in vec2 aPos;
    uniform mat4 model;
    void main()
    {
        gl_Position = model * vec4(aPos, 0.0, 1.0);
    }";

    private readonly string _fragmentShaderSource = @"
    #version 330 core
    out vec4 FragColor;
    uniform vec3 uColor;
    void main()
    {
        FragColor = vec4(uColor, 1.0);
    }";

    public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) { }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.1f, 0.1f, 0.12f, 1.0f);

        int vs = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vs, _vertexShaderSource);
        GL.CompileShader(vs);
        CheckShaderCompileStatus(vs, "VERTEX");

        int fs = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fs, _fragmentShaderSource);
        GL.CompileShader(fs);
        CheckShaderCompileStatus(fs, "FRAGMENT");

        _shaderProgram = GL.CreateProgram();
        GL.AttachShader(_shaderProgram, vs);
        GL.AttachShader(_shaderProgram, fs);
        GL.LinkProgram(_shaderProgram);
        GL.DeleteShader(vs);
        GL.DeleteShader(fs);

        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();
        _ebo = GL.GenBuffer();

        GL.BindVertexArray(_vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.BindVertexArray(0);
        VSync = VSyncMode.On;
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.UseProgram(_shaderProgram);

        _angle += (float)args.Time * 45f;
        Matrix4 model = Matrix4.Identity;
        Matrix4 scale = Matrix4.CreateScale(1.2f, 0.8f, 1.0f);
        Matrix4 rotation = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(_angle));
        Matrix4 translation = Matrix4.CreateTranslation(0.5f, 0.0f, 0.0f);
        model = scale * rotation * translation;

        int modelLoc = GL.GetUniformLocation(_shaderProgram, "model");
        GL.UniformMatrix4(modelLoc, false, ref model);

        int colorLoc = GL.GetUniformLocation(_shaderProgram, "uColor");
        GL.Uniform3(colorLoc, new Vector3(0.0f, 0.8f, 0.7f));

        GL.BindVertexArray(_vao);
        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        if (KeyboardState.IsKeyDown(Keys.Escape))
            Close();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, Size.X, Size.Y);
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        GL.DeleteBuffer(_ebo);
        GL.DeleteBuffer(_vbo);
        GL.DeleteVertexArray(_vao);
        GL.DeleteProgram(_shaderProgram);
    }

    private void CheckShaderCompileStatus(int shader, string type)
    {
        GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
        if (success == 0)
        {
            string info = GL.GetShaderInfoLog(shader);
            Console.WriteLine($"{type} SHADER COMPILATION ERROR:\n{info}");
        }
    }
}
