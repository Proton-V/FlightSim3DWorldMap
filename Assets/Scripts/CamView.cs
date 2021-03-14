using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CamView
{
    public Transform Point;
    public CamViewName ViewName;
    public bool IsControll;
    public enum CamViewName
    {
        First,
        Third
    }
}
