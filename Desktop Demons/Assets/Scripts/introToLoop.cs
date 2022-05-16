using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introToLoop : MonoBehaviour
{
    [SerializeField] AudioClip intro;
    AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.PlayOneShot(intro);
       Invoke("PlayLater", intro.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlayLater()
    {
        aud.Play();
    }
}
