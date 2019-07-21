using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{

    public float dmg = 20;
    public float power = 100;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("Enemy"))
        {
            DamageInfo dmgInfo = new DamageInfo(dmg, power);
            col.SendMessageUpwards("Damage", dmgInfo);
            Debug.Log(dmg);
        }
    }
}
