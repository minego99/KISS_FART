using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    Collider m_collider;
    private void Awake()
    {
        m_collider = GetComponent<Collider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Egg") == true)
        {
            Debug.Log("Egg +1");
           GameManager.s_instance.m_currentEggsNumber++;
        }
    }




    private void Update()
    {
    
    }
}