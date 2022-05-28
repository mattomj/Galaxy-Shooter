using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health = 0f;

    [Serializable]
    public class HealthChangedEvent : UnityEvent<float> { }
    public HealthChangedEvent OnHealthChangedEvent;

    public float CurrentHealth 
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Damager>() && other.tag != gameObject.tag)
        {
            takeDamage();
        }
    }

    private void takeDamage()
    {
        health--;
        if (OnHealthChangedEvent != null)
        {
            OnHealthChangedEvent.Invoke(health);
        }
        else if (health < 0) 
        {
            Destroy(this.gameObject);
        }
    }
    public void TriggerDestroy()
    {
        Destroy(this.gameObject);
    }
   
}
