using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class DinoMovement : MonoBehaviour
{
    public static DinoMovement s_instance;
    Animator m_animator;
    private CharacterController m_controller;
    [SerializeField] float m_speed;
    private Vector3 m_oldDir, m_currentDir;
    public GameObject m_mouthObject;
    [SerializeField]
    private float m_controlSmoothing, m_smoothingFactor, m_deadzone;
    private float m_currentSmoothing;
    private float m_gravity = 9.81f;
    public int m_poulerToLayEgg = 5;
    public GameObject m_eggPrefab;
    public static Transform s_backPosition;
    //   public static Transform s_backPosition;
    public Transform m_layEggPosition;
    public bool m_mouthActivated;


    public Transform m_dinoBack;

    private void Awake()
    {
        s_backPosition = GetComponent<Transform>();
        s_backPosition.position = transform.position += new Vector3(0, 1.5f, 0);
        
            if (s_instance == null)
            {
                s_instance = this;
            }


            m_animator = GetComponent<Animator>();
            PlayerInput inputs = new PlayerInput();
        }
        void LayEgg()
        {
            if (GameManager.s_poulerScore >= m_poulerToLayEgg)
            {
                GameManager.s_poulerScore = 0;
                m_animator.SetTrigger("LayEgg");
            }
        }

        void InstantiateEgg()
        {
            Instantiate(m_eggPrefab, m_layEggPosition.transform.position, Quaternion.identity);
        }
        void ManageMouth()
        {
            m_mouthObject.SetActive(m_mouthActivated);
        }
        private void Start()
        {
            m_controller = GetComponent<CharacterController>();
        }

        void Action()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (GameManager.s_doesattack == true)
                {
                    m_animator.SetTrigger("Eat");
                }

                else
                {
                    m_animator.SetTrigger("Take");
                }


            }
        }
        private void Update()
        {
            LayEgg();
            Debug.Log(GameManager.s_poulerScore);
            ManageMouth();
            Action();
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");


            Vector3 move = new Vector3(horizontal, 0, vertical) * m_speed * Time.deltaTime;
            if (vertical != 0 || horizontal != 0)
            {
                transform.rotation = Quaternion.Euler(0, -Vector2.SignedAngle(Vector2.up, new Vector2(horizontal, vertical)), 0);

                m_controller.Move(move - Vector3.up * m_gravity);


            }
            //Debug.Log("move is" + move);
            //Debug.Log("move normalized is " + move.normalized);
            //Debug.Log("move magnitude is"+ move.magnitude);
            //Debug.Log("move normalized sqr magnitude is" + move.normalized.sqrMagnitude);
            // m_animator.SetFloat("Speed", move.magnitude /2 );
        }

        //public void ChangeDirection(InputAction.CallbackContext context)
        //{
        //    Vector2 newDir = context.ReadValue<Vector2>();
        //    m_oldDir = m_currentDir;
        //    m_currentSmoothing = m_controlSmoothing;
        //    if (CheckDeadzone(newDir, m_deadzone))
        //    {
        //        m_currentDir = newDir;
        //    }
        //    else
        //    {
        //        m_currentDir = Vector2.zero;
        //    }
        //}

        //private bool CheckDeadzone(Vector2 direction, float threshold)
        //{
        //    //Debug.Log(direction.sqrMagnitude);
        //    return Mathf.Abs(direction.sqrMagnitude) > threshold;
        //}

        //private void Move()
        //{
        //    m_currentSmoothing = Mathf.Clamp01(m_currentSmoothing + Time.deltaTime / m_smoothingFactor);
        //    Vector2 interpolatedDir = Vector2.Lerp(m_oldDir, m_currentDir, m_currentSmoothing);
        //    if (m_currentDir.sqrMagnitude > 0)
        //    {
        //        transform.rotation = Quaternion.Euler(0, -Vector2.SignedAngle(Vector2.up, interpolatedDir), 0);// needs refining
        //    }
        //    m_controller.Move(new Vector3(transform.forward.x, transform.forward.y - m_gravity, transform.forward.z) * m_speed * interpolatedDir.sqrMagnitude * Time.deltaTime);
        //    //m_animator.SetFloat("MovementIntensity", interpolatedDir.sqrMagnitude); // Add the value to the blend tree: https://answers.unity.com/questions/1604947/blend-tree-parameter.html
        //}
    }

