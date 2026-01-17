using UnityEngine;

public abstract class Spell
{
    float StartTime;

    public AnimationCurve ChargeCurve;
    public float ChargeScale;
    public void StartSpell()
    {
        StartTime = Time.time;
    }
    public float CurrentChargeLevel()
    {
        return ChargeScale * ChargeCurve.Evaluate(Time.time - StartTime);
    }
    public void EndSpell()
    {
        CastSpell(Time.time-StartTime);
    }

    public abstract void CastSpell(float timeSinceStart);
}
