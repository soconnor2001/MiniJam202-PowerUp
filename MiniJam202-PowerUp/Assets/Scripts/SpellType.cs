using UnityEngine;
using System;


public interface SpellType
{
    public void CastSpell(float Charge, Transform Origin);
    public float Damage(float Charge);
}

