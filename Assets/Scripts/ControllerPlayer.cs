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

        // Movement vector
        Vector2 movement = new Vector2(hAxis * vitesseDeplacement * Time.deltaTime, 0);

        // Calculate the new position
        Vector2 newPos = (Vector2) transform.position + movement;

        // Déplacement
        rb.MovePosition(newPos);
    }

}
