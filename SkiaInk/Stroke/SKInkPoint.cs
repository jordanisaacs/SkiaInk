using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace SkiaInk.Stroke
{
    /// <summary>
    /// Provides raw input data for a single point used in the construction of an <see cref="SKInkStroke"/>
    /// </summary>
    public class SKInkPoint
    {
        /// <summary>
        /// The X, Y coordinates of the <see cref="SKInkPoint"/>, in device-independent pixel (DIP)
        /// relative to the upper left-hand corner of the inking area.
        /// </summary>
        /// <value>The X, Y coordinates relative to the upper left-hand corner of the inking area</value>
        public SKPoint Position { get; }

        /// <summary>
        /// The pressure of the contact on the digitizer surface for the <see cref="SKInkPoint"/>
        /// </summary>
        /// <value>The pressure of the contact</value>
        public float Pressure { get; }

        /// <summary>
        /// Gets the plane angle between the Y-Z plane and the plane containing the Y axis and
        /// the axis of the input device
        /// </summary>
        /// <value>A value in the range of -90 to +90 degrees. A positive X tilt is to the right</value>
        public float TiltX { get; }

        /// <summary>
        /// Gets the plane angle between the X-Z plane and the plane containing the X axis and
        /// the axis of the input device.
        /// </summary>
        /// <value>A value in the range of -90 to +90 degrees. A positive Y tilt is toward the user.</value>
        public float TiltY { get; }

        /// <summary>
        /// Gets the elapsed time since the previous <see cref="SKInkPoint"/> of an <see cref="SKInkStroke"/>
        /// was placed, 0 when <see cref="SKInkStroke"/> is pasted or loaded.
        /// </summary>
        /// <value>The time, relative to the previous <see cref="SKInkPoint"/>, in microseconds</value>
        public ulong Timestamp { get; }

        /// <summary>
        /// Creates a basic <see cref="SKInkPoint"/> object used in the construction of a
        /// <see cref="SKInkStroke"/>
        /// </summary>
        /// <param name="position">The screen coordinates for the <see cref="SKInkPoint"/> object</param>
        /// <param name="pressure">The pressure of the contact on the digitizer surface. The default is 0.5</param>
        public SKInkPoint(SKPoint position, float pressure)
        {
            Position = position;
            Pressure = pressure;
        }

        /// <summary>
        /// Creates a complex <see cref="SKInkPoint"/> object used in the construction of an <see cref="SKInkStroke"/>
        /// </summary>
        /// <param name="position">The screen coordinates for the <see cref="SKInkPoint"/> object</param>
        /// <param name="pressure">The pressure of the contact on the digitizer surface. The default is 0.5</param>
        /// <param name="tiltX">The plane angle between the Y-Z plane and the plane containing the Y axis and the axis of the input device. The default is 0.</param>
        /// <param name="tiltY">The plane angle between the X-Z plane and the plane containing the X axis and the axis of the input device. The default is 0.</param>
        /// <param name="timestamp">The time, relative to the previous <see cref="SKInkPoint"/> of an <see cref="SKInkStroke"/>, 0 when an entire <see cref="SKInkStroke"/> is pasted or loaded</param>
        public SKInkPoint(SKPoint position, float pressure, float tiltX, float tiltY, ulong timestamp)
        {
            Position = position;
            Pressure = pressure;
            TiltX = tiltX;
            TiltY = tiltY;
            Timestamp = timestamp;
        }
    }
}
