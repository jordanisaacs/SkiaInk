using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace SkiaInk.GeometryPipeline
{
    public static class ExtensionMethods
    {
        public static PolygonPoint ToPolygonPoint(this SKPoint point, int polygonNum)
        {
            return new PolygonPoint(point, polygonNum);
        }
    }
}
