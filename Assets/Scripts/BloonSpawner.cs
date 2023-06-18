using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class BloonSpawner : MonoBehaviour
{
    [SerializeField] BloonScript bloonBase;

    public List<BloonSO> nextWave = new();
    public List<float> delays = new();
    public SplineContainer[] tracks;
    private int spawnIndex = 0;


    private void OnValidate()
    {
        int dif = nextWave.Count - delays.Count;
        if (dif > 0)
            for (int i = 0; i < dif; i++)
                delays.Add(.1f);
        else if (dif < 0)
            for (int i = 0; i < -dif; i++)
                delays.RemoveAt(delays.Count - 1);
    }

    private void Start()
    {
        SpawnWave();
    }

    [ContextMenu("SpawnWave")]
    public void SpawnWave()
    {
        if (!Application.isPlaying || tracks == null || tracks.Contains(null))
        {
            return;
        }
        StopAllCoroutines();
        spawnIndex = 0;
        for (int i = 0; i < nextWave.Count; i++)
        {
            StartCoroutine(SpawnBloon(delays[i]));
        }
    }

    private IEnumerator SpawnBloon(float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);

        var script = Instantiate(bloonBase);
        script.spawnIndex = spawnIndex;
        script.bloonObject = nextWave[spawnIndex];
        script.path = tracks[0];
        spawnIndex++;
    }
}
