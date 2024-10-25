using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableProps : MonoBehaviour
{
    [SerializeField] private float propHealth;

    public void TakeDamage(float dmg)
    {
        propHealth -= dmg;

        if(propHealth <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
