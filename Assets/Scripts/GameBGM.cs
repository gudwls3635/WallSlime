using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGM : MonoBehaviour
{
    public static GameBGM instance;
    public AudioSource audio;
    void Awake()
    {
        if (GameBGM.instance == null)
        {
            GameBGM.instance = this;
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
