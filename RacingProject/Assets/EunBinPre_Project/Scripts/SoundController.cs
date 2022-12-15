using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController instance;

    public static SoundController GetInstance()
    {
        return instance;
    }

    public AudioClip audioCount;
    public AudioClip audioBoost;
    public AudioClip audioGoal;
    AudioSource audioSource;

    public enum Actions
    {
        Count,
        Run,
        Goal
    }
    void Awake()
    {
        instance = this;
        this.audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public void GetSound(Actions action)
    {

        switch (action)
        {
            case Actions.Count:
                audioSource.clip = audioCount;
                break;
            case Actions.Goal:
                audioSource.clip = audioGoal;
                break;
        }
        audioSource.Play();


    }

}