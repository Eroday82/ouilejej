using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnnemiController : MonoBehaviour
{

    public float speed = 1;
    public float mouvXPos;
    public float mouvXNeg;

    public float lifeMax;
    private float life;
    public Image barLife;
    private float lifeDown;

    Vector2 initialPos;

    int direction = 1;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        life = lifeMax;
    }
    
    void Update()
    {
        if(transform.position.x < mouvXNeg)
        {
            direction = 1;
        }
        else if (transform.position.x > mouvXPos)
        {
            direction = -1;
        }
        float hAxis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        CheckLife();
        UpdateBar();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            direction *= -1;
        }
    }

    public void Damage(DamageInfo dmgInfo)
    {
        Vector2 jumpVector = new Vector2(dmgInfo.pwr(), dmgInfo.pwr());
        rb.AddForce(jumpVector);

        life -= dmgInfo.dmg();
        lifeDown += dmgInfo.dmg();
    }

    private void CheckLife()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    // La vie/mana descend dynamiquement
    private void UpdateBar()
    {
        if (lifeDown != 0)
        {
            barLife.fillAmount = life / lifeMax;
            lifeDown--;
        }
    }
}
