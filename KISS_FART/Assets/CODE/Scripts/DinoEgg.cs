using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoEgg : MonoBehaviour
{
    public Transform m_backDinoPosition;
    bool m_isEggOnBack = false;
    private void OnTriggerStay(Collider other)
    {
        if (GameManager.s_doesattack != true)
        {
       
            if (other.CompareTag("Mouth") == true)
            {

                m_isEggOnBack = true;
                

            }
        }
  
    }
    
    private void OnEnable()
    {
        Debug.Log("enabled");
        m_backDinoPosition = DinoMovement.s_instance.m_dinoBack;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Nest"))
        {
            //seteggtonest
        }
    }
    private void GetEggOnBack()
    {
        transform.position = m_backDinoPosition.position;
    }
    void TakeEggOff()
    {
      m_isEggOnBack = false;
    }
    void TakeEggOn()
    {
        m_isEggOnBack = true;
    }
    private void Update()
    {
        if (m_isEggOnBack)
        {
            GetEggOnBack();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (m_isEggOnBack == true)
            {
                m_isEggOnBack = false;
            }
        }
   
    }
}
