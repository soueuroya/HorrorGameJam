using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicsScript : MonoBehaviour
{

    [SerializeField] AudioSource audioS;
    [SerializeField] AudioClip knock1;
    [SerializeField] AudioClip knock2;
    [SerializeField] AudioClip knock3;
    [SerializeField] AudioClip footsteps;

    public void PlayFootsteps()
    {
        audioS.PlayOneShot(footsteps);
    }

    public void PlayKnock1()
    {
        audioS.PlayOneShot(knock1);
    }

    public void PlayKnock2()
    {
        audioS.PlayOneShot(knock1);
    }
    public void PlayKnock3()
    { 
        audioS.PlayOneShot(knock1);
    }


}
