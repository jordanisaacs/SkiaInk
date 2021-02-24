using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaInk.Stroke
{
    /// <summary>
    /// Specifies the type of <see cref="SKInkDrawingAttributes"/> associated with the <see cref="SKInkStroke"/>
    /// </summary>
    enum SKInkDrawingAttributesKind
    {
        /// <summary>
        /// Supports attributes associated with a <see cref="SKVectorBrush"/>
        /// </summary>
        VectorBrush = 0,

        /// <summary>
        /// Supports attributes associated with a <see cref="SKRasterBrush"/>
        /// </summary>
        RasterBrush = 1
    }
}
