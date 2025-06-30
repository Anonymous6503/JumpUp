using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGColor : MonoBehaviour
{
    [SerializeField] private Color[] bgColors;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private int i;
    private void Awake()
    {
        i = 0;
        InvokeRepeating("ColorChange",1.5f,1.5f);
    }

    void ColorChange()
    {
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, bgColors[i], 0.1f);
        i++;
        if ((i) >= bgColors.Length)
        {
            i = 0;
        }
    }
}
