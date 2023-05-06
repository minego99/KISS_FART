using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pouler : MonoBehaviour
{
    NavMeshAgent m_agent;
    Collider m_collider;
    Animator m_animator;

    [SerializeField]
    private float m_minimumPatrolDistance, m_patrolMaxDistance, m_navMeshSnapMaxDistance, m_minTimeOnPoint, m_maxTimeOnPoint;

    private float m_currentTimeOnPoint;
    private float m_maxTimeOnCurrentPoint;

    public Transform m_dinoBack;
    public bool m_isOnDino;
    private Vector3 m_currentDestination;

    //float m_minPoulerMovement = 0.5f;
    //float m_maxPoulerMovement = 1f;
    //Vector3 m_randomMovement = Vector3.zero;

    #region StateMachines

    public enum EEntityState
    {
        Movement,
        Taken,
        Death
    }

    private EEntityState m_currentState = EEntityState.Movement;
    #endregion

    bool m_poulerIsOnBack = false;
    #region States management

    private void OnEnterState()
    {
        switch (m_currentState)
        {
            case EEntityState.Movement:
                break;
            case EEntityState.Taken:
                break;
            case EEntityState.Death:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    private void OnExitState()
    {
        switch (m_currentState)
        {
            case EEntityState.Movement:
                break;
            case EEntityState.Taken:
                break;
            case EEntityState.Death:
                break;
            default:
                break;
        }
    }

    private void OnUpdateState()
    {
        switch (m_currentState)
        {
            case EEntityState.Movement:
                if (!m_agent.pathPending && m_agent.remainingDistance <= m_agent.stoppingDistance)
                {
                    if (!m_agent.hasPath || m_agent.velocity.sqrMagnitude == 0f)
                    {
                        if (m_currentTimeOnPoint >= m_maxTimeOnCurrentPoint)
                        {
                            //Debug.Log("Move");
                            m_currentDestination = DefinePatrolDestination();
                            m_agent.SetDestination(m_currentDestination);
                            m_maxTimeOnCurrentPoint = Random.Range(m_minTimeOnPoint, m_maxTimeOnPoint);
                            m_currentTimeOnPoint = 0;
                        }

                        else
                        {
                            m_currentTimeOnPoint += Time.deltaTime;
                            //Debug.Log(m_currentTimeOnPoint);
                        }

                    }
                    //Debug.Log("wut");
                }
                else
                {
                    //Debug.Log("out");
                }
                break;
            case EEntityState.Taken:
                break;
            case EEntityState.Death:
                break;
            default:
                break;
        }
    }

    public void OnSwitchState(EEntityState newState)
    {
        OnExitState();
        m_currentState = newState;
        OnEnterState();
    }

    #endregion

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.Warp(transform.position);
        m_collider = GetComponent<Collider>();
        m_animator = GetComponent<Animator>();
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (GameManager.s_doesattack == true)
        {
            if (other.CompareTag("Mouth") == true)
            {
                m_animator.SetTrigger("Death");
            }
        }
        else

        {

            if (other.CompareTag("Mouth") == true)
            {

                OnSwitchState(EEntityState.Taken);
                m_poulerIsOnBack = true;

            }

        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    

    void Addscore()
    {
        GameManager.s_poulerScore++;
    }
    void AddPoulerCount()
    {
        GameManager.s_poulerScore++;
    }
    private void GetPoulerOnBack()
    {
        transform.position = m_dinoBack.position;
    }


    void Update()
    {


        if (m_poulerIsOnBack)
        {
            GetPoulerOnBack();
        }
        OnUpdateState();

        if (Input.GetButtonDown("Fire1"))
        {
            if (m_poulerIsOnBack == true)
            {
                m_poulerIsOnBack = false;
            }
        }
    }

    #region Pathfinding and movement

    private Vector3 GeneratePatrolPoint(float radius)
    {
        Vector3 newPoint = Random.insideUnitSphere * radius;
        // if(!CheckPositionInRadius(newPoint, m_patrolOrigin, m_patrolRadius))
        // {
        //     newPoint = GeneratePatrolPoint(radius);
        // }
        return newPoint;
    }

    private Vector3 DefinePatrolDestination()
    {
        Vector3 newPatrolPoint = GeneratePatrolPoint(m_patrolMaxDistance) + transform.position;
        NavMeshHit patrolHit;
        int i = 0;

        //Checking closest point on the navmesh and snapping to it at https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html & https://forum.unity.com/threads/finding-closest-point-on-navmesh.284775/
        while (!NavMesh.SamplePosition(newPatrolPoint, out patrolHit, m_navMeshSnapMaxDistance, -1) || CheckPositionInRadius(newPatrolPoint, transform.position, m_minimumPatrolDistance) && (i < 30))
        {
            newPatrolPoint = GeneratePatrolPoint(m_patrolMaxDistance) + transform.position;
        }
        m_currentDestination = patrolHit.position;
        return m_currentDestination;
    }

    private bool CheckPositionInRadius(Vector3 origin, Vector3 end, float radius)
    {
        Vector3 distance = end - origin;
        //Debug.Log(distance);
        return CheckPositionInRadius(distance.x, radius) && CheckPositionInRadius(distance.y, radius) && CheckPositionInRadius(distance.z, radius);
    }
    private bool CheckPositionInRadius(float distance, float radius)
    {
        return Mathf.Abs(distance) <= Mathf.Abs(radius);
    }
}
    #endregion
