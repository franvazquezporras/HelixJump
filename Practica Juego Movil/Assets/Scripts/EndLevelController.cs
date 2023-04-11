using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameController.singleton.LevelUp();
    }
}
