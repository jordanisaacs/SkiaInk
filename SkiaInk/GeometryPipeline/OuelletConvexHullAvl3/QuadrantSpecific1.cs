using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SkiaSharp;
using SkiaInk.GeometryPipeline.OuelletConvexHullAvl3.AvlTreeSet;

namespace SkiaInk.GeometryPipeline.OuelletConvexHullAvl3
{
	public class QuadrantSpecific1 : Quadrant
	{
		// ************************************************************************
		public const string QuadrantName = "Quadrant 1";

		// ************************************************************************
		public QuadrantSpecific1(ConvexHull convexHull, IReadOnlyList<PolygonPoint> listOfPoint) : base(convexHull, listOfPoint, new Q1Comparer())
		{
			Name = QuadrantName;
		}

		// ******************************************************************
		private QuadrantSpecific1()
		{
		}

		// ******************************************************************
		public override Quadrant Clone()
		{
			var q = new QuadrantSpecific1();
			this.CopyTo(q);
			return q;
		}

		// ******************************************************************
		protected override void SetQuadrantLimits()
		{
			PolygonPoint firstPoint = this.ListOfPoint.First();

			FirstPoint = firstPoint;
			LastPoint = firstPoint;

			foreach (var point in ListOfPoint)
			{
				if (point.Pos.X >= FirstPoint.Pos.X)
				{
					if (point.Pos.X == FirstPoint.Pos.X)
					{
						if (point.Pos.Y > firstPoint.Pos.Y)
						{
							FirstPoint = point;
						}
					}
					else
					{
						FirstPoint = point;
					}
				}

				if (point.Pos.Y >= LastPoint.Pos.Y)
				{
					if (point.Pos.Y == LastPoint.Pos.Y)
					{
						if (point.Pos.X > LastPoint.Pos.X)
						{
							LastPoint = point;
						}
					}
					else
					{
						LastPoint = point;
					}
				}
			}

			RootPoint = new SKPoint(LastPoint.Pos.X, FirstPoint.Pos.Y);
		}

		// ******************************************************************
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal override bool IsGoodQuadrantForPoint(PolygonPoint pt)
		{
			if (pt.Pos.X >= this.RootPoint.X && pt.Pos.Y >= this.RootPoint.Y)
			{
				return true;
			}

			return false;
		}
		
		// ******************************************************************
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal override EnumConvexHullPoint IsHullPoint(ref PolygonPoint point)
		{
			CurrentNode = Root;
			AvlNode<PolygonPoint> currentPrevious = null;
			AvlNode<PolygonPoint> currentNext = null;

			while (CurrentNode != null)
			{
				if (point.Pos.X > CurrentNode.Item.Pos.X)
				{
					if (CurrentNode.Left != null)
					{
						CurrentNode = CurrentNode.Left;
						continue;
					}

					currentPrevious = CurrentNode.GetPreviousNode();
					if (CanQuickReject(ref point, ref currentPrevious.Item))
					{
						return EnumConvexHullPoint.NotConvexHullPoint;
					}

					if (!IsPointToTheRightOfOthers(currentPrevious.Item, CurrentNode.Item, point))
					{
						return EnumConvexHullPoint.NotConvexHullPoint;
					}
				}
				else if (point.Pos.X < CurrentNode.Item.Pos.X)
				{
					if (CurrentNode.Right != null)
					{
						CurrentNode = CurrentNode.Right;
						continue;
					}

					currentNext = CurrentNode.GetNextNode();
					if (CanQuickReject(ref point, ref currentNext.Item))
					{
						return EnumConvexHullPoint.NotConvexHullPoint;
					}

					if (!IsPointToTheRightOfOthers(CurrentNode.Item, currentNext.Item, point))
					{
						return EnumConvexHullPoint.NotConvexHullPoint;
					}
				}
				else
				{
					if (point.Pos.Y <= CurrentNode.Item.Pos.Y)
					{
						if (point.Pos.Y == CurrentNode.Item.Pos.Y)
						{
							return EnumConvexHullPoint.AlreadyExists;
						}

						return EnumConvexHullPoint.NotConvexHullPoint; // invalid point
					}

					return EnumConvexHullPoint.ConvexHullPoint;
				}

				return EnumConvexHullPoint.ConvexHullPoint;
			}

			return EnumConvexHullPoint.NotConvexHullPoint;
		}

