using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuildDungeon : MonoBehaviour
{
    [Serializable]
    public struct RoomSpace
    {
        public GameObject RoomPrefab;
        public float Probability;
    }

    [Serializable]
    public struct Corridor
    {
        public GameObject CorridorPrefab;
        public float height;
    }
    
    private string[] _mRoomSizes = {"Large", "Medium", "Small"};
    [SerializeField] private Transform startRoom;
    [SerializeField] private GameObject endRoomPrefab;
    private Vector3 m_ExitPos, m_ExitRot;
    private GameObject m_PrevObj, m_CurrentObj;

    public int dungeonLength, numRoomTypes;
    public float offset;

    private List<Vector3> m_positions = new List<Vector3>();
    private List<Vector3> m_directions = new List<Vector3>();

    [SerializeField] private List<RoomSpace> Rooms;
    [SerializeField] private List<Corridor> Corridors;
    private RoomSpace _currentRoom, _currentCorridor;

    // Start is called before the first frame update
    void Start()
    {
        var room = startRoom.GetComponent<Room>();
        m_ExitPos = room.exitPosition;
        m_ExitRot = room.exitRotation;

        numRoomTypes = Rooms.Count;
        for (int i = 0; i < dungeonLength; i++)
        {
            bool success = false;
            while (!success)
            {
                var x = Random.Range(0, Rooms.Count);
                var y = Random.Range(0, 101);
                if (y < Rooms[x].Probability)
                {
                    success = true;
                    _currentRoom = Rooms[x];
                    m_CurrentObj = Instantiate(_currentRoom.RoomPrefab, m_ExitPos,Quaternion.Euler(m_ExitRot), this.transform) as GameObject;
                    room = m_CurrentObj.GetComponent<Room>();
                    m_ExitPos = room.exitPosition;
                    m_ExitRot = room.exitRotation;
                    if (!room.CheckForOverlap())
                    {
                        Debug.Log("Theres Fuckery about");
                        //Remove currentObj
                        // Insert correct staircase
                        // set positions and rotations
                        // place object Again
                    }
                }
            }
        }

        Instantiate(endRoomPrefab, m_ExitPos, Quaternion.Euler(m_ExitRot), this.transform);
    }

    private float GetTotal()
    {
        var num = Rooms.Count;
        float tot = 0;
        foreach (var x in Rooms)
        {
            tot += x.Probability;
        }

        return tot / num;
    }

    private Vector3 RotateVectorByAngle(Vector3 vec, float angle)
    {
        //angle += offset;
        var a = new Vector3((float) Math.Cos(angle), 0f, (float) Math.Sin(angle)) * -1;
        var b = new Vector3((float) Math.Sin(angle) * -1, 0f, (float) Math.Cos(angle));
        var y = new Vector3(0, vec.y, 0);

        return (vec.x * a + vec.z * b);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
