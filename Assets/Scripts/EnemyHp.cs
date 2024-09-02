using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;

    public Animator _animator;

    public AudioSource SFX;
    public AudioClip slimeHit, slimeDead;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        _animator.SetTrigger("isHurt");
        SFX.clip = slimeHit;
        SFX.Play();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            SFX.clip = slimeDead;
            SFX.Play();
            StartCoroutine(EnemyDeath());
        }
    }

    IEnumerator EnemyDeath()
    {
        _animator.SetBool("isDead", true);
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        Destroy(gameObject);
    }
}
