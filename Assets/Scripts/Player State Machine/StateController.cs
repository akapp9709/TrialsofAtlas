using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateController : MonoBehaviour
{
    private BaseState m_CurrentState;
    public StandardState Standard = new StandardState();
    public StealthState Stealth = new StealthState();
    public CombatState Combat = new CombatState();
    public FallingState Falling = new FallingState();

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform playerMesh;
    public bool moving, crouched, sprint, combat;

    private Move _move;
    private PlayerInput _pi;

    public Vector2 inputVec;
    
    //Delegates
    public delegate void Jump();
    public static Jump jump;

    public delegate void Crouch();
    public static Crouch crouch;

    // Start is called before the first frame update
    void Start()
    {
        _move = GetComponent<Move>();

        m_CurrentState = Standard;
        m_CurrentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        moving = inputVec.magnitude >= 0.5f;
        m_CurrentState.UpdateState(this);
        
        
    }

    public void OnMove(InputValue input)
    {
        m_CurrentState.Move(this, input.Get<Vector2>());
        inputVec = input.Get<Vector2>();
    }

    public void OnCrouch(InputValue input)
    {
        // crouched = !crouched;
        // m_CurrentState.EnterStealth(this);
        crouch?.Invoke();
    }

    public void OnJump(InputValue input)
    {
        jump?.Invoke();
    }

    public void SwitchState(BaseState state)
    {
        m_CurrentState = state;
        m_CurrentState.EnterState(this);
    }
    
    public Move PlayerMove => _move;
    public Animator PlayerAnimator => playerAnimator;
    public PlayerInput Input => _pi;
    public Transform PlayerMesh => playerMesh;
    public Rigidbody PlayerBody => _move.GetComponent<Rigidbody>();
}
