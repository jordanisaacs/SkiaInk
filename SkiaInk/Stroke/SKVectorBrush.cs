using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace SkiaInk.Stroke
{
    class SKVectorBrush : SKInkBrush
    {
        public SKPaint Paint;
        public SKVectorBrushShape Shape;
        public SKPoint[] BrushCoords;
        public SKMatrix BrushTransform;
    }
}
