using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SpellProjectile : MonoBehaviour
{
    float projectileSpeed = .1f;
    float damageRadius = .1f;
    float damage = .1f;
    
    public void Initialize(float speed, float damageRadius, float damage)
    {
        this.projectileSpeed = speed;
        this.damageRadius = damageRadius;
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * projectileSpeed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        string[] layers = { "Targets" };
        int mask = LayerMask.GetMask(layers);
        if (mask == (mask | (1 << other.gameObject.layer))) //add environment layer later
        {
            Explode();
        }
    }
    

    void Explode()
    {
        // do damage
        string[] layers = { "Targets" };
        int mask = LayerMask.GetMask(layers);
        Collider[] hits = Physics.OverlapSphere(transform.position, damageRadius,
            mask);
        foreach (Collider hit in hits)
        {
            //hit.collider.gameObject.GetComponent<PLAYER_OR_MONSTER_SCRIPT_HERE>.Damage(this.damage);
            Debug.Log(hit.gameObject.name + this.damage);
        }
        Destroy(gameObject);
    }
}
