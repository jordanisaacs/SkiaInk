using System.Collections;
using System.Collections.Generic;
using SkiaInk.GeometryPipeline.OuelletConvexHullAvl3.AvlTreeSet;
using SkiaSharp;

namespace SkiaInk.GeometryPipeline.OuelletConvexHullAvl3
{
	/// <summary>
	/// Does not support multihtread
	/// </summary>
	public class ConvexHullEnumerator : IEnumerator<PolygonPoint>
	{
		// ******************************************************************
		private ConvexHull _convexHull;

		private Quadrant _currentQuadrant = null;
		private AvlNode<PolygonPoint> _currentNode = null;

		// ******************************************************************
		public ConvexHullEnumerator(ConvexHull convexHull)
		{
			_convexHull = convexHull;
		}

		// ******************************************************************
		public PolygonPoint Current => _currentNode.Item;

		object IEnumerator.Current => _currentNode.Item;

		// ******************************************************************
		public void Dispose()
		{

		}

		// ******************************************************************
		public bool MoveNext()
		{
			if (! _convexHull.IsInitDone)
			{
				return false;
			}

			if (_currentQuadrant == null)
			{
				_currentQuadrant = _convexHull._q1;
				_currentNode = _currentQuadrant.GetFirstNode();
			}
			else
			{
				for (;;)
				{
					AvlNode<PolygonPoint> nextNode = _currentNode.GetNextNode();
					if (nextNode == null)
					{
						if (_currentQuadrant == _convexHull._q4)
						{
							return false;
						}

						_currentQuadrant = _currentQuadrant.GetNextQuadrant();

						nextNode = _currentQuadrant.GetFirstNode();

						if (nextNode.Item == _currentNode.Item)
						{
							_currentNode = nextNode;
							return MoveNext();
						}
					}
					else
					{
						_currentNode = nextNode;
						break;
					}
				}
			}

			return true;
		}

		// ******************************************************************
		public void Reset()
		{
			_currentQuadrant = null;
			_currentNode = null;
		}

		// ******************************************************************

	}
}
