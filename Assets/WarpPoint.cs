using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    [SerializeField] private Vector3 warpPosition;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(warpPosition + transform.position, 0.2f);
    }

    public Vector3 WarpPos => warpPosition + transform.position;
}
