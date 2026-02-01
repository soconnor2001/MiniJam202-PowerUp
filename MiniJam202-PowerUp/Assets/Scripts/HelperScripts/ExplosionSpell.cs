using UnityEngine;


public class ExplosionSpell : ScriptableObject, SpellType
{
    GameObject Projectile;
    

    public GameObject CastSpell(float Charge, Transform Origin)
    {
        //forward is +X axis
        Projectile = Resources.Load<GameObject>("Prefabs/ExplosionSpellProjectile");
        Projectile = Instantiate(Projectile);
        Projectile.transform.position = Origin.position;
        Projectile.transform.Translate(Vector3.right,Origin);
        Projectile.transform.rotation = Origin.rotation;
        Projectile.transform.Rotate(Vector3.up, 90,Space.Self);
        Projectile.GetComponent<SpellProjectile>().Initialize(Charge, Damage(Charge));
        return Projectile;
    }

    public float Damage(float Charge)
    {
        return Charge;
    }
}
