using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo : MonoBehaviour
{
    float damage;
    float power;
    
    public DamageInfo(float dmg, float pwr)
    {
        damage = dmg;
        power = pwr;
    }

    public float dmg()
    {
        return this.damage;
    }

    public float pwr()
    {
        return this.power;
    }
}
