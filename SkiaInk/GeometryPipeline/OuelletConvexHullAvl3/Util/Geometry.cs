using System.Runtime.CompilerServices;
using SkiaSharp;

namespace SkiaInk.GeometryPipeline.OuelletConvexHullAvl3.Util
{
	public class Geometry
	{
		// ******************************************************************
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float CalcSlope(float x1, float y1, float x2, float y2)
		{
			//if (Math.Abs(x2 - x1) <= Double.Epsilon)
			//{
			//	return Double.NaN;
			//}

			return (y2 - y1) / (x2 - x1);
		}

		// ******************************************************************
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsPointToTheRightOfOthers(SKPoint p1, SKPoint p2, SKPoint ptToCheck)
		{
			return ((p2.X - p1.X) * (ptToCheck.Y - p1.Y)) - ((p2.Y - p1.Y) * (ptToCheck.X - p1.X)) < 0;
		}

		// ******************************************************************

	}
}
