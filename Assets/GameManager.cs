using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public CanvasMenuManager CanvasMenu;

    public GameObject HelicopterPrefab;
    [Range(-30f,30f)]
    public float HeightOffset;
    [Range(0f,5f)]
    public float HangTime;

    public List<Rout> Routs;
    [HideInInspector]
    public Rout ChooseRout;

    private void Awake()
    {
        Instance = this;
    }
}
