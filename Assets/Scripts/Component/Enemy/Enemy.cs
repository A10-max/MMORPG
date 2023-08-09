using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : NetworkBehaviour

{
    public float lookRadius = 10f;
    public int damageAmount = 10;
    public NetworkVariable<float> currentHealth = new NetworkVariable<float>();

    Transform target;
    NavMeshAgent agent;
    Animator animator;

    private void Start()
    {
        target = FindAnyObjectByType<Player>().transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!IsServer)
        {
            return;
        }

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance < lookRadius)
        {
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {
                FaceTarget();
                Attack();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,LookRotation, Time.deltaTime * 5f);
    }

    void Attack()
    {
        animator.SetBool("Attack", true);
        target.GetComponent<Player>().TakeDamage(damageAmount);
    }

    public void TakeDamage(float damage)
    {
        if (!IsServer)
        {
            return;
        }

        currentHealth.Value -= damage;
        if (currentHealth.Value <= 0)
        {
            Die();
        }

        else
        {
            animator.SetTrigger("Hurt");
        }
    }

    void Die()
    {
        if (IsServer)
        {
            NetworkManager.Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, lookRadius);
    }
}
