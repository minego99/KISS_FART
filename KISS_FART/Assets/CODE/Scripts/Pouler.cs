using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pouler : MonoBehaviour
{
    NavMeshAgent m_agent;
    Collider m_collider;
    Animator m_animator;
    float m_minPoulerMovement = 0.5f;
    float m_maxPoulerMovement = 1f;
    Vector3 m_randomMovement = Vector3.zero;
    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.Warp(transform.position);
        m_collider = GetComponent<Collider>();
        m_animator = GetComponent<Animator>();
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(GameManager.s_doesattack == true)
        {
            if(collision.collider.CompareTag("Player")== true)
            {
                m_animator.SetTrigger("Death");
            }
        }
        else
        {
            if(collision.collider.CompareTag("Player") == true)
            {
                transform.position = transform.position; /*dinobacktransform*/
            }
        }
    }
    void RandomMovement()
    {   float randomX = Random.Range(m_minPoulerMovement, m_maxPoulerMovement);
        float randomZ = Random.Range(m_minPoulerMovement, m_maxPoulerMovement);
        m_randomMovement = new Vector3(randomX, 0,randomZ);
     }
    void PoulerMove()
    {
        bool destinationReached = false;
        m_agent.SetDestination(transform.position + m_randomMovement);

 
    }
    void Start()
    {
        
    }

    void Update()
    {
        PoulerMove();
    }
}
