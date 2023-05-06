using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class DinoMovement : MonoBehaviour
{
    private CharacterController m_controller ;
    [SerializeField]  float m_speed;

    private Vector3 m_oldDir, m_currentDir;

    [SerializeField]
    private float m_controlSmoothing, m_smoothingFactor, m_deadzone;
    private float m_currentSmoothing;

    private float m_gravity = 9.81f;

    private void Awake()
    {
        PlayerInput inputs = new PlayerInput();
    }

    private void Start()
    {
        m_controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    public void ChangeDirection(InputAction.CallbackContext context)
    {
        Vector2 newDir = context.ReadValue<Vector2>();
        m_oldDir = m_currentDir;
        m_currentSmoothing = m_controlSmoothing;
        if (CheckDeadzone(newDir, m_deadzone))
        {
            m_currentDir = newDir;
        }
        else
        {
            m_currentDir = Vector2.zero;
        }
    }

    private bool CheckDeadzone(Vector2 direction, float threshold)
    {
        //Debug.Log(direction.sqrMagnitude);
        return Mathf.Abs(direction.sqrMagnitude) > threshold;
    }

    private void Move()
    {
        m_currentSmoothing = Mathf.Clamp01(m_currentSmoothing + Time.deltaTime / m_smoothingFactor);
        Vector2 interpolatedDir = Vector2.Lerp(m_oldDir, m_currentDir, m_currentSmoothing);
        if (m_currentDir.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.Euler(0, -Vector2.SignedAngle(Vector2.up, interpolatedDir), 0);// needs refining
        }
        m_controller.Move(new Vector3(transform.forward.x, transform.forward.y - m_gravity, transform.forward.z) * m_speed * interpolatedDir.sqrMagnitude * Time.deltaTime);
        //m_animator.SetFloat("MovementIntensity", interpolatedDir.sqrMagnitude); // Add the value to the blend tree: https://answers.unity.com/questions/1604947/blend-tree-parameter.html
    }
}
