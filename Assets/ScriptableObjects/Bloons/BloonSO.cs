using UnityEngine;

namespace Bloons
{
    [System.Serializable]
    public class ChildInfo
    {
        public BloonSO childBloon;
        [Min(1)] public int numChildren = 1;
    }

    [CreateAssetMenu(menuName = AssetMenuConst.ScriptableObject + nameof(BloonSO))]
    public class BloonSO : ScriptableObject
    {
        public ChildInfo[] children;
        [Min(1)] public int maxHealth = 1;
        [Min(0.01f)] public float speed = 1;
        public Vector2 size = Vector2.one;
        public Sprite sprite;
        public bool isMoab;
        
        public int GetRBE()
        {
            int sum = 1;

            foreach (ChildInfo info in children)
                if (!(info.childBloon == null || info.childBloon == this))
                    sum += info.childBloon.GetRBE() * info.numChildren;

            return sum;
        }
    }
}