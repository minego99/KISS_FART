using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoEgg : MonoBehaviour
{
    bool m_isEggOnBack = false;
   
    private void OnTriggerStay(Collider other)
    {
          if (GameManager.s_doesattack == false)
           {
            if (other == CompareTag("Mouth") && m_isEggOnBack == false)

            {
                Debug.Log("take egg on");
                TakeEggOn();
            }

            else if (other == CompareTag("Mouth") && m_isEggOnBack == true)
            {

                {
                    Debug.Log("take egg off");
                    TakeEggOff();
                }

            }

        }

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
        if (m_isEggOnBack == true)
        {
            transform.position = DinoMovement.s_instance.m_dinoBack.position;
        }
        if (m_isEggOnBack == false)
        {
            transform.position = transform.position;
        }

    }
}
