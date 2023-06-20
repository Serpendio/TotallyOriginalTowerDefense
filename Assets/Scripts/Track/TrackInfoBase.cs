using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public abstract class TrackInfoBase : MonoBehaviour
{
    public SplineContainer track;
    public int[] beginningSplineIndexes;
    public float[] splineLengths;

    protected virtual void OnValidate()
    {
        Setup();
    }

    protected virtual void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        if (track != null)
        {
            if (beginningSplineIndexes == null || beginningSplineIndexes.Count() == 0)
                beginningSplineIndexes = new int[1];
            else
                for (int i = 0; i < beginningSplineIndexes.Count(); i++)
                    beginningSplineIndexes[i] = Mathf.Clamp(beginningSplineIndexes[i], 0, track.Splines.Count());

            splineLengths = track.Splines.Select(s => s.GetLength()).ToArray();
        }
        else if (Application.isPlaying)
        {
            Debug.LogError("No track set");
        }
    }

    public Vector3 EvaluatePositionByDistance(float distance, int waveSubindex, ref int currentSplineIndex)
    {
        if (distance <= 0)
        {
            return track[currentSplineIndex][0].Position;
        }

        float currentLength = splineLengths[currentSplineIndex];
        float temp = distance - currentLength;

        while (temp >= 0)
        {
            distance = temp;
            if (!GetNextSplineIndex(waveSubindex, ref currentSplineIndex, out _))
                return track[currentSplineIndex][^1].Position;

            currentLength = splineLengths[currentSplineIndex];
            temp -= currentLength;
        }

        return track[currentSplineIndex].EvaluatePosition(distance / currentLength); 
    }

    public Vector3 EvaluatePositionByPercentage(float percentage, int waveSubindex, ref int currentSplineIndex)
    {
        if (percentage <= 0) return track[currentSplineIndex][0].Position;

        List<int> splineIndexes = new() { currentSplineIndex };

        while (GetNextSplineIndex(waveSubindex, ref currentSplineIndex, out _))
        {
            splineIndexes.Add(currentSplineIndex);
        }

        if (percentage >= 1) return track[currentSplineIndex][^1].Position;

        float targetLen = percentage * splineLengths.Sum();
        float currentLen = 0;
        int index = 0;
        
        foreach(int i in splineIndexes)
        {
            index = i;
            if (currentLen + splineLengths[i] < targetLen)
                currentLen += splineLengths[i];
        }

        return track[splineIndexes[index]].EvaluatePosition((targetLen - currentLen) / splineLengths[index]);
    }

    public abstract int GetSpawnSplineIndex(int waveSubindex);

    public abstract bool GetNextSplineIndex(int waveSubindex, ref int splineIndex, out Vector2Int numSplinesInAndOut);
}
