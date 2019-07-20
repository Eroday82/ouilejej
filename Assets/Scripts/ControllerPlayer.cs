using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{

    // Vitesse de déplacement 
    public float vitesseDeplacement;

    // Vitesse de saut
    public float vitesseDeSaut;

    //Rigidbody component
    Rigidbody2D rb;

    //flag to keep track of key pressing
    bool pressedJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Déplacement();
        Saut();
    }

    void Déplacement()
    {
        // Input on x (Horizontal)
        float hAxis = Input.GetAxis("Horizontal");

        // Movement vector
        Vector2 movement = new Vector2(hAxis * vitesseDeplacement * Time.deltaTime, 0);

        // Calculate the new position
        Vector2 newPos = (Vector2) transform.position + movement;

        // Déplacement
        rb.MovePosition(newPos);
    }

    // takes care of the jumping logic
    void Saut()
    {
        // Input on the Jump axis
        float jAxis = Input.GetAxis("Jump");

        // If the key has been pressed
        if (jAxis > 0)
        {
            
                pressedJump = true;

                //jumping vector
                Vector2 jumpVector = new Vector2(0, jAxis * vitesseDeSaut);

                //apply force
                rb.AddForce(jumpVector, ForceMode2D.Impulse);
        }
        else
        {
            //set flag to false
            pressedJump = false;
        }
    }

}
