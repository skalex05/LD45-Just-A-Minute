using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPOnTouch : MonoBehaviour
{
    public Vector3 Location;
    public float CameraOffset = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = Location;
        GameObject.Find("Main Camera").transform.position = Location + new Vector3(0,CameraOffset,-10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
