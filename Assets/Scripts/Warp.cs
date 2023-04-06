using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Warp : MonoBehaviour
{
    [SerializeField] private float warpTime = 0.5f;
    [SerializeField] private AnimationCurve WarpEase;
    
    private GameObject[] _points;
    private Transform warpTarget;
    private Look _playerLook;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerLook = GetComponent<Look>();
        _points = GameObject.FindGameObjectsWithTag("WarpPoint");
    }

    // Update is called once per frame
    void Update()
    {
        warpTarget = GetClosest().transform;
    }

    GameObject GetClosest()
    {
        float[] dists = new float[_points.Length];
        for (int i = 0; i < _points.Length; i++)
        {
            dists[i] = Vector2.Distance(Camera.main.WorldToScreenPoint(_points[i].transform.position),
                new Vector2(Screen.width / 2f, Screen.height / 2f));
        }

        float minDist = Mathf.Min(dists);
        int index = 0;

        for (int i = 0; i < dists.Length; i++)
        {
            if (minDist == dists[i])
            {
                index = i;
            }
        }

        return _points[index];
    }

    private void OnWarp(InputValue input)
    {
        transform.DOMove(warpTarget.GetComponent<WarpPoint>().WarpPos, warpTime).SetEase(WarpEase);
    }
}
