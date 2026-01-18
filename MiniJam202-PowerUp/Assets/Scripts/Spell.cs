using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Spell : MonoBehaviour
{

    InputAction _CastSpellAction;

    float StartTime;

    public AnimationCurve ChargeCurve;
    public float ChargeScale;
    public float ChargeTimeScale = 1;
    private SpellType _SpellType;
    public string SpellTypeName;
    public int MaxProjectilesOnScreen = 1;

    List<GameObject> projectiles;

    public void OnValidate()
    {
        if(ChargeTimeScale < .001f)
        {
            ChargeTimeScale = .001f;
        }
        
    }

    


    public void StartSpell()
    {
        StartTime = Time.time;
    }
    public float CurrentChargeLevel()
    {
        return ChargeScale * ChargeCurve.Evaluate((Time.time - StartTime)/ChargeTimeScale);
    }
    

    /// <summary>
    /// Origin, Location where spell starts, and will then travel along local Z Axis
    /// </summary>
    /// <param name="Origin"></param>
    public GameObject EndSpell(Transform Origin)
    {
        return _SpellType.CastSpell(CurrentChargeLevel(), Origin);
    }

    public bool IsInCoolDown()
    {
        int i = 0;
        while (i < projectiles.Count)
        { 
            if (projectiles[i] == null || !projectiles[i].GetComponent<SpellProjectile>().IsAlive)
            {
                projectiles.RemoveAt(i);
            }
            i++;
        }

        return projectiles.Count >= MaxProjectilesOnScreen;
    }
    

    private void Start()
    {
        projectiles = new List<GameObject>();
        _SpellType = (ExplosionSpell)ScriptableObject.CreateInstance(SpellTypeName);
        _CastSpellAction = InputSystem.actions.FindAction("CastSpell");
    }
    private void Update()
    {
        if (_CastSpellAction.WasPressedThisFrame())
        {
            StartSpell();
            //Debug.Log("spell Cast pressed");
        }
        if (_CastSpellAction.WasReleasedThisFrame() && !IsInCoolDown())
        {
            
            //Debug.Log("spell Cast released"+ CurrentChargeLevel()+transform.position);
            projectiles.Add(EndSpell(transform));
        }
    }
}
