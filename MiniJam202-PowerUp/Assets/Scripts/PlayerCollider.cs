using UnityEngine;
using UnityEngine.Events;

public class PlayerCollider : MonoBehaviour
{
    [Range(0, 5)]
    public uint damageInflictedOnContact;

    public UnityEvent<uint> collidedWithPlayer;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collidedWithPlayer.Invoke(damageInflictedOnContact);
        }
    }
}
