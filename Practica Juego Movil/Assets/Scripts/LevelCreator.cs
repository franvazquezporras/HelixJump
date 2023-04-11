using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class LevelDesign
{
    [Range(1, 11)]
    public int platforms = 11;

    [Range(0, 11)]
    public int deathZones = 1;
}


[CreateAssetMenu(fileName ="New Level")]
public class LevelCreator : ScriptableObject
{
    public Color levelBackgroundColor = Color.white;
    public Color levelPlataformColor = Color.white;
    public Color levelPlayerColor = Color.white;

    public List<LevelDesign> levels = new List<LevelDesign>();
}
