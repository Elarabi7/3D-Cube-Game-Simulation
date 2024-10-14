using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {
        Bitmap off;

        _3D_Model cube = new _3D_Model();
        List<Kwtsh> kwtsh = new List<Kwtsh>();
        List<_3D_Model> Plane = new List<_3D_Model>();
        

        Camera Cam;
        Timer t = new Timer();
        Random r = new Random();

       
        int cubePos = 0, caminc = 10 ;
        int Step = 0, Rct = 0, Lct = 0, ct = 0;
        bool Rclicked = false;
        bool RC = false;
        bool LC = false;
        bool Lclicked = false;
        bool flags = false;
        bool flaglose = false;

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += Form1_KeyDown;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            t.Tick += Tt_Tick;
           t.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Right && cube.L_3D_Pts[0].X < 200)
            {
                RC = true;
            }      
            if (e.KeyCode == Keys.Left && cube.L_3D_Pts[2].X > -400)
            {
                LC = true;
            }
            
                DrawDubb(this.CreateGraphics());
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            Transformation.Scale(cube.L_3D_Pts, 1f, 1f, 1f);

            
            Cam.cop.Y += caminc;

            
            if (Rclicked == false && Lclicked == false &&  flaglose == false)
            {
               {
                   _3D_Point v1 = new _3D_Point(Plane[Step].L_3D_Pts[2]);
                   _3D_Point v2 = new _3D_Point(Plane[Step].L_3D_Pts[3]);
          
                   Transformation.RotateArbitrary(cube.L_3D_Pts, v1, v2, -10);
          
                   ct++;
          
                   if (ct == 9 && !RC && !LC)
                   {
                       Step += 8;
                       ct = 0;
                       cubePos += 8;
                   }
                   if (ct == 9 && RC && !Rclicked)
                   {
                       Rclicked = true;
                       Step += 8;
                       ct = 0;
                       cubePos += 8;
                   }
                   if (ct == 9 && LC && !Lclicked)
                   {
                       Lclicked = true;
                       Step += 8;
                       ct = 0;
                       cubePos += 8;
                   }
               }
            }
            if (Rclicked == true)
            {
                _3D_Point v1 = new _3D_Point(Plane[Rct].L_3D_Pts[1]);
                _3D_Point v2 = new _3D_Point(Plane[Rct].L_3D_Pts[2]);
            
                Transformation.RotateArbitrary(cube.L_3D_Pts, v1, v2, -10);
            
                ct++;
                Cam.cop.Y -= 2;
                if (ct == 9)
                {
                    Rct += 1;
                    Lct += 1;
                    ct = 0;
                    RC = false;
                    Rclicked = false;
                    Lclicked = false;
                    cubePos += 1;
                }
            
            }
            if (Lclicked == true)

            {
                _3D_Point v1 = new _3D_Point(Plane[Lct].L_3D_Pts[3]);
                _3D_Point v2 = new _3D_Point(Plane[Lct].L_3D_Pts[0]);

                Transformation.RotateArbitrary(cube.L_3D_Pts, v1, v2, -10);

                ct++;
                Cam.cop.Y -= 2;
                if (ct == 9)
                {
                    Rct -= 1;
                    Lct -= 1;
                    ct = 0;
                    t.Start();
                    LC = false;
                    Lclicked = false;
                    Rclicked = false;
                    cubePos -= 1;
                }

            }
                  
            if (Plane[cubePos].f == 1)
            {
                flaglose = true;
                caminc = 0;
               Transformation.TranslateZ(cube.L_3D_Pts, 10);
                
            }

           
            DrawDubb(this.CreateGraphics());
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            int x = -500;
            int y = -600;
            CreateCamera();

            for (int i = 1; i <= 1000; i++)
            {
                _3D_Model pnn = new _3D_Model();
                pnn.f = 0;
                CreateMap(pnn, x, y, 0);
                x += 100;
                Plane.Add(pnn);

                if (i % 8 == 0)
                {
                    x -=800 ;
                    y += 100;
                }
            }
            CreateCube(cube, -500, -600, 0);
            Transformation.Scale(cube.L_3D_Pts, 1f, 1f, 1f);

            int rand;
            int kwtshno = r.Next(100);
            for (int i = 0; i < kwtshno; i++)
            {
                
                rand = r.Next(5,1000);
                Kwtsh pnn = new Kwtsh();
                pnn.cam = Cam;
                pnn.Design();
                kwtsh.Add(pnn);

                Transformation.TranslateY(kwtsh[i].L_3D_Pts, Plane[rand].L_3D_Pts[0].Y + 50);
                Transformation.TranslateX(kwtsh[i].L_3D_Pts, Plane[rand].L_3D_Pts[0].X + 50);
                Plane[rand].f = 1;
            }
        }

        void CreateMap(_3D_Model pln, float XS, float YS, float ZS)
        {
            float[] vert =
                            {
                                0     ,0     ,0,
                                100   ,0     ,0,
                                100   ,100   ,0,
                                0     ,100   ,0
                            };


            _3D_Point pnn;
            int j = 0;
            for (int i = 0; i < 4; i++)
            {
                pnn = new _3D_Point(vert[j] + XS, vert[j + 1] + YS, vert[j + 2] + ZS);
                j += 3;
                pln.AddPoint(pnn);
            }


            int[] Edges = {
                              0,1 ,
                              1,2 ,
                              2,3 ,
                              3,0
                          };
            j = 0;
           
            for (int i = 0; i < 4; i++)
            {
                pln.AddEdge(Edges[j], Edges[j + 1], Color.Purple);

                j += 2;
            }
            pln.cam = Cam;
        }

        void CreateCube(_3D_Model cb, float XS, float YS, float ZS)
        {
            float[] vert =
        {
                                0     ,0   ,0,
                                100   ,0   ,0,
                                100   ,100 ,0,
                                0     ,100 ,0,

                                0     ,0   ,-100,
                                100   ,0   ,-100,
                                100   ,100 ,-100,
                                0     ,100 ,-100,


        };


            _3D_Point pnn;
            int j = 0;
            for (int i = 0; i < 8; i++)
            {
                pnn = new _3D_Point(vert[j] + XS, vert[j + 1] + YS, vert[j + 2] + ZS);
                j += 3;
                cb.AddPoint(pnn);
            }


            int[] Edges = {
                              0,1 ,
                              1,2 ,
                              2,3 ,
                              3,0 ,

                              4,5,
                              5,6,
                              6,7,
                              7,4,

                              0,4,
                              1,5,
                              2,6,
                              3,7

                          };
            j = 0;
           
            for (int i = 0; i < 12; i++)
            {
                cb.AddEdge(Edges[j], Edges[j + 1], Color.LightPink);

                j += 2;
            }

            cb.cam = Cam;
            Cam.cop.Y -= 500;
            Cam.cop.Z -= 100;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        void CreateCamera()
        {
            Cam = new Camera();

            Cam.cxScreen = 70;
            Cam.cyScreen = 70;

            Cam.ceneterX = this.ClientSize.Width / 2;
            Cam.ceneterY = this.ClientSize.Height / 2;

            Cam.BuildNewSystem();

        }

    
        void DrawScene(Graphics g)
        {

            g.Clear(Color.Black);
            for (int i = 0; i < Plane.Count; i++)
            {
                Plane[i].DrawYourSelf(g);
            }
            for (int i = 0; i < kwtsh.Count; i++)
            {
                kwtsh[i].DrawYourSelf(g);
            }
            cube.DrawYourSelf(g);
            if(flaglose== true)
            {
                g.DrawString("Game Over!", new Font("Eras Demi ITC", 50), Brushes.White, 250, ClientSize.Height / 2 - 100);
            }
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
