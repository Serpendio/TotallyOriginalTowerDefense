using UnityEngine;

namespace Tests
{
    public class BloonMovementTest : MonoBehaviour
    {
        [SerializeField, Min(0)] float distanceAlongTrack;
        [SerializeField, Range(0f, 1f)] float percentAlongTrack;
        [SerializeField] bool usePercentage;
        [SerializeField] TrackInfoBase track;
        [SerializeField, Min(0)] int waveSubindex;

        private void OnValidate()
        {
            if (track != null)
            {
                int splineIndex = track.GetSpawnSplineIndex(waveSubindex);

                if (usePercentage)
                    transform.position = track.EvaluatePositionByPercentage(percentAlongTrack, waveSubindex, ref splineIndex);
                else
                    transform.position = track.EvaluatePositionByDistance(distanceAlongTrack, waveSubindex, ref splineIndex);
            }
        }
    }
}
