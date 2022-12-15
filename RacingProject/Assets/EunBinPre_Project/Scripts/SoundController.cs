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


    public AudioClip audioGoal;
    public AudioClip audioButtonCilck;
    AudioSource audioSource;

    public enum Actions
    {
        Goal,
        ButtonCilck
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

            case Actions.Goal:
                audioSource.clip = audioGoal;
                break;
            case Actions.ButtonCilck:
                audioSource.clip = audioButtonCilck;
                break;
        }
        audioSource.Play();


    }

}