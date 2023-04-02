using System.Collections.Generic;

namespace yield;

public static class MovingAverageTask
{
	public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
	{
		Queue<double> windowPoints = new Queue<double>();
		double windowSum = 0;
		foreach(var point in data)
		{
            windowPoints.Enqueue(point.OriginalY);
            windowSum += point.OriginalY;
			if(windowPoints.Count > windowWidth)
			{
				windowSum -= windowPoints.Dequeue();
			}
			yield return new DataPoint(point.X, point.OriginalY).WithAvgSmoothedY(windowSum / windowPoints.Count);
        }
	}
}