using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public Vector2 MaxMinSpeed;
    public Vector2 MinMaxXPos;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(MaxMinSpeed.x, MaxMinSpeed.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        if (transform.position.x >= MinMaxXPos.y)
        {
            transform.position = new Vector3(MinMaxXPos.x, transform.position.y, transform.position.z);
        }
    }
}
