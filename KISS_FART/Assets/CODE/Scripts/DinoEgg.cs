using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoEgg : MonoBehaviour
{
    bool m_isEggOnBack = false;
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Nest") == true)
        {
            
        }

        if (GameManager.s_doesattack == false)
        {
            if (other.CompareTag("Mouth") == true && m_isEggOnBack == false)
            {
                TakeEggOn();
            }
        }
        else
        {
            if (other.CompareTag("Mouth") == true)
            {
                TakeEggOff();
            }
        }

       

    }
    void TakeEggOff()
    {
        m_isEggOnBack = false;
    }
    void TakeEggOn()
    {
      //  Debug.Log("Egg si now on");
        m_isEggOnBack = true;
    }
    private void Update()
    {
        
        if (m_isEggOnBack == true)
        {
            Debug.Log("Go on back");
            transform.position = DinoMovement.s_instance.m_dinoBack.position;
        }
        if (m_isEggOnBack == false)
        {
            Debug.Log("GOes not on back");
            transform.position = transform.position;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if(m_isEggOnBack == true)
            {
                transform.position = transform.position;
                m_isEggOnBack = false;
            }
        }
    }




}



