using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;


namespace ConsoleApplication1
{

    
    public class Elka : GameWindow
    {
        
        const float RotationSpeed = 50.0f;
        float _angle;
        //матрица случайных отклонений
        readonly double[] _matr;
        
      

        public Elka(): base(640, 480)
        {
            _matr = new double[1000]; 
            var rnd = new Random();
            for (var i = 0; i < _matr.Length; i++)
            {
                _matr[i] = rnd.NextDouble() / 6 - 0.11;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.MidnightBlue);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);
            var aspectRatio = Width / (double)Height;
            var perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspectRatio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        #region OnUpdateFrame


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);



            var keyboard = OpenTK.Input.Keyboard.GetState();
            if (keyboard[Key.Escape] || keyboard[Key.Space])
            {
                Exit();
                return;
            }
        }

        #endregion

        #region OnRenderFrame

        /// <summary>
        /// Place your rendering code here.
        /// </summary>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            var camera = Matrix4.LookAt(-6, 2, 5, 0, 2, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref camera);

            _angle += RotationSpeed * (float)e.Time;
            GL.Rotate(_angle, 0.0f, 2.0f, 0.0f);

            DrawTelka();

            SwapBuffers();
            Thread.Sleep(1);
        }
       

        private void DrawTelka()
        {
            DrawBase();

            //высота елки 
            var maxY = 4;
            //шаг изменения высоты
            var stepY = 0.18;
            //длина ветки
            var z = 0.6;
            //угол поворота
            var angle = 20;
            //число повотов ветки
            var n = 12;
            //число подветок 
            var m = 3;
            //счетчик для постоянки на рандоме
            var index = 0;
            

            var v1 = new Vector3d(0, 0, 0);
            var v2 = new Vector3d(0, 0, 0);
            for (var i = 0.1; i < maxY; i += stepY)
            {
                z = z - 0.04;
                stepY+= 0.01;
          
                for (var j = 0; j < n; j++)
                {
                    //v1
                    v1.X = 0;
                    v1.Y = i * 1.0 + _matr[index++];
                    v1.Z = 0;

                    var newx = z * Math.Cos(j * angle) - z * Math.Sin(j * angle);
                    var newz = z * Math.Sin(j * angle) + z * Math.Cos(j * angle);
                    
                    //v2
                    v2.X = newx;
                    v2.Y = stepY + i + _matr[index++];
                    v2.Z = newz;
                    DrawLine(v1, v2,Color.Green);
                    
                    var v3 = Helper.GetCenterLine(v1, v2);
                    var v4 = Helper.GetCenterLine(v3, v2); 
                    v4.Y += 0.1;
                    DrawLine(v3, v4, Color.White);
                    v4.Y -= 0.2;
                    DrawLine(v3, v4, Color.Red);

              
            
                   
                    //рисуем под ветки

                }
            }
        

            
        }
        #endregion

#region draw
        public void DrawLine(Vector3d v1, Vector3d v2,Color c)
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Color4(c);
            GL.Vertex3(v1);
            GL.Vertex3(v2);
            GL.End();
        }

        void DrawBase()
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(-1.0f, 0, 0);
            GL.Vertex3(1.0f, 0, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 4.0F, 0);
            GL.Color3(Color.White);
            GL.Vertex3(0, 0, -1.0f);
            GL.Vertex3(0, 0, 1.0F);
            GL.End();
        }

#endregion
   

        [STAThread]
        public static void Main()
        {
            var process = Process.GetCurrentProcess();
            //SetParent(example.WindowInfo.Handle, process.MainWindowHandle);

            var windowInfo = Utilities.CreateWindowsWindowInfo(process.MainWindowHandle);
            var context = new GraphicsContext(GraphicsMode.Default, windowInfo);


            using (var elka = new Elka())
            {
                elka.Context.MakeCurrent(windowInfo);
                elka.Run(30, 30);
            }
       
            Console.ReadKey();
        }


    }

}