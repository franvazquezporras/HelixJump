using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarController : MonoBehaviour
{
    Vector2 TouchPosition;
    Vector3 pilarRotation;

    public Transform TopPlataform;
    public Transform BotPlataform;


    public GameObject levelPrefab;

    public List<LevelCreator> levels = new List<LevelCreator>();
    public float plataformDistance;
    List<GameObject> levelsCreated = new List<GameObject>();


    private void Awake()
    {
        pilarRotation = transform.localEulerAngles;
        plataformDistance = TopPlataform.localPosition.y - (BotPlataform.localPosition.y + 0.1f);
        LoadLevel(0);
    }


    
    void Update()
    {
        //especifica la pantalla como si fuese el dedo
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTouch = Input.mousePosition;
            if(TouchPosition == Vector2.zero)
            {
                TouchPosition = currentTouch;
            }

            float distance = TouchPosition.x - currentTouch.x;
            TouchPosition = currentTouch;

            transform.Rotate(Vector3.up * distance);

        }

        if (Input.GetMouseButtonUp(0))
        {
            TouchPosition = Vector2.zero;
        }
    }

    public void LoadLevel(int level)
    {
        LevelCreator nextLevel = levels[Mathf.Clamp(level, 0, levels.Count - 1)];

        if (nextLevel ==null)
        {
            Debug.Log("No hay niveles agregados");
            return;
        }


        Camera.main.backgroundColor = levels[level].levelBackgroundColor;
        FindObjectOfType<PlayerController>().GetComponent<Renderer>().material.color = levels[level].levelPlayerColor;
        transform.localEulerAngles = pilarRotation;

        //destruir niveles residuales
        foreach(GameObject i in levelsCreated)
        {
            Destroy(i);
        }

        //calculo de distancia entre plataformas y creacion del nivel
        float levelPlataformDistance = plataformDistance / nextLevel.levels.Count;
        float spawnY = TopPlataform.localPosition.y;

        for(int i = 0; i < nextLevel.levels.Count; i++)
        {
            spawnY -= levelPlataformDistance;

            GameObject levelInstance = Instantiate(levelPrefab, transform);
            levelInstance.transform.localPosition = new Vector3(0, spawnY, 0);

            levelsCreated.Add(levelInstance);

            int plataformDisable = 12 - nextLevel.levels[i].platforms;
            List<GameObject> disablePlataform = new List<GameObject>();

            while (disablePlataform.Count < plataformDisable)
            {
                GameObject randomPlataform = levelInstance.transform.GetChild(Random.Range(0, levelInstance.transform.childCount)).gameObject;
                if (!disablePlataform.Contains(randomPlataform))
                {
                    randomPlataform.SetActive(false);
                    disablePlataform.Add(randomPlataform);
                }
            }

            List<GameObject> missPlataform = new List<GameObject>();
            foreach (Transform tr in levelInstance.transform)
            {
                tr.GetComponent<Renderer>().material.color = levels[level].levelPlataformColor;
                if (tr.gameObject.activeInHierarchy)
                {
                    missPlataform.Add(tr.gameObject);
                }
            }


            List<GameObject> deathPlataforms = new List<GameObject>();
            while(deathPlataforms.Count< nextLevel.levels[i].deathZones)
            {
                GameObject randomDeathPlataform = missPlataform[(Random.Range(0, missPlataform.Count))];
                if (!deathPlataforms.Contains(randomDeathPlataform))
                {
                    randomDeathPlataform.gameObject.AddComponent<DeathZone>();
                    deathPlataforms.Add(randomDeathPlataform);
                }
            }
                
        }
    }
}
