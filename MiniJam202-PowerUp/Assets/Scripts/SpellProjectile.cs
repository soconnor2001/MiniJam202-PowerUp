using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(SphereCollider))]
public class SpellProjectile : MonoBehaviour
{
    public ParticleSystem ProjectileSystem;
    public ParticleSystem ExplosionSystem;
    public float projectileSpeed = 20f;
    public float ExplosionLengthInSeconds = .5f;
    float damageRadius = .1f;
    float damage = .1f;
    [HideInInspector]
    public bool IsAlive;
    [HideInInspector]
    public bool IsExploding;

    private float _ExplosionStartTime = 0;
    private float _ExplosionSpeed = 0;
    private SphereCollider sphereCollider = default!;

    private void Start()
    {
        sphereCollider = gameObject.GetComponent<SphereCollider>();
        this.IsAlive = true;
    }
    public void Initialize(float damageRadius, float damage)
    {
        
        this.damageRadius = damageRadius;
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsExploding)
        {
            sphereCollider.radius += _ExplosionSpeed * Time.deltaTime;
            if (Time.time - _ExplosionStartTime > ExplosionLengthInSeconds)
            {
                sphereCollider.radius = 0;
                _ExplosionSpeed = 0;
            }
            ShowDebugSphere(sphereCollider.radius, .1f);


        }
        else if (IsAlive)
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsExploding)
        {
            string[] explodingLayers = { "Targets" };
            int explodingMask = LayerMask.GetMask(explodingLayers);
            if (explodingMask == (explodingMask | (1 << other.gameObject.layer)))
            {
                other.gameObject.GetComponent<Health>().ReceiveDamage((uint)damage);
            }
            
        }
        else
        {
            string[] movingLayers = { "Targets", "Environment" };
            int movingMask = LayerMask.GetMask(movingLayers);
            if (movingMask == (movingMask | (1 << other.gameObject.layer)))
            {
                StartExplosion();
            }
        }
            
    }
    

    void StartExplosion()
    {
        ProjectileSystem.Stop();
        SetPSExplosionToRange(ExplosionSystem, damageRadius);
        ExplosionSystem.Play();
        //ShowDebugSphere(damageRadius,ExplosionSystem.main.duration);
        IsAlive = false;
        projectileSpeed = 0;
        IsExploding = true;
        _ExplosionStartTime = Time.time;
        _ExplosionSpeed = damageRadius / ExplosionLengthInSeconds;

        Destroy(gameObject,Mathf.Max(ExplosionSystem.main.duration,ExplosionLengthInSeconds));
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

    }

    void ShowDebugSphere(float radius, float seconds)
    {
        GameObject sphere = Instantiate((GameObject)Resources.Load("Prefabs/DamageRadiusDebug"), transform);
        sphere.transform.localPosition=Vector3.zero;
        sphere.transform.localScale=new Vector3(2*radius, 2*radius, 2 * radius);
        Destroy(sphere, seconds);
    }
}
