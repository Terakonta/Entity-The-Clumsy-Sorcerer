﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlastsBar : MonoBehaviour
{

    public static UIBlastsBar instance { get; private set; }

    public Image mask;
    float originalSize;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        if (value >= 0)
            mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
