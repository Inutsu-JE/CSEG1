using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float enemySpeed;
    public int pointReached;
    public float idleTime = 1f; // Adjustable idle time

    public Animator _animator;

    void Start()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    void Update() 
    {
        if (pointReached == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, enemySpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
            {
                pointReached = 1;
                if (_animator != null)
                {
                    _animator.SetBool("isMoving", false);
                }
                StartCoroutine(IdleBeforeMoving());
            }
        }

        if (pointReached == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, enemySpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
            {
                pointReached = 0;
                if (_animator != null)
                {
                    _animator.SetBool("isMoving", false);
                }
                StartCoroutine(IdleBeforeMoving());
            }
        }
    }

    IEnumerator IdleBeforeMoving()
    {
        yield return new WaitForSeconds(idleTime);
        if (_animator != null)
        {
            _animator.SetBool("isMoving", true);
        }
    }
}
