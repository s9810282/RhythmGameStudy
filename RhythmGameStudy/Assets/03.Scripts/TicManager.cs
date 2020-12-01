using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicManager
{
    private static TicManager instance;
    public static TicManager Instance
    {
        get
        {
            if (instance == null)
                instance = new TicManager();

            return instance;
        }
    }

    public float Time { get => time; set => time = value; }

    private float time = 0;
    
}
