using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    public static Nest s_instance;
    Collider m_collider;
    public bool m_contactEgg;
    private void Awake()
    {if(s_instance == null)
        {

        s_instance =this;
        }
        m_collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Egg") == true && DinoMovement.s_instance.m_hasEgg == true) 
        {
            Debug.Log("Nest  has Egg");
            Debug.Log("Egg +1");
           GameManager.s_instance.m_currentEggsNumber++;
            m_contactEgg = true;
        }
    }

}
