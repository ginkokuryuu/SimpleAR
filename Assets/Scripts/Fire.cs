using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        CustomEventHandler.INSTANCE.OnObjectDetected += ObjectDetected;
        CustomEventHandler.INSTANCE.OnObjectNotDetected += ObjectNotDetected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StopParticle()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            ParticleSystem particle = transform.GetChild(i).GetComponent<ParticleSystem>();
            if (particle)
            {
                particle.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }

    void StartParticle()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            ParticleSystem particle = transform.GetChild(i).GetComponent<ParticleSystem>();
            if (particle)
            {
                particle.gameObject.SetActive(true);
                particle.Play();
            }
        }
    }

    public void StartAudio()
    {
        audioSource.Play(0);
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    public void ObjectDetected()
    {
        isDetected = true;
        StartAudio();
        StartParticle();
    }

    public void ObjectNotDetected()
    {
        isDetected = false;
        StopAudio();
        StopParticle();
    }

    private void OnDestroy()
    {
        CustomEventHandler.INSTANCE.OnObjectDetected -= ObjectDetected;
        CustomEventHandler.INSTANCE.OnObjectNotDetected -= ObjectNotDetected;
    }
}
