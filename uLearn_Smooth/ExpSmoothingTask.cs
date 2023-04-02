using Avalonia;
using System.Collections.Generic;

namespace yield;

public static class ExpSmoothingTask
{
	public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
	{
        var isFirstPoint = true;
        double previousPointY = 0;
        foreach (var e in data)
        {
            if (isFirstPoint)
            {
                isFirstPoint = false;
                previousPointY = e.OriginalY;
            }
            else
            {
                previousPointY = alpha * e.OriginalY + (1 - alpha) * previousPointY;
            }
            var smoothedPoint = new DataPoint(e.X, e.OriginalY).WithExpSmoothedY(previousPointY);
            yield return smoothedPoint;
        }
    }
}
