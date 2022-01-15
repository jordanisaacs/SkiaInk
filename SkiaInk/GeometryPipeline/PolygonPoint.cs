using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace SkiaInk.GeometryPipeline
{
    /// <summary>
    /// Wrapper for <see cref="SKPoint"/> to associated points with polygons
    /// </summary>
    public struct PolygonPoint
    {
        public int Polygon { readonly get; set; }

        public SKPoint Pos { readonly get; set; }
        
        public PolygonPoint(int polygonNumber, SKPoint pointPos)
        {
            Polygon = polygonNumber;
            Pos = pointPos;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            
            if (!(obj is PolygonPoint))
            {
                return false;
            }

            var p = (PolygonPoint)obj;

            return p.Polygon == Polygon && p.Pos == Pos;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Polygon, Pos);
        }

        public static bool operator !=(PolygonPoint obj1, PolygonPoint obj2)
        {
            return !(obj1 == obj2);
        }

        public static bool operator ==(PolygonPoint obj1, PolygonPoint obj2)
        {
            return obj1.Equals(obj2);
        }
    }
}
