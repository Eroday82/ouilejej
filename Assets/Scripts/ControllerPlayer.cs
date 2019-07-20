using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
    
    public float vitesseDeplacement;
    public float puissanceDeSaut;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;
    bool isJump = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        Déplacement();
        Saut();
        MeilleurSaut();
    }

    void Déplacement()
    {
        float hAxis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(hAxis * vitesseDeplacement, rb.velocity.y);
    }
    
    void Saut()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                isJump = true;
                Vector2 jumpVector = new Vector2(0, Input.GetAxis("Jump") * puissanceDeSaut);

                rb.AddForce(jumpVector);
            }
        }
    }

    void MeilleurSaut()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sol"))
        {
            isJump = false;
            rb.velocity = Vector2.zero;
        }
    }
}
