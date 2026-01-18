using UnityEngine;
using UnityEngine.InputSystem;


public class Spell : MonoBehaviour
{

    InputAction _CastSpellAction;

    float StartTime;

    public AnimationCurve ChargeCurve;
    public float ChargeScale;
    public float ChargeTimeScale = 1;
    private SpellType _SpellType;
    public string SpellTypeName;

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
    public void EndSpell(Transform Origin)
    {
        _SpellType.CastSpell(CurrentChargeLevel(), Origin);
    }

    

    private void Start()
    {
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
        if (_CastSpellAction.WasReleasedThisFrame())
        {
            
            //Debug.Log("spell Cast released"+ CurrentChargeLevel()+transform.position);
            EndSpell(transform);
        }
    }
}
