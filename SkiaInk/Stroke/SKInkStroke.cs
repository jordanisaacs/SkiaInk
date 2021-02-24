using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaInk.Stroke
{
    class SKInkStroke
    {
        private SKInkPoint[] _inkPoints;

        /// <summary>
        /// Gets or sets the properties associated with a <see cref="SKInkStroke"/>
        /// </summary>
        /// <value>The drawing attributes</value>
        public SKInkDrawingAttributes DrawingAttributes { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the <see cref="SKInkStroke"/> was started
        /// </summary>
        /// <value>The date and time of day</value>
        public Nullable<DateTimeOffset> StrokeStartedTime { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="SKInkPoint"/> objects used to construct the <see cref="SKInkStroke"/>
        /// </summary>
        /// <returns>The collection of <see cref="SKInkPoint"/> objects used to construct the <see cref="SKInkStroke"/></returns>
        public IReadOnlyList<SKInkPoint> GetInkPoints()
        {
            return Array.AsReadOnly(_inkPoints);
        }
    }
}
