﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project
{
    class Transformation
    {
        public static void Scale(List<_3D_Point> L_Pts, float sx, float sy, float sz)
        {
            for (int i = 0; i < L_Pts.Count; i++)
            {
                L_Pts[i].X *= sx;
                L_Pts[i].Y *= sy;
                L_Pts[i].Z *= sz;

            }
        }
        public static void RotatX(List<_3D_Point> L_Pts, float theta)
        {

            float th = (float)(Math.PI * theta / 180);
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3D_Point p = L_Pts[i];

                float x_ = p.X;
                float y_ = (float)(p.Y * Math.Cos(th) - p.Z * Math.Sin(th));
                float z_ = (float)(p.Y * Math.Sin(th) + p.Z * Math.Cos(th));

                p.X = x_;
                p.Y = y_;
                p.Z = z_;
            }
        }

        public static void RotatY(List<_3D_Point> L_Pts, float theta)
        {

            float th = (float)(Math.PI * theta / 180);
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3D_Point p = L_Pts[i];


                float x_ = (float)(p.Z * Math.Sin(th) + p.X * Math.Cos(th));
                float y_ = p.Y;
                float z_ = (float)(p.Z * Math.Cos(th) - p.X * Math.Sin(th));                

                p.X = x_;
                p.Y = y_;
                p.Z = z_;
            }
        }

        public static void RotatZ(List<_3D_Point> L_Pts, float theta)
        {

            float th = (float)(Math.PI * theta / 180);
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3D_Point p = L_Pts[i];


                float x_ = (float)(p.X * Math.Cos(th) - p.Y * Math.Sin(th));
                float y_ = (float)(p.X * Math.Sin(th) + p.Y * Math.Cos(th));
                float z_ = p.Z;

                p.X = x_;
                p.Y = y_;
                p.Z = z_;
            }
        }

        public static void TranslateX(List<_3D_Point> L_Pts, float tx)
        {
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3D_Point p = L_Pts[i];
                p.X += tx;
            }
        }

        public static void TranslateY(List<_3D_Point> L_Pts, float ty)
        {
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3D_Point p = L_Pts[i];
                p.Y += ty;
            }
        }

        public static void TranslateZ(List<_3D_Point> L_Pts, float tz)
        {
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3D_Point p = L_Pts[i];
                p.Z += tz;
            }
        }


        public static void RotateArbitrary(List<_3D_Point> L_Pts,
                                            _3D_Point v1,
                                            _3D_Point v2,
                                            float ang)
        {
            Transformation.TranslateX(L_Pts, v1.X * -1);
            Transformation.TranslateY(L_Pts, v1.Y * -1);
            Transformation.TranslateZ(L_Pts, v1.Z * -1);

            float dx = v2.X - v1.X;
            float dy = v2.Y - v1.Y;
            float dz = v2.Z - v1.Z;

            float theta = (float)Math.Atan2(dy, dx);
            float phi = (float)Math.Atan2(Math.Sqrt(dx * dx + dy * dy), dz);

            theta = (float)(theta * 180 / Math.PI);
            phi = (float)(phi * 180 / Math.PI);
            Transformation.RotatZ(L_Pts, theta * -1);
            Transformation.RotatY(L_Pts, phi * -1);

            Transformation.RotatZ(L_Pts, ang);

            Transformation.RotatY(L_Pts, phi * 1);
            Transformation.RotatZ(L_Pts, theta * 1);
            Transformation.TranslateZ(L_Pts, v1.Z * 1);
            Transformation.TranslateY(L_Pts, v1.Y * 1);
            Transformation.TranslateX(L_Pts, v1.X * 1);
        }
     /*   public static void Scale(ref ArrayList L_Model, float sx, float sy, float sz)
        {

            ArrayList L_new = new ArrayList();
            for (int i = 0; i < L_Model.Count; i++)
            {
                CPoint3D_Node v = (CPoint3D_Node)L_Model[i];
                v.X = v.X * sx;
                v.Y = v.Y * sy;
                v.Z = v.Z * sz;
                L_new.Add(v);
            }
            L_Model.Clear();
            L_Model = L_new;
        }*/

    }
}
