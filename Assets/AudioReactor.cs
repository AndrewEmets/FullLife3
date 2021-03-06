﻿using UnityEngine;
using System.Collections;

public class AudioReactor : MonoBehaviour {

    public new AudioSource audio;

    new SpriteRenderer renderer;

    public Color startColor;
    public Color finalColor;

    float[] Samples = new float[128];
    public float t;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public float min;
    public float max;
    public float sum;

    void Update()
    {
        audio.GetOutputData(Samples, 0);

        sum = 0f;

        for (int i = 0; i < Samples.Length; i++)
        {
            sum += Samples[i]*Samples[i];
        }

        sum = Mathf.Sqrt(sum);
        sum /= Samples.Length;

        if (sum < min) min = sum;
        if (sum > max) max = sum;

        t = (sum - min) / (max - min);

        var currentColor = Color.Lerp(startColor, finalColor, t);
        renderer.color = currentColor;
    }
	
}
