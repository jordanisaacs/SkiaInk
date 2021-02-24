using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace SkiaInk.Stroke
{
    /// <summary>
    /// Provides properties associated with the drawing of a <see cref="SKInkStroke"/>
    /// </summary>
    class SKInkDrawingAttributes
    {
        /// <summary>
        /// Gets or sets a value that indicates whether a Bezier curve or a
        /// collection of straight line segments is used to draw an <see cref="SKInkStroke"/>
        /// </summary>
        /// <value><c>true</c> if a Bezier curve is used; otherwise <c>false</c>. The default value is <c>true</c></value>
        public bool FitToCurve { get; set; } = true;

        /// <summary>
        /// Gets or sets a value that indicates whether the pressure of the contact on the digitizer surface
        /// is ignored when you draw an <see cref="SKInkStroke"/>
        /// </summary>
        /// <value><c>true</c> if pressure is ignored; otherwise <c>false</c>. The default value is <c>false</c></value>
        public bool IgnorePressure { get; set; } = false;

        /// <summary>
        /// Gets or sets a value that indicates whether the tilt (<see cref="SKInkPoint.TiltX"/>, <see cref="SKInkPoint.TiltY"/>)
        /// of the contact on the digitizer surface is ignored when you draw an <see cref="SKInkStroke"/>
        /// </summary>
        /// <value><c>true</c> if tilt is ignored; otherwise <c>false</c>. The default value is <c>false</c></value>
        public bool IgnoreTilt { get; set; } = false;

        /// <summary>
        /// Gets or sets the brush that is applied when you draw an <see cref="SKInkStroke"/> NEEDS DEFAULT
        /// </summary>
        /// <value>Either a <see cref="SKVectorBrush"/> or a <see cref="SKRasterBrush"/></value>
        public SKInkBrush Brush { get; set; }

        /// <summary>
        /// Creates a new <see cref="SKInkDrawingAttributes"/> object that is used to specify <see cref="SKInkStroke"/> attributes
        /// </summary>
        public SKInkDrawingAttributes() { }
    }
}
