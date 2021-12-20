using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static event Action<int> OnChangeHealth;

    public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnChangeHealth?.Invoke(health);
    }

}

