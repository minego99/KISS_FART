using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Guardian : MonoBehaviour
{
    [SerializeField]
    private Patrol[] m_patrols;
    private Patrol m_currentPatrol;

    private float m_timeOnPoint;

    [SerializeField]
    private float m_travelTime;
    private float m_travelAlpha = 0;

    private bool m_isTraveling = false;

    [SerializeField]
    private PatrolPoint m_currentPoint;
    [SerializeField]
    private PatrolPoint m_nextPoint;


    // Start is called before the first frame update
    void Start()
    {
        ChangePatrol();
        m_currentPoint = m_currentPatrol.m_PatrolPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isTraveling)
        {
            StayOnPoint(m_currentPoint);
        }
        else
        {
            TravelToAnotherPoint(m_currentPoint, m_nextPoint);
        }
    }

    private void StayOnPoint(PatrolPoint point)
    {
        if(m_timeOnPoint < point.m_WaitDuration)
        {
            m_timeOnPoint += Time.deltaTime;
        }
        else
        {
            if(point == m_currentPatrol.m_PatrolPoints.Last())
            {
                ChangePatrol();
                m_nextPoint = m_currentPatrol.m_PatrolPoints[0];
            }
            else
            {
                Debug.Log("test");
                m_nextPoint = m_currentPoint.m_NextPatrolPoint;
            }
            m_travelAlpha = 0;
            m_isTraveling = true;
        }
    }

    private void TravelToAnotherPoint(PatrolPoint currentPoint, PatrolPoint nextPoint)
    {
        if(m_travelAlpha < 1)
        {
            transform.position = Vector3.Lerp(currentPoint.transform.position, nextPoint.transform.position, m_travelAlpha);
            m_travelAlpha =Mathf.Clamp01(m_travelAlpha + Time.deltaTime / m_travelTime);
        }
        else
        {
            m_timeOnPoint = 0;
            m_currentPoint = nextPoint;
            m_isTraveling = false;
        }
    }

    private void ChangePatrol()
    {
        int randomPatrol = Random.Range(0, m_patrols.Length);
        m_currentPatrol = m_patrols[randomPatrol];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
