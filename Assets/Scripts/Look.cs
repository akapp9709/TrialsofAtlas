using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    [SerializeField] private Transform aimPos;
    public float range;
    [Space] 
    public float sensitivityX;
    public float sensitivityY;
    
    private Vector3 _inVec;
    private Transform _cam;
    private float _tilt;
    private void OnDrawGizmos()
    {
        var pos = transform.position;
        
        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(pos, pos - transform.forward * 3);
    }

    // Start is called before the first frame update
    void Start()
    {
        _cam = GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, _inVec.y * Time.deltaTime);
        _tilt += _inVec.x * Time.deltaTime;
        _tilt = Math.Clamp(_tilt, -45, 45);
        
        var targetRot = transform.eulerAngles;
        targetRot.x = _tilt;
        targetRot.z = 0f;
        
        transform.eulerAngles = targetRot;

        var hit = new RaycastHit();
        var layer = LayerMask.NameToLayer("Ground");

        if (Physics.Raycast(_cam.position, _cam.forward,out hit, range, ~layer))
        {
            aimPos.position = hit.point;
        }
        else
        {
            aimPos.position = _cam.position + _cam.forward * range;
        }
    }

    void OnLook(InputValue input)
    {
        var vec = input.Get<Vector2>();
        _inVec = new Vector3(vec.y * sensitivityX, vec.x * sensitivityY, 0);
    }

    public Vector3 AimPos => aimPos.position;
}
