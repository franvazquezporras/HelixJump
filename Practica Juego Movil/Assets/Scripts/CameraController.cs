using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    PlayerController player;

    float distance;
    void Start()
    {
        distance = transform.position.y - player.transform.position.y;
    }

 
    void Update()
    {
        Vector3 actualPosition = transform.position;
        actualPosition.y = player.transform.position.y + distance;
        transform.position = actualPosition;
    }
}
