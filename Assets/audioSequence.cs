using UnityEngine;
using System.Collections;

public class audioSequence : MonoBehaviour
{

    public AudioClip[] clips;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start ()
	{
	    StartCoroutine(startSequence());
	}
    
    IEnumerator startSequence()
    {
        for (int j = 0; j < clips.Length; j++)
        {
            audioSource.PlayOneShot(clips[j]);
            yield return  new WaitForSeconds(clips[j].length + 0.2f);
        }
    }
}