		// ******************************************************************
		/// <summary>
		/// Iterate over each points to see if we can add it has a ConvexHull point.
		/// It is specific by Quadrant to improve efficiency.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal override EnumConvexHullPoint ProcessPoint(ref PolygonPoint point)
		{
			CurrentNode = Root;
			AvlNode<PolygonPoint> currentPrevious = null;
			AvlNode<PolygonPoint> currentNext = null;

			while (CurrentNode != null)
			{
				if (CanQuickReject(ref point, ref CurrentNode.Item))
				{
					return EnumConvexHullPoint.NotConvexHullPoint;
				}

				var insertionSide = Side.Unknown;
				if (point.Pos.X > CurrentNode.Item.Pos.X)
				{
					if (CurrentNode.Left != null)
					{
						CurrentNode = CurrentNode.Left;
						continue;
					}

					currentPrevious = CurrentNode.GetPreviousNode();
					if (CanQuickReject(ref point, ref currentPrevious.Item))
					{
						return EnumConvexHullPoint.NotConvexHullPoint;
					}

					if (!IsPointToTheRightOfOthers(currentPrevious.Item, CurrentNode.Item, point))
					{
						return EnumConvexHullPoint.NotConvexHullPoint;
					}

					insertionSide = Side.Left;
				}
				else if (point.Pos.X < CurrentNode.Item.Pos.X)
				{
					if (CurrentNode.Right != null)
					{
						CurrentNode = CurrentNode.Right;
						continue;
					}

					currentNext = CurrentNode.GetNextNode();
					if (CanQuickReject(ref point, ref currentNext.Item))
					{
						return EnumConvexHullPoint.NotConvexHullPoint;
					}

					if (!IsPointToTheRightOfOthers(CurrentNode.Item, currentNext.Item, point))
					{
						return EnumConvexHullPoint.NotConvexHullPoint;
					}

					insertionSide = Side.Right;
				}
				else
				{
					if (point.Pos.Y <= CurrentNode.Item.Pos.Y)
					{
						if (point.Pos.Y == CurrentNode.Item.Pos.Y)
						{
							return EnumConvexHullPoint .AlreadyExists;
						}

						return EnumConvexHullPoint.NotConvexHullPoint; // invalid point
					}

					// Replace CurrentNode point with point
					CurrentNode.Item = point;

					InvalidateNeighbors(CurrentNode.GetPreviousNode(), CurrentNode, CurrentNode.GetNextNode());
					return EnumConvexHullPoint.ConvexHullPoint;
				}

				//We should insert the point

				// Try to optimize and verify if can replace a node instead insertion to minimize tree balancing
				if (insertionSide == Side.Right)
				{
					currentPrevious = CurrentNode.GetPreviousNode();
					if (currentPrevious != null && !IsPointToTheRightOfOthers(currentPrevious.Item, point, CurrentNode.Item))
					{
						CurrentNode.Item = point;
						InvalidateNeighbors(currentPrevious, CurrentNode, currentNext);
						return EnumConvexHullPoint.ConvexHullPoint;
					}

					var nextNext = currentNext.GetNextNode();
					if (nextNext != null && !IsPointToTheRightOfOthers(point, nextNext.Item, currentNext.Item))
					{
						currentNext.Item = point;
						InvalidateNeighbors(null, currentNext, nextNext);
						return EnumConvexHullPoint.ConvexHullPoint;
					}
				}
				else // Left
				{
					currentNext = CurrentNode.GetNextNode();
					if (currentNext != null && !IsPointToTheRightOfOthers(point, currentNext.Item, CurrentNode.Item))
					{
						CurrentNode.Item = point;
						InvalidateNeighbors(currentPrevious, CurrentNode, currentNext);
						return EnumConvexHullPoint.ConvexHullPoint;
					}

					var previousPrevious = currentPrevious.GetPreviousNode();
					if (previousPrevious != null && !IsPointToTheRightOfOthers(previousPrevious.Item, point, currentPrevious.Item))
					{
						currentPrevious.Item = point;
						InvalidateNeighbors(previousPrevious, currentPrevious, null);
						return EnumConvexHullPoint.ConvexHullPoint;
					}
				}

				// Should insert but no invalidation is required. (That's why we need to insert... can't replace an adjacent neightbor)
				AvlNode<PolygonPoint> newNode = new AvlNode<PolygonPoint>();
				if (insertionSide == Side.Right)
				{
					newNode.Parent = CurrentNode;
					newNode.Item = point;
					CurrentNode.Right = newNode;
					this.AddBalance(newNode.Parent, -1);
				}
				else // Left
				{
					newNode.Parent = CurrentNode;
					newNode.Item = point;
					CurrentNode.Left = newNode;
					this.AddBalance(newNode.Parent, 1);
				}
				return EnumConvexHullPoint.ConvexHullPoint;
			}

			return EnumConvexHullPoint.NotConvexHullPoint;
		}

		// ******************************************************************
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool CanQuickReject(ref PolygonPoint pt, ref PolygonPoint ptHull)
		{
			if (pt.Pos.X <= ptHull.Pos.X && pt.Pos.Y <= ptHull.Pos.Y)
			{
				return true;
			}

			return false;
		}

		// ******************************************************************
		internal override Quadrant GetNextQuadrant()
		{
			return ConvexHull._q2;
		}

		// ******************************************************************
		internal override Quadrant GetPreviousQuadrant()
		{
			return ConvexHull._q4;
		}

		// ******************************************************************

	}
}

