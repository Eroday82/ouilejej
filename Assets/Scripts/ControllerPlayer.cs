using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerPlayer : MonoBehaviour
{
    
    public float vitesseDeplacement;
    public float puissanceDeSaut;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float fallMin;

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
        FallHandler();
    }

    void Déplacement()
    {
        float hAxis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(hAxis * vitesseDeplacement, rb.velocity.y);
    }
    
    void Saut()
    {
        float saut = Input.GetAxis("Jump");
        if (saut > 0)
        {
            if (!isJump)
            {
                isJump = true;
                Vector2 jumpVector = new Vector2(0, saut * puissanceDeSaut);

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
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // Game over!
            SceneManager.LoadScene("Menu");
        }
        else if (collision.gameObject.CompareTag("Fin"))
        {
            // Bravo
            SceneManager.LoadScene("Menu");
        }
    }

    //check if the player fell
    private void FallHandler()
    {
        if (transform.position.y <= fallMin)
        {
            // Game over!
            SceneManager.LoadScene("Menu");
        }
    }
}
