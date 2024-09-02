using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator _animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.40f;
    public int atkDamage = 10;
    public float attackRate = 2f;
    float nextAtkTime = 0f;

    public AudioSource SFX;
    public AudioClip playerAttack;


    void Update()
    {
        if (Time.time >= nextAtkTime) 
        {
            if (Input.GetMouseButton(0)) // 0 = left click
            {
                SFX.clip = playerAttack;
                SFX.Play();
                Attack();
                nextAtkTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack() 
    {
        _animator.SetTrigger("Attack");

        Collider2D[] atkEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in atkEnemies) 
        {
            enemy.GetComponent<EnemyHp>().TakeDamage(atkDamage);
        }
    }
}
