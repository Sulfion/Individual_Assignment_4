using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public float StepDistance;
    public List<AudioClip> sounds;

    private Vector3 lastPosition;
    private AudioSource audioSource;
    private static System.Random rnd = new System.Random();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    void LateUpdate()
    {
        if (Vector3.Distance(transform.position, lastPosition) > StepDistance)
        {
            audioSource.PlayOneShot(sounds[rnd.Next(sounds.Count)]);
            lastPosition = transform.position;
        }
    }
}

