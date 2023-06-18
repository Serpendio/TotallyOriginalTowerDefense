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
        MapSO();
        transform.position = path[pathIndex].EvaluatePosition(0);
    }
    private void OnValidate()
    {
        MapSO();
    }
    private void MapSO()
    {
        if (bloonObject == null)
            return;

        if (TryGetComponent(out SpriteRenderer renderer))
        {
            renderer.sprite = bloonObject.sprite;
        }
        if (TryGetComponent(out BoxCollider2D collider))
        {
            collider.size = bloonObject.size;
        }
        if (TryGetComponent(out Health health))
        {
            health.MaxHealth = bloonObject.maxHealth;
        }
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
