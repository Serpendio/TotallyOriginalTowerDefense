using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlaceTowers : MonoBehaviour
{
    public Vector3 mousePos;
    public Vector3 worldPosition;
    
    void Start()
    {
        
    }

    
    void Update()
    {
      gameObject.transform.position = worldPosition;
      mousePos = Input.mousePosition;
        mousePos.z += 9.9f;


        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);


    }
  
}
