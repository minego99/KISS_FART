using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField]
    private float m_distanceThreshold;
    public float m_WaitDuration = 0.5f;
    public PatrolPoint m_NextPatrolPoint;
    private void Start()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, m_distanceThreshold))
        {
            float distanceFromGround = hit.distance;
            Vector3 currentPosition = transform.position;
            transform.position = new Vector3(currentPosition.x, currentPosition.y + (m_distanceThreshold - distanceFromGround), currentPosition.z);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
