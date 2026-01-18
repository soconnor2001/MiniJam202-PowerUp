using UnityEngine;
using System;


public interface SpellType
{
    public GameObject CastSpell(float Charge, Transform Origin);
    public float Damage(float Charge);
}

