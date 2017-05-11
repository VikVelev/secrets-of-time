﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_lookat : MonoBehaviour{

    public GameObject Canvas;
    public GameObject Interaction_Canvas = null;
    public bool IsInteractive = false;
    int n;
    public void OnLookOn()
    {
        if (!Canvas.GetComponent<Canvas>().enabled)
        {
            Canvas.GetComponent<Canvas>().enabled = true;
            if (IsInteractive)
            {
                Interaction_Canvas.GetComponent<Canvas>().enabled = true;
                Debug.Log(n++);
            }
        }
    }

    public void OnLookOff()
    {
        Canvas.GetComponent<Canvas>().enabled = false;
        if (IsInteractive)
        {
            Interaction_Canvas.GetComponent<Canvas>().enabled = false;
        }
    }
}
