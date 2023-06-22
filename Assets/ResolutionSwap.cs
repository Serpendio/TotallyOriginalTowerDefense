using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteAlways]
public class ResolutionSwap : UIBehaviour
{
    [SerializeField] GameObject horizontalLayout, verticalLayout;
    [SerializeField] float ratioSwapVal;

    
    protected override void OnRectTransformDimensionsChange()
    {
        if (horizontalLayout == null || verticalLayout == null) return;

        Vector2 newSize = (transform as RectTransform).sizeDelta;
        horizontalLayout.SetActive(newSize.x / newSize.y >= ratioSwapVal);
          verticalLayout.SetActive(newSize.x / newSize.y <  ratioSwapVal);
    }
}
