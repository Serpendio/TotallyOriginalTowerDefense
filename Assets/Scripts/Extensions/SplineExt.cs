using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public static class SplineExt
{
    public static void GetInAndOutSplines(this SplineContainer spline, SplineKnotIndex splineKnotIndex, out IEnumerable<SplineKnotIndex> inSplines, out IEnumerable<SplineKnotIndex> outSplines)
    {
        if (!spline.KnotLinkCollection.TryGetKnotLinks(splineKnotIndex, out IReadOnlyList<SplineKnotIndex> linkedKnots))
        {
            inSplines = outSplines = Enumerable.Empty<SplineKnotIndex>();
            return;
        }

        BezierKnot fromKnot = spline[splineKnotIndex.Spline].Knots.LastOrDefault();

        inSplines = linkedKnots.Where(i => i.Knot == spline[i.Spline].Count()); // implies the last knot in the spline
        outSplines = linkedKnots.Where(i => i.Knot == 0); // implies the first knot in the spline
    }
}
