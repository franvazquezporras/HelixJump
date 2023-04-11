using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameController.singleton.Score(1);
        FindObjectOfType<PlayerController>().pass++;
    }
}
