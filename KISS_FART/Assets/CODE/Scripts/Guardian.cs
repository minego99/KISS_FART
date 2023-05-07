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
    private float m_travelTime, m_distanceThreshold;
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
              //  Debug.Log("test");
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
            Vector3 currentPosition = transform.position;
            float yPos;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, m_distanceThreshold))
            {
                float distanceFromGround = hit.distance;
                yPos = currentPosition.y + (m_distanceThreshold - distanceFromGround);
                //transform.position = new Vector3(currentPosition.x, currentPosition.y + (m_distanceThreshold - distanceFromGround), currentPosition.z);
            }
            else
            {
                yPos = Mathf.Lerp(currentPoint.transform.position.y, nextPoint.transform.position.y, m_travelAlpha);
            }
            //transform.position = Vector3.Lerp(currentPoint.transform.position, nextPoint.transform.position, m_travelAlpha);
            float xPos = Mathf.Lerp(currentPoint.transform.position.x, nextPoint.transform.position.x, m_travelAlpha);
            float zPos = Mathf.Lerp(currentPoint.transform.position.z, nextPoint.transform.position.z, m_travelAlpha);
            transform.position = new Vector3(xPos, yPos, zPos);
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
