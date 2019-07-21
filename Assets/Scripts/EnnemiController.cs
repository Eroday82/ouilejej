using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : MonoBehaviour
{

    public float speed = 1;
    
    Vector2 initialPos;

    int direction = 1;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -2.2f)
        {
            direction = 1;
        }
        else if (transform.position.x > 2.2f)
        {
            direction = -1;
        }
        float hAxis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            direction *= -1;
        }
    }
}
