using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Patrol : MonoBehaviour
{
    public List<PatrolPoint> m_PatrolPoints;
    // Start is called before the first frame update

    private void Awake()
    {
        m_PatrolPoints = GetComponentsInChildren<PatrolPoint>().ToList();

        for (int i = 0; i < m_PatrolPoints.Count - 1; i++)
        {
            if (m_PatrolPoints[i] != null && m_PatrolPoints[i + 1] != null)
            {
                m_PatrolPoints[i].m_NextPatrolPoint = m_PatrolPoints[i + 1];
            }

        }

        if (m_PatrolPoints.Count > 1)
        {
            m_PatrolPoints.Last().m_NextPatrolPoint = m_PatrolPoints[0];
        }
    }
}
