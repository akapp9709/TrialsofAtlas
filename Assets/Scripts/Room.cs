using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform roomExit, roomCentre;
    [SerializeField] private float checkRadius;
    public Vector3 exitPosition => roomExit.position;
    public Vector3 exitRotation => roomExit.eulerAngles;

    public Vector3 exitForward => roomExit.forward;
    public float roomAngle => EntranceToExit();

    public float Angle;
    public bool changeDirection;
    public bool clipping = false;
    
    Vector3 dir, penePos;
    float dist;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(roomCentre.position, checkRadius);
    }

    private void OnDrawGizmos()
    {
        var pos = transform.position;
        Angle = roomAngle;
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(exitPosition, exitPosition + roomExit.forward * 5);
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos, exitPosition);
        Gizmos.DrawLine(pos, pos + transform.forward*3);
        
        Gizmos.color = Color.yellow;
        if (clipping)
        {
            Gizmos.DrawSphere(penePos, 0.2f);
            Gizmos.DrawRay(penePos, dir*dist);
            Handles.Label(penePos + dist*dir, $"{dist} - {this.name}");
        }
        
    }

    [ExecuteInEditMode]
    private float EntranceToExit()
    {
        var d = exitPosition - transform.position;
        var f = transform.forward;

        d.y = 0;
        
        var theta = Vector3.Angle(d, f);
        Angle = theta;
        return theta;
    }

    public void Start()
    {
        Angle = roomAngle;
    }

    public bool CheckForOverlap()
    {
        int layer = LayerMask.NameToLayer("Room");
        bool clear;
        var coll = GetComponent<BoxCollider>();
        var pos = transform.position;
        var rot = transform.rotation;
        
        var colls = Physics.OverlapSphere(roomCentre.position, checkRadius, ~layer);
        foreach (var x in colls)
        {
            if (x.GetComponent<Room>() == null) continue;
            if (Physics.ComputePenetration(coll, roomCentre.position, roomCentre.rotation, x, 
                x.transform.position,
                x.transform.rotation, out dir, out dist) 
                && x.transform != this.transform)
            {
                if (dist > 0.5f)
                {
                    clipping = true;
                    penePos = x.transform.position;
                    return false;
                }
                else
                {
                    clipping = false;
                }
            }
        }

        return true;
    }
}
