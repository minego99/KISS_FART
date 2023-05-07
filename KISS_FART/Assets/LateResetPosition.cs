using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateResetPosition : MonoBehaviour
{
    public Transform m_whatToMove;
    public Vector3 InitialPos;
    public Quaternion InitRot;
    public float m_timeToWait;
    // Start is called before the first frame update
    void Start()
    {
        InitialPos = m_whatToMove.position;
        InitRot = m_whatToMove.rotation;
        Invoke("RestPos", m_timeToWait);
    }

    public void RestPos()
    {
        m_whatToMove.position = InitialPos;
        m_whatToMove.rotation = InitRot;
    }

    public void Reset()
    {
        m_whatToMove = transform;
    }
}
