using System;

namespace SkiaInk.GeometryPipeline.OuelletConvexHullAvl3
{
	[Flags]
	public enum EnumConvexHullPoint
	{
		NotConvexHullPoint = 0,
		AlreadyExists = 1,
		ConvexHullPoint = 2
	}
}
