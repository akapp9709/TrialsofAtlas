using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public float speed = 2;
    public float gravity = 10;
    [SerializeField] private Transform camAnchor;
    [Space] [Header("Jump Variables")] 
    public float jumpHeight, jumpSpeed;
    private bool _jumping;
    private float _jumpTime;

    [Space] [Header("Wall Running Variables")]
    public float wallRunSpeed;
    public float wallRunTime, runTime, angleThreshold;
    public float checkRadius;
    private bool _onWall, _canContinueRun;
    private Vector3 _wallPos, _wallDir;

    private Vector3 _mInVec, m_Direction, m_CamDirection, m_AlignedV;
    private Rigidbody m_Rb;

    public bool _isMoving, _grounded;

    private Vector3 _normal, _point, downV;
    private void OnDrawGizmos()
    {
        var pos = transform.position;
        var rgt = camAnchor.right;
        var lft = rgt * -1;
        var offset = new Vector3(0, 2, 0);
        
        Gizmos.color = Color.white;
        Gizmos.DrawLine(pos+offset, pos + m_AlignedV * 5 + offset);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos+offset, pos+offset+camAnchor.right*1.5f);
        if (_onWall)
        {
            Gizmos.DrawSphere(_wallPos, 0.2f);
            Gizmos.DrawRay(_wallPos, _wallDir);
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos+offset, pos+offset+camAnchor.forward*1.5f);
        Gizmos.DrawRay(pos, rgt*checkRadius);
        Gizmos.DrawRay(pos, lft*checkRadius);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_point + pos, _point + _normal*2 + pos);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var camFwd = camAnchor.forward;
        var camRt = camAnchor.right;
        camFwd.y = 0;
        camRt.y = 0;
        
        #region MoveCheck
        if (m_Rb.velocity.magnitude > 0.5f)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
        #endregion

        #region GroundCheck
        var hit = new RaycastHit();
        var groundLayer = LayerMask.GetMask("Ground");
        _grounded = Physics.Raycast(transform.position + transform.up*0.01f, Vector3.down, out hit, 0.1f,
            groundLayer);
        if (_grounded)
        {
            _normal = hit.normal;
            runTime = wallRunTime;
            _canContinueRun = true;
            downV = Vector3.zero;
        }
        #endregion
        
        #region Jump

        if (_jumping)
        {
            if (_grounded)
            {
                downV.y = Mathf.Sqrt(2 * gravity * jumpHeight);
            }
            _jumping = false;
        }
        
        m_AlignedV = _mInVec.z*camFwd + _mInVec.x*camRt;
        m_Direction = m_AlignedV;
        //m_AlignedV = Align(m_Direction, _normal);

        if (!_grounded)
        {
            downV.y -= gravity*Time.deltaTime;
        }

        #endregion
        
        #region WallCheck
        
        int wallMask = LayerMask.GetMask("Runnable");
        var rgt = camAnchor.right;
        var lft = rgt * -1;
        var pos = transform.position;
        var hitLeft = new RaycastHit();
        var hitRight = new RaycastHit();

        var rayLeft = Physics.Raycast(pos, lft, out hitLeft, checkRadius, wallMask);
        var rayRight = Physics.Raycast(pos, rgt,out hitRight, checkRadius, wallMask);

        _onWall = rayLeft | rayRight;
        if (_onWall && downV.y < 0 && runTime >= 0f && _canContinueRun)
        {
            runTime -= Time.deltaTime;
            downV = Vector3.zero;
            m_Rb.useGravity = false;
            if (rayLeft)
            {
                _wallPos = hitLeft.point;
                _wallDir = Vector3.Cross(Vector3.up, hitLeft.normal).normalized * -1;
                m_Direction = _wallDir.normalized * _mInVec.z;
            }
            else if (rayRight)
            {
                _wallPos = hitRight.point;
                _wallDir = Vector3.Cross(Vector3.up, hitRight.normal).normalized;
                m_Direction = _wallDir.normalized * _mInVec.z;
            }
        }
        else if (runTime < 0 | _mInVec.x != 0)
        {
            Debug.Log($"{_mInVec}");
            runTime = 0;
            _onWall = false;
        }
        else
        {
            m_Rb.useGravity = true;
        }

        #endregion

        var totalV = m_Direction * speed + downV;
        m_Rb.velocity = totalV;
    }
    
    
    public Vector3 Align(Vector3 vector, Vector3 normal)
    {
        //typically used to rotate a movement vector by a surface normal
        Vector3 tangent = Vector3.Cross(normal, vector);
        Vector3 newVector = -Vector3.Cross(normal, tangent);
        vector = newVector.normalized * vector.magnitude;
        return vector;
    }

    void OnMove(InputValue input)
    {
        var vec = input.Get<Vector2>();
        vec = vec.normalized;
        _mInVec = new Vector3(vec.x, 0f, vec.y);
        if(_onWall)
            if (_mInVec.x * _mInVec.x > 0)
            {
                _canContinueRun = false;
            }
    }

    void OnJump(InputValue input)
    {
        _jumping = true;
        _jumpTime = jumpHeight / jumpSpeed;
    }
    
    bool IsMovingWithWall()
    {
        var dotVal = Mathf.Cos(Mathf.Deg2Rad * angleThreshold);
        var dot = Vector3.Dot(_mInVec.normalized, _wallDir);

        return dot > dotVal | _mInVec.magnitude > 0.2f;
    }

    public bool isMoving => _isMoving;
    public Vector3 Direction => m_Direction;
}
