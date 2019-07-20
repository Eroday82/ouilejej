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
    }

    void Déplacement()
    {
        float hAxis = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(hAxis * vitesseDeplacement * Time.deltaTime, 0);
        Vector2 newPos = (Vector2) transform.position + movement;
        rb.MovePosition(newPos);
    }
    
    void Saut()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * puissanceDeSaut;
            Vector2 jumpVector = new Vector2(0, Input.GetAxis("Jump") * puissanceDeSaut);
            rb.AddForce(jumpVector);
        }
    }

    //void MeilleurSaut()
    //{
    //    if (rb.velocity.y < 0)
    //    {
    //        rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    //    }
    //    else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
    //    {
    //        rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

    //    }
    //}
}
