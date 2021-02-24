using System.Collections.Generic;
using SkiaSharp;

namespace SkiaInk.GeometryPipeline.OuelletConvexHullAvl3
{
	public class Q1Comparer : IComparer<SKPoint>
	{
		public int Compare(SKPoint pt1, SKPoint pt2)
		{
			if (pt1.X > pt2.X) // Decreasing order
			{
				return -1;
			}
			if (pt1.X < pt2.X)
			{
				return 1;
			}

			return 0;
		}
	}
}
