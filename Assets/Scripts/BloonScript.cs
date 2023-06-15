using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class BloonScript : MonoBehaviour
{
    public BloonSO bloonObject;
    public SplineContainer path;
    public int spawnIndex;
    private int pathIndex = 0;
    private float percentAlongPath = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = bloonObject.sprite;
        GetComponent<BoxCollider2D>().size = bloonObject.size;
        transform.position = path[pathIndex].EvaluatePosition(0);
    }

    // Update is called once per frame
    void Update()
    {
        var tempPos = path[pathIndex].GetPointAtLinearDistance(percentAlongPath, bloonObject.speed * Time.deltaTime, out percentAlongPath);
        
        if (percentAlongPath >= 1)
        {
            print("ReachedSplit");
            //path.KnotLinkCollection.;
            //path[pathIndex].kno
        }

        transform.position = tempPos;
    }
}
