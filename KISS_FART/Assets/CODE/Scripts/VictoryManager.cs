using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public Animator m_animator;
    public Camera m_gameCamera;
    public Camera m_victoryCamera;
    private void Awake()
    {
        m_animator = GetComponent<Animator> ();
    }
    void ChangeCamera()
    {
        m_gameCamera.enabled = false;
        m_victoryCamera.enabled = true;
    }
    void Win()
    {

    if(GameManager.s_instance.m_poulerScoreToEgg >= 3)
        {
            m_animator.SetTrigger("Victory");
        }
    }
    private void Update()
    {
        Win();
    }
}
