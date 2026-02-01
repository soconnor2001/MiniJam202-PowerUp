using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SpellProjectile : MonoBehaviour
{
    public ParticleSystem ProjectileSystem;
    public ParticleSystem ExplosionSystem;
    public float projectileSpeed = 20f;
    float damageRadius = .1f;
    float damage = .1f;

    [SerializeField] AudioSource pew;
    [SerializeField] AudioSource boom;

    [HideInInspector]
    public bool IsAlive;
    
    public void Initialize(float damageRadius, float damage)
    {
        
        this.damageRadius = damageRadius;
        this.damage = damage;
        this.IsAlive = true;
    }

    public void Start()
    {
        Debug.Log("pew!");
        pew.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * projectileSpeed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        string[] layers = { "Targets" ,"Environment"};
        int mask = LayerMask.GetMask(layers);
        if (mask == (mask | (1 << other.gameObject.layer))) 
        {
            Explode();
        }
    }
    

    void Explode()
    {
        Debug.Log("boom!");
        boom.Play();

        ProjectileSystem.Stop();
        SetPSExplosionToRange(ExplosionSystem, damageRadius);
        ExplosionSystem.Play();
        ShowDebugSphere(damageRadius,ExplosionSystem.main.duration);
        IsAlive = false;
        projectileSpeed = 0;
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
        
        Destroy(gameObject,ExplosionSystem.main.duration);
    }

    void SetPSExplosionToRange(ParticleSystem ps, float radius)
    {
        float explosionRadius = radius*1.05f;
        float newSpeed = 100;
        float newLifeTime = explosionRadius / newSpeed;
        if(newLifeTime < .5)
        {
            newLifeTime = .5f;
            newSpeed = explosionRadius / newLifeTime;
        }
        ParticleSystem.MainModule main = ps.main;
        main.startLifetimeMultiplier = newLifeTime;
        main.startSpeedMultiplier = newSpeed;

        //Debug.Log("speed is " + newSpeed);
        float particles = 6.28f * explosionRadius; //rough circumference calculation (2*pi*R)
        //Debug.Log("radius is " + explosionRadius + " and spawning " + particles + "(+5) particles.");
        main.maxParticles = (int)particles + 5;
    }

    void ShowDebugSphere(float radius, float seconds)
    {
        GameObject sphere = Instantiate((GameObject)Resources.Load("Prefabs/DamageRadiusDebug"), transform);
        sphere.transform.localPosition=Vector3.zero;
        sphere.transform.localScale=new Vector3(2*radius, 2*radius, 2 * radius);
        Destroy(sphere, seconds);
    }
}
