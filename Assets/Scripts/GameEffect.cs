using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEffect : MonoBehaviour
{
    public static GameEffect instance;
    public AudioSource audio;
    public AudioClip btnSound;
    void Awake()
    {
        if (GameEffect.instance == null)
        {
            GameEffect.instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
