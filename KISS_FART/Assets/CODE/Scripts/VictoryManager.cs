using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public Animator m_animator;
    public Camera m_gameCamera;
    public Transform m_victoryCameraTransform;
    private void Awake()
    {
        m_animator = GetComponent<Animator> ();
        m_gameCamera.enabled = true;
        
    }
    void ChangeCamera()
    {
        m_gameCamera.transform.position = m_victoryCameraTransform.position;
       
    }
    void Win()
    {

    if(GameManager.s_instance.m_poulerScoreToEgg >= 3)
        {
            ChangeCamera();
            m_animator.SetTrigger("Victory");
        }
    }
    private void Update()
    {
        Win();
    }
}
