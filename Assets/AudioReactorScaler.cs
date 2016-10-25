using UnityEngine;
using System.Collections;

public class AudioReactorScaler : MonoBehaviour
{
    public AudioSource audio;

    SpriteRenderer renderer;

    public float startScale;
    public float finalScale;

    float[] Samples = new float[128];
    public float t;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    public float min = -0.01f;
    public float max = 0.01f;
    public float sum;

    void Update()
    {
        audio.GetOutputData(Samples, 0);

        sum = 0f;

        for (int i = 0; i < Samples.Length; i++)
        {
            sum += Samples[i] * Samples[i];
        }

        sum = Mathf.Sqrt(sum);
        sum /= (Samples.Length);

        if (sum < min) min = sum;
        if (sum > max) max = sum;

        t = (sum - min) / (max - min);

        transform.localScale = Vector3.one*Mathf.Lerp(startScale, finalScale, t);
    }
}
