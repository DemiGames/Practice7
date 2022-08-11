using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapSoundScript : MonoBehaviour
{
    public AudioSource tap;
    public GameManager gm;
    void Start()
    {
        tap = GetComponent<AudioSource>();
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }
    public void TapSound()
    {
        if (gm.playMusic)
            tap.Play();
    }
}
