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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Déplacement();
    }

    void Déplacement()
    {
        // Input on x (Horizontal)
        float hAxis = Input.GetAxis("Horizontal");

        // Input on z (Vertical)
        float vAxis = Input.GetAxis("Vertical");

        // Movement vector
        Vector2 movement = new Vector2(hAxis * vitesseDeplacement * Time.deltaTime, 0);

        // Calculate the new position
<<<<<<< HEAD
        Vector2 newPos = (Vector2) transform.position + movement;

        // Déplacement
        rb.MovePosition(newPos);
=======
        //Vector2 newPos = transform.position + movement;

        // Move
        //rb.MovePosition(newPos);

        //check that we are moving
        if (hAxis != 0 || vAxis != 0)
        {
            //movement direction
            Vector3 direction = new Vector3(hAxis, 0, vAxis);

            //option1 : modify the transform
            //transform.forward = direction;

            //option 2: using our rigid body
            rb.rotation = Quaternion.LookRotation(direction);

        }
>>>>>>> 55b2dd5c9230eeb7e82f2e30c99bbab7d01292c1
    }

}
