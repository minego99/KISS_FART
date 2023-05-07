using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
     public static GameManager s_instance;
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
    public int m_maxEggsNumber = 3;
    public int m_currentEggsNumber = 0;
   public  GameObject m_nestGameObject;
    public static int s_poulerScore = 0;
    public int m_poulerScoreToEgg = 5;
    public bool m_hasLayedAnEgg = false;
    
    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;


        }

        m_currentTimeRotation = m_maxTimeRotation;
        m_animator = GetComponent<Animator>();
    }
    void TimePass()
    {
        if (m_currentTimeRotation >= 0)
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
        if (m_switchTimeState == true)
        {
            SwitchPlayState(m_currentTime);
            m_switchTimeState = false;
        }
    }

    void SwitchPlayState(ETimeState newstate)
    {
        ExitTimeState(m_currentTime);
        newstate = m_currentTime;
        //Debug.Log("Switch play state is " + newstate);
        EnterTimeState(newstate);
    }
    void EnterDayState()
    {
        m_animator.SetTrigger("SunRise");
 

    }

   
    void EnterNightState()
    {
        m_animator.SetTrigger("SunDown");
       }
    public void ReturnToNest()
    {

        m_playerTransform.position = m_nestPosition.position;
    }
    void ExitDayState()
    {
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
        //if (Input.GetButtonDown("Fire2"))
        //    {

        //        Debug.Log("win");
        //        SceneManager.LoadScene("VictoryScene", LoadSceneMode.Single);
        //    }
        if (m_currentEggsNumber >= m_maxEggsNumber)
        {
           
            Debug.Log("win");
            SceneManager.LoadScene("VictoryScene", LoadSceneMode.Single);
            // m_animator.SetTrigger("WIN");
        }
        if (s_poulerScore > m_poulerScoreToEgg)
        {
            m_animator.SetTrigger("LayEgg");
        }
        TimePass();
        SwitchDayAndNight();
        //Debug.Log(m_currentTime);
        //Debug.Log("doesattack is  " + s_doesattack);

    }
}
