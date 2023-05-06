using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager s_instance;
    [SerializeField]
    bool m_switchTimeState = false;
     [SerializeField] Transform m_directionnalLight;
    public float m_nightRotation = -10f;
    public float m_dayRotation = 25f;
    public Animator m_animator;
    public float m_maxTimeRotation = 120;
    float m_currentTimeRotation = 120;
    public Transform m_playerTransform;
    public Transform m_nestPosition;
 public static bool s_doesattack = false;
   public static bool s_patrolFarmer = false;
    
    public static int s_poulerScore = 0;
    public int m_poulerScoreToEgg = 5;
   private void Awake()
    {
        if(s_instance == null)
        {
            s_instance = this;


        }
      
        m_currentTimeRotation = m_maxTimeRotation;  
        m_animator = GetComponent<Animator>();
    }
    void TimePass()
    {
        if(m_currentTimeRotation >= 0)
        {

        m_currentTimeRotation -= Time.deltaTime;


        }
        else
        {
            m_switchTimeState = true;
            m_currentTimeRotation = m_maxTimeRotation;
        }
    }

    public void CanAttack()
    {
        s_doesattack = true;
    }

    public void CantAttack()
    {
        s_doesattack = false;
    }
    public void DoesPatrol()
    {
        s_patrolFarmer = true;
    }
    public void DontPatrol()
    {
        s_patrolFarmer = false;
    }
    public enum EGameState
    {
        Pause,
        Play
    }
    public enum ETimeState
    {
        Day,
        Night
    }
    public ETimeState m_currentTime = ETimeState.Day;
    public EGameState m_currentState = EGameState.Play;

    void SwitchDayAndNight()
    {
        if(m_switchTimeState == true)
        {
            SwitchPlayState(m_currentTime);
            m_switchTimeState = false;
        }
    }

    void SwitchPlayState(ETimeState newstate)
    {
        ExitTimeState(m_currentTime);
        newstate = m_currentTime  ;
        //Debug.Log("Switch play state is " + newstate);
        EnterTimeState(newstate);
    }
    void EnterDayState() 
    {
       // float dayRotation = 25;
       // Debug.Log("C'est le jour");
       m_animator.SetTrigger("SunRise");
       // Quaternion newRotation = Quaternion.Euler(m_dayRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
       //m_directionnalLight.transform.rotation = newRotation;

       
    } 
    void EnterNightState()
    {
       // Debug.Log("C'est la nuit");
        // float nightRotation = -10;
        m_animator.SetTrigger("SunDown");
      //  Quaternion newRotation = Quaternion.Euler(m_nightRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
      //m_directionnalLight.transform.rotation = newRotation/*;*/
    }
    public void ReturnToNest()
    {

        m_playerTransform.position = m_nestPosition.position;
    }
    void ExitDayState()
    {
       // Debug.Log("ExitDayState");
        m_currentTime = ETimeState.Night;
    }
    void ExitNightState()
    {
       // Debug.Log("ExitNightState");
        m_currentTime = ETimeState.Day;
    }
   
    void EnterTimeState(ETimeState newstate)
    {
        switch (m_currentTime)
        {
            case ETimeState.Day:
                EnterDayState();
                break;

            case ETimeState.Night:
                EnterNightState();
                break;
        }
    }
    void ExitTimeState(ETimeState newstate)
    {
        switch (m_currentTime)
        {

            case ETimeState.Day:
                ExitDayState();
               

                break;

            case ETimeState.Night:
                ExitNightState();

                
                break;
        }
    }
    void Update()
    {
        if(s_poulerScore > m_poulerScoreToEgg)
        {
            m_animator.SetTrigger("LayEgg");
        }
        TimePass();
        SwitchDayAndNight();
        //Debug.Log(m_currentTime);
        //Debug.Log("doesattack is  " + s_doesattack);
        
    }
}
