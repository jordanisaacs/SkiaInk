using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SkiaSharp;
using SkiaInk.GeometryPipeline.OuelletConvexHullAvl3;
using System.Diagnostics;

namespace SkiaInk.GeometryPipeline
{
    public static partial class PipelineMethods
    {

        /// <summary>
        /// Join two separated polygons together
        /// <see href="https://stackoverflow.com/questions/18619117/combine-nearby-polygons"/>
        /// Using a port of @brianmearns answer
        /// </summary>
        public static List<SKPoint> JoinPolygon(List<SKPoint> polygon1, List<SKPoint> polygon2)
        {
            ConvexHull convexHull = new ConvexHull();

            convexHull.CalcConvexHull(new List<SKPoint>().Concat(polygon1).Concat(polygon2).ToList().AsReadOnly());
            SKPoint[] points = convexHull.GetResultsAsArrayOfPoint();
            Debug.Print(points.ToString());

            List<SKPoint> finalPolygon = new List<SKPoint>();
            finalPolygon.Add(points[0]);
            (List<SKPoint>, int) poly = polygon1.Contains(points[0]) ? (polygon1, FindIndex(polygon1, points[0])) : (polygon2, FindIndex(polygon2, points[0]));
            int[] directions = { 0, 0 };
            for (int i = 1; i < points.Length; i++)
            {
                int polyIndex = FindIndex(poly.Item1, points[i]);
                if (polyIndex == -1)
                {
                    finalPolygon.Add(points[i]);
                    poly = poly.Item1 == polygon1 ? (polygon1, FindIndex(polygon1, points[0])) : (polygon2, FindIndex(polygon2, points[0]));
                }
                else
                {
                    int polyNum = poly.Item1 == polygon1 ? 0 : 1;
                    int direction;
                    if (directions[polyNum] == 0)
                    {
                        if (polyIndex > poly.Item2)
                        {

                            direction = poly.Item2 == 0 && polyIndex == poly.Item1.Count - 1 ? -1 : 1;
                        }
                        else
                        {
                            direction = poly.Item2 == poly.Item1.Count - 1 && polyIndex == 0 ? 1 : -1;
                        }
                        directions[polyNum] = direction;
                    }
                    else
                    {
                        direction = directions[polyNum];
                    }

                    int v = poly.Item2;
                    while (v != poly.Item2)
                    {
                        v += direction;
                        if (v >= poly.Item1.Count) v = 0;
                        else if (v == -1) v = poly.Item1.Count - 1;
                        finalPolygon.Add(poly.Item1[v]);
                    }
                }
            }

            return finalPolygon;
        }

        private static int FindIndex(List<SKPoint> list, SKPoint point)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == point) return i;
            }
            return -1;
        }

    }
}
