using UnityEngine;


public class ExplosionSpell : ScriptableObject, SpellType
{

    public void CastSpell(float Charge, Transform Origin)
    {
        //forward is +X axis
        Debug.Log("boom");
    }
}
