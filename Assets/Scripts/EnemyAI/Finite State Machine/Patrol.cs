using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;

    public List<Transform> Points => waypoints;
}
