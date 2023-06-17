using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class PlaceTowers : MonoBehaviour
{
    Vector3 mousePos;
    public Vector3 worldPosition;
    [SerializeField] GameObject monkey;
    [SerializeField] LayerMask blockedArea;
    Collider2D ACollider;
    void Start()
    {
        
    }

    private void MouseToWorldPos()
    {
        gameObject.transform.position = worldPosition;
        mousePos = Input.mousePosition;
        mousePos.z += 10f;


        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
    }
    void Update()
    {
        MouseToWorldPos();
        ACollider = Physics2D.OverlapCircle(worldPosition, 0.5f, blockedArea);
        
        if(ACollider != null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

    }
    public void PlaceTurret(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("df");
           
            if (ACollider == null)
            {
                Instantiate(monkey, worldPosition, gameObject.transform.rotation);
            }
            
        }
        
    }
}
