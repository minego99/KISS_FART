using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoMovement : MonoBehaviour
{
  [SerializeField] private CharacterController m_controller ;
  [SerializeField]  float m_speed;

    private void Awake()
    {
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0, vertical) * m_speed * Time.deltaTime;
        m_controller.Move(move);
    }
}
