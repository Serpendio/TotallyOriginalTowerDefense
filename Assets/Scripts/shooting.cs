using System;
using UnityEngine;
public class shooting : MonoBehaviour
{
    //needed
    float currentTime;
    public Transform firePoint;

    //temp
    Vector3 mousePos;
    public Vector3 mousePosWorld;
    
    //changeables
    public float dartSpeed;
    public float shootDelay;
    [SerializeField] GameObject dartPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseToWorldPos();
        lockOn();
        currentTime += Time.deltaTime;
        if (currentTime >= shootDelay)
        {
            GameObject dart = Instantiate(dartPrefab, firePoint.position, firePoint.rotation);
            dart.GetComponent<Rigidbody2D>().AddForce(firePoint.right * dartSpeed, ForceMode2D.Impulse);
            currentTime= 0;
        }
    }
    //temp
    private void MouseToWorldPos()
    {
        
        mousePos = Input.mousePosition;
        mousePos.z += 10f;
        mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
    }
    void lockOn()
    {                           
        Vector3 distanceVector = mousePosWorld/*<-- this is the target the the towers focus on*/ - firePoint.position;
        float angle = MathF.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
