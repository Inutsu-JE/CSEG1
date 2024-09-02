using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBarScript healthBar;
    public Animator _animator;

    public AudioSource SFX;
    public AudioClip playerHit, playerDead;

    private bool isDead = false;

    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            SFX.clip = playerDead;
            SFX.Play();

            StartCoroutine(HandlePlayerDeath());
        }
    }

    public void TakeDamage(int dmgTaken)
    {
        if (isDead) return;

        currentHealth -= dmgTaken;
        SFX.clip = playerHit;
        SFX.Play();
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);


        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            SFX.clip = playerDead;
            SFX.Play();
            StartCoroutine(HandlePlayerDeath());
        }
    }

    IEnumerator HandlePlayerDeath()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Dead"); // Assumes you have a trigger named "Dead" in your animator
        }
        // Wait for the death animation duration
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        // Trigger the scene transition with fade out and restart
        SceneController.instance.restartLevel();
    }

}
