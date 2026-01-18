using UnityEngine;
using UnityEngine.Events;

public class EnemyCollider : MonoBehaviour
{
    [Range(0, 5)]
    public uint damageInflictedOnContact;

    public UnityEvent<uint> collidedWithPlayer;
    public UnityEvent collidedWithEnvironment;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collidedWithPlayer.Invoke(damageInflictedOnContact);
        }

        if (other.CompareTag("Wall"))
        {
            collidedWithEnvironment.Invoke();
        }
    }
}
