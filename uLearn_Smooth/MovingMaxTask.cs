using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;

namespace yield;

public static class MovingMaxTask
{
	public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
	{
		LinkedList<double> potentiallyMaxs = new LinkedList<double>();
		Queue<double> windowPoints = new Queue<double>();

		foreach(var point in data)
		{
            windowPoints.Enqueue(point.OriginalY);
            if (windowPoints.Count > windowWidth)
            {
                var outOfWindow = windowPoints.Dequeue();
                potentiallyMaxs.Remove(outOfWindow);
            }

            if (potentiallyMaxs.Count == 0) 
                potentiallyMaxs.AddFirst(point.OriginalY);
			else
			{
                while (potentiallyMaxs.Count > 0 && potentiallyMaxs.Last.Value < point.OriginalY)
                {
                    potentiallyMaxs.RemoveLast();
                }
                potentiallyMaxs.AddLast(point.OriginalY);
            }

			yield return new DataPoint(point.X, point.OriginalY).WithMaxY(potentiallyMaxs.First.Value);
        }
	}
}