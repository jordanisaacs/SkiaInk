using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace SkiaInk.GeometryPipeline
{
    /// <summary>
    /// Wrapper for <see cref="SKPoint"/> to associated points with polygons
    /// </summary>
    struct PolygonPoint
    {
        public float X { readonly get; set; }

        public float Y { readonly get; set; }

        /// <summary>
        /// Gets the number of the polygon associated with the <see cref="PolygonPoint"/>
        /// </summary>
        public int PolygonNum { get; }

        public PolygonPoint(SKPoint point, int polygonNum)
        {
            Position = point;
            PolygonNum = polygonNum;
        }
    }
}
