using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class GenericTrackInfo : TrackInfoBase
{
    public override bool GetNextSplineIndex(int waveSubindex, ref int splineIndex, out Vector2Int numSplinesInAndOut)
    {
        SplineKnotIndex fromIndex = new(splineIndex, track[splineIndex].Count() - 1);
        track.GetInAndOutSplines(fromIndex, out IEnumerable<SplineKnotIndex> inSplines, out IEnumerable<SplineKnotIndex> outSplines);

        numSplinesInAndOut = new(inSplines.Count(), outSplines.Count());
        if (outSplines.Count() == 0)
            return false;

        splineIndex = outSplines.ElementAt(Random.Range(0, outSplines.Count())).Spline;
        return true;
    }

    public override int GetSpawnSplineIndex(int waveSubindex)
    {
        return beginningSplineIndexes[(waveSubindex + 1) % beginningSplineIndexes.Length];
    }
}
