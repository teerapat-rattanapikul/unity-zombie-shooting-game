using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mouseOver : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource effect;
    public AudioClip soundEffect;
    void Start()
    {
        effect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void overSound(){
        effect.PlayOneShot(soundEffect);
    }

}
