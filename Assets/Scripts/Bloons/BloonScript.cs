using UnityEngine;
using UnityEngine.Splines;

namespace Bloons
{
    public class BloonScript : MonoBehaviour
    {
        public TrackInfoBase path;
        public BloonData data;
        public int spawnIndex;
        private int pathIndex = 0;
        private float percentAlongPath = 0;

        private void OnValidate()
        {
            MapSO();
        }

        void Start()
        {
            MapSO();

            if (path != null)
            {
                transform.position = path.track[pathIndex].EvaluatePosition(0);
            }
        }

        private void MapSO()
        {
            if (data == null || data.bloonObject == null)
                return;

            if (TryGetComponent(out SpriteRenderer renderer))
            {
                renderer.sprite = data.bloonObject.sprite;
            }
            if (TryGetComponent(out BoxCollider2D collider))
            {
                collider.size = data.bloonObject.size;
            }
            if (TryGetComponent(out Health health))
            {
                health.MaxHealth = data.bloonObject.maxHealth;
            }
        }

        void Update()
        {
            if (path == null)
            {
                return;
            }
            
            transform.position = GetNextPos(data.bloonObject.speed * Time.deltaTime);
        }

        private Vector3 GetNextPos(float moveDist)
        {
            float lengthRemaining = percentAlongPath * path.splineLengths[pathIndex] + moveDist - path.splineLengths[pathIndex];
            if (lengthRemaining > 0)
            {
                if (!path.GetNextSplineIndex(data.roundSubindex, ref pathIndex, out _))
                {
                    print("Reached End");
                    return transform.position;
                }

                percentAlongPath = 0;
                return GetNextPos(lengthRemaining);
            }
            return path.track[pathIndex].GetPointAtLinearDistance(percentAlongPath, moveDist, out percentAlongPath);
        }
    }
}
