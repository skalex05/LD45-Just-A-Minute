using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform jumpDetection;
    public Rigidbody2D rb;
    public float JumpHeight;
    public float MovementSpeed;
    public LayerMask ground;
    int validLayers;

    // Start is called before the first frame update
    void Start()
    {
        validLayers = 1 << 8;
    }

    // Update is called once per frame
    void Update()
    {
        float mov = Input.GetAxis("Horizontal");
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"),1,1);
        }
        rb.velocity = new Vector2(mov * MovementSpeed, rb.velocity.y);
        if (Physics2D.OverlapCircle(jumpDetection.position,0.01f,validLayers) && Input.GetAxisRaw("Vertical") > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
        }
        else if (!Physics2D.OverlapCircle(jumpDetection.position, 0.01f, validLayers) && Input.GetAxisRaw("Vertical") <= 0)
        {
            rb.velocity += Vector2.up * -JumpHeight * Time.deltaTime;
        }
    }
}
