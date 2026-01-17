using UnityEngine;
using UnityEngine.Events;

public class EnemyCollider : MonoBehaviour
{
    public UnityEvent<uint> collidedWithEnemy;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collidedWithEnemy.Invoke(1);
        }
    }

}
