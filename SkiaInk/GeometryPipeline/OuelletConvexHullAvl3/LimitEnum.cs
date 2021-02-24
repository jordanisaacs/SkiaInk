using System;

namespace SkiaInk.GeometryPipeline.OuelletConvexHullAvl3
{
	[Flags]
	public enum LimitEnum
	{
		Right = 1,
		Top = 2,
		Left = 4,
		Bottom = 8,
	}
}
