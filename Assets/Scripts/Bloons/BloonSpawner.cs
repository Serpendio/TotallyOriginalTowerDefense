using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloons
{
    public class BloonSpawner : MonoBehaviour
    {
        [System.Serializable]
        public class WaveSegment
        {
            public BloonData bloonData;
            public float spawnDelay;
            public int numBloons;

            public WaveSegment(BloonData bloonData, float spawnDelay, int numBloons)
            {
                this.bloonData = bloonData;
                this.spawnDelay = spawnDelay;
                this.numBloons = numBloons;
            }
        }

        [SerializeField] BloonScript bloonBase;

        [SerializeField] List<WaveSegment> waveData = new();
        public TrackInfoBase trackInfo;

        private void Start()
        {
            BeginWave();
        }

        [ContextMenu("BeginWave")]
        private void BeginWave()
        {
            if (!Application.isPlaying || trackInfo == null)
            {
                return;
            }
            StartCoroutine(SpawnWave());
        }

        [ContextMenu("StopWaves")]
        private void StopWaves()
        {
            if (!Application.isPlaying || trackInfo == null)
            {
                return;
            }
            StartCoroutine(SpawnWave());
        }

        public void PushWave(List<WaveSegment> waveData)
        {
            this.waveData = waveData;
            StartCoroutine(SpawnWave());
        }

        private IEnumerator SpawnWave()
        {
            int spawnIndex = 0;
            for (int i = 0; i < waveData.Count; i++)
            {
                for (int j = 0; j < waveData[i].numBloons; j++)
                {
                    spawnIndex++;
                    yield return new WaitForSeconds(waveData[i].spawnDelay);
                    SpawnBloon(waveData[i].bloonData, spawnIndex);
                }
            }
        }

        private void SpawnBloon(BloonData data, int spawnIndex)
        {
            var script = Instantiate(bloonBase);
            script.spawnIndex = spawnIndex;
            script.data = data;
            script.path = trackInfo;
        }
    }
}