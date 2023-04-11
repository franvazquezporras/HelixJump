using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasController : MonoBehaviour
{
    [SerializeField] Text actualScore;
    [SerializeField] Text highScore;

    public Slider progress;
    [SerializeField] Text actualLevel;
    [SerializeField] Text nextLevel;

    [SerializeField] Transform topTransform;
    [SerializeField] Transform botTransform;
    [SerializeField] Transform player;

    void Update()
    {
        actualScore.text ="Score: " + GameController.singleton.actualScore;
        highScore.text = "High Score: " + GameController.singleton.highScore;
        SliderLevelProgress();
    }



    void SliderLevelProgress()
    {
        actualLevel.text = "" + (GameController.singleton.level+1);
        nextLevel.text = "" + (GameController.singleton.level + 2);

        float distanceLevel = topTransform.position.y - botTransform.position.y;
        float distanceToEnd = distanceLevel - (player.position.y - botTransform.position.y);

        float point = (distanceToEnd / distanceLevel);
        progress.value = Mathf.Lerp(progress.value,point,4);
    }
}
