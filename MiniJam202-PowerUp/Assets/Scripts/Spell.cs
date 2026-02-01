using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Spell : MonoBehaviour
{
    public Animator animator;

    InputAction _CastSpellAction;

    float StartTime;

    public AnimationCurve ChargeCurve;
    public float ChargeScale;
    public float ChargeTimeScale = 1;
    private SpellType _SpellType;
    public string SpellTypeName = "ExplosionSpell";
    public int MaxProjectilesOnScreen = 1;
    public int isHeldDelayInt = 5;
    public int isHeldDelayBase = 5;

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
        StartTime = Time.timeSinceLevelLoad;
    }
    public float CurrentChargeLevel()
    {
        return ChargeScale * ChargeCurve.Evaluate((Time.timeSinceLevelLoad - StartTime)/ChargeTimeScale);
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
        
        if(_CastSpellAction.IsPressed() == false){
            if(isHeldDelayInt <= 0){
                animator.SetBool("IsHeld", false);
                isHeldDelayInt = isHeldDelayBase;
            }else
            {
            isHeldDelayInt -= 1;
            }
            
        }
        if(_CastSpellAction.WasPressedThisFrame()){
            animator.SetTrigger("Fire");
            animator.SetBool("IsHeld", true);
        }

        if (_CastSpellAction.WasPressedThisFrame() && !IsInCoolDown())
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
