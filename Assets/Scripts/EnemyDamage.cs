using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public PlayerHp playerHp;
    public int damage = 10; // Amount of damage the enemy deals
    public float damageCooldown = 1.0f; // Cooldown time in seconds

    private float lastDamageTime;

    private void Start()
    {
        lastDamageTime = -damageCooldown; // Ensure the enemy can deal damage immediately when the game starts
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                playerHp.TakeDamage(damage);
                lastDamageTime = Time.time; 
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                playerHp.TakeDamage(damage);
                lastDamageTime = Time.time; 
            }
        }
    }
}
