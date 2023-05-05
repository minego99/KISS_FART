using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    bool m_switchTimeState = false;
     [SerializeField] Light m_directionnalLight;

    private void Awake()
    {
       
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
        m_currentTime = newstate;
        EnterTimeState(newstate);
    }
    void EnterDayState() 
    {
        float dayRotation = 25;
        Debug.Log("C'est le jour");
    //    m_directionnalLight.transform.rotation.x = dayRotation;
       ;
    }
    void ExitDayState()
    {

    }
    void ExitNightState()
    {

    }
    void EnterNightState()
    {
        Debug.Log("C'est la nuit");
        float currentRotation = m_directionnalLight.transform.rotation.x;
        currentRotation = -10;
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
  
    // Update is called once per frame
    void Update()
    {
        SwitchDayAndNight();
        Debug.Log(m_currentTime);
    }
}
