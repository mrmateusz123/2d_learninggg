using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtest : MonoBehaviour
{
    private float dirX;
    public AudioClip Walkingsound;
    public AudioClip jumpsound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        if (dirX != 0 && !audioSource.isPlaying) 
        { 
            audioSource.PlayOneShot(Walkingsound); 
        }
        else if (dirX == 0){ 
            audioSource.Stop(); 
        }
        if (Input.GetButtonDown("Jump")) { audioSource.PlayOneShot(jumpsound); }
    }
}
