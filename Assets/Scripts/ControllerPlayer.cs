using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerPlayer : MonoBehaviour
{
    public float lifeMax;
    private float life;
    public float energyMax;
    private float energy;

    public float vitesseDeplacement;
    public float puissanceDeSaut;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float fallMin;

    public Image barLife;
    public Image barEnergy;
    private float lifeDown;
    private float energyDown;
    
    public AudioSource source;

    public Collider2D attackCollider;
    private bool attacking;
    private float attackTimer = 0f;
    public float attackCd;
    private int directionAttack;
    private int directionAttackLast = 1;

    public Camera mainCamera;

    public Animator anim;

    
    
    private Rigidbody2D rb;
    bool isJump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackCollider.enabled = false;
        energy = energyMax;
        life = lifeMax;
        anim = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        Debug.Log(anim.GetBool("doAttack"));
        Déplacement();
        Saut();
        MeilleurSaut();
        FallHandler();
        UpdateBar();
        CheckLife();
        CameraFollow();
        attack();
    }

    void Déplacement()
    {
        float hAxis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(hAxis * vitesseDeplacement, rb.velocity.y);
        if (hAxis < 0)
        {
            directionAttack = -1;
            rb.transform.rotation = (new Quaternion(0, 180, 0, 0));

        }
        else if (hAxis > 0) 
        {
            directionAttack = 1;
            rb.transform.rotation = (new Quaternion(0, 0, 0, 0));
        }
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
            if (attacking == false)
            {
                lifeDown += 25f;
                life -= 25f;
                source.Play();
            }
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

    // La vie/mana descend dynamiquement
    private void UpdateBar()
    {
        if (lifeDown != 0)
        {
            barLife.fillAmount -= 1 / lifeMax;
            lifeDown--; 
        }
        if (energyDown > 0)
        {
            barEnergy.fillAmount -= 4 / energyMax;
            energyDown -= 4;
        }

        if (energy <= energyMax)
        {
            barEnergy.fillAmount += 0.001f;
            energy += 0.1f;
        }
    }

    private void CheckLife()
    {
        if(life <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private void CameraFollow()
    {
        mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
    }

    private void attack()
    {
        
        if (Input.GetKeyDown("f") && !attacking && energy >= 20)
        { 
            //if (directionAttack != directionAttackLast)
            //{
            //    attackCollider.transform.position = new Vector3
            //        (rb.transform.position.x + 0.8f * directionAttack, 
            //        rb.transform.position.y,
            //        rb.transform.position.z);
            //    directionAttackLast = directionAttack;
            //}

            anim.SetBool("doAttack", true);
            energy -= 20;
            energyDown += 20;
            attacking = true;
            attackTimer = attackCd;
            
            attackCollider.enabled = true;

        }
         else { ;  }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackCollider.enabled = false;
                anim.SetBool("doAttack", false);
            }
        }
    }
}
