using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    float bounce = 3f;

    bool ignoreCollision;
    Vector3 iniPosition;


    public int pass;
    float betterSpeed = 8;
    private bool activeBetterSpeed;
    private int countPlataform = 3;



    private void Start()
    {
        iniPosition = transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        

        if(ignoreCollision)
            return;

        if (activeBetterSpeed && !collision.transform.GetComponent<EndLevelController>())
            
            Destroy(collision.transform.parent.gameObject,0.2f);//destruye el padre de la plataforma
        else
        {
            DeathZone deathZone = collision.transform.GetComponent<DeathZone>();
            if (deathZone)            
                GameController.singleton.Restart();            
        }

        

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * bounce, ForceMode.Impulse);

        ignoreCollision = true;
        Invoke("changeCollision", 0.2f);

        pass = 0;
        activeBetterSpeed = false;
    }

    private void Update()
    {
        if (pass >= countPlataform && !activeBetterSpeed)
        {
            activeBetterSpeed = true;
            rb.AddForce(Vector3.down * betterSpeed, ForceMode.Impulse);
        }
            
    }

    void changeCollision()
    {
        ignoreCollision = false;
    }

    public void resetPlayer()
    {
        transform.position = iniPosition;
    }
}
