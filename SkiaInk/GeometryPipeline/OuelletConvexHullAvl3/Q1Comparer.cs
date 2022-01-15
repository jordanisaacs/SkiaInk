using System.Collections.Generic;
using SkiaSharp;

namespace SkiaInk.GeometryPipeline.OuelletConvexHullAvl3
{
	public class Q1Comparer : IComparer<PolygonPoint>
	{
		public int Compare(PolygonPoint pt1, PolygonPoint pt2)
		{
			if (pt1.Pos.X > pt2.Pos.X) // Decreasing order
			{
				return -1;
			}
			if (pt1.Pos.X < pt2.Pos.X)
			{
				return 1;
			}

			return 0;
		}
	}
}
