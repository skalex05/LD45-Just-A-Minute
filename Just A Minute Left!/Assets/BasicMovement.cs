using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector2 minMaxBounds = new Vector2(-1, 1);
    public float waitTime;
    // Start is called before the first frame update


    // Update is called once per frame
    bool min = false;
    float w = 0;
    void Update()
    {
        if (w < waitTime)
        {
            w += Time.deltaTime;
            return;
        }
        if (min)
        {
            rb.velocity = Vector2.right * -speed;
            if (transform.position.x < minMaxBounds.x)
            {
                rb.velocity = Vector2.zero;
                transform.localScale = new Vector3(1, 1, 1);
                min = !min;
                w = 0;
            }
        }
        else
        {
            rb.velocity = Vector2.right * speed;
            if (transform.position.x > minMaxBounds.y)
            {
                rb.velocity = Vector2.zero;
                min = !min;
                transform.localScale = new Vector3(-1, 1, 1);
                w = 0;
            }
        }
    }
}
