using UnityEngine;

[CreateAssetMenu(fileName = "bloon", menuName = "ScriptableObjects/Bloon")]
public class BloonSO : ScriptableObject
{
    [System.Serializable]
    public class ChildInfo
    {
        public BloonSO childBloon;
        [Min(1)] public int numChildren;
    }

    public ChildInfo[] children;
    public BloonSO parentBloon;
    [Min(1)] public int maxHealth = 1;
    [Min(0)] public float speed = 1;
    public Vector2 size;
    public Sprite sprite;

    [Header("Instance Vars")]
    public bool isFortified;
    public bool isRegrow;
    public bool isCamo;
    public int roundIndex;

    public int GetRBE()
    {
        int sum = 1;

        if (children != null)
            foreach (ChildInfo info in children)
                if (!(info.childBloon == null || info.childBloon == this))
                    sum += info.childBloon.GetRBE() * info.numChildren;

        return sum;
    }
}
