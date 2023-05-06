using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pouler : MonoBehaviour
{
    float m_minPoulerMovement = 0.5f;
    float m_maxPoulerMovement = 1f;
    Vector3 m_randomMovement = Vector3.zero;
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
    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.Warp(transform.position);
        m_collider = GetComponent<Collider>();
        m_animator = GetComponent<Animator>();
    }
    public void Death()
    {
        Destroy(gameObject);
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
                transform.position = transform.position; /*dinobacktransform*/
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {

    }
    void RandomMovement()
    {
        float randomX = Random.Range(m_minPoulerMovement, m_maxPoulerMovement);
        float randomZ = Random.Range(m_minPoulerMovement, m_maxPoulerMovement);
        m_randomMovement = new Vector3(randomX, 0, randomZ);
    }
    void PoulerMove()
    {
        bool destinationReached = false;
        m_agent.SetDestination(transform.position + m_randomMovement);


    }
    void Start()
    {

    }

    void Update()
    {
        PoulerMove();
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

    #endregion
}

#endregion