using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pouler : MonoBehaviour
{
    Collider m_collider;
    Animator m_animator;
    private void Awake()
    {
        m_collider = GetComponent<Collider>();
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
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
