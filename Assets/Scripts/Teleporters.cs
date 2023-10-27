using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class Teleporters : MonoBehaviour
{

    [SerializeField]
    private Transform destination;

    public Vector3 GetDestination()
    {
        return new Vector3(destination.position.x, destination.position.y, 0);
    }
}
