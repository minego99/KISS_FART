using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CameraMovement : MonoBehaviour
{
    private Camera m_camera;

    [SerializeField]
    private Transform m_focusPoint, m_cameraAnchor;
    [SerializeField]
    private float m_MovementSmoothing;

    [SerializeField]
    private Vector3 m_offset;
    // Start is called before the first frame update
    void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.LookAt(m_focusPoint);
     
        transform.position = Vector3.Lerp(transform.position, m_focusPoint.position + m_offset, Mathf.Clamp01(m_MovementSmoothing + Time.deltaTime));
        //transform.rotation =  m_cameraAnchor.rotation;

    }

}
