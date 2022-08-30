using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject panel, slime1, slime2, title, mainCamera;
    public int time; // 0: 게임 진행 1: 게임 멈춤
    public float timer;
    public Text timeText;
    public Image who;
    public Sprite[] slimeImg = new Sprite[2];
    public AudioClip vic,bgm;
    public int ch;
    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        time = 1;
        timer = 0;
        
        //DontDestroyOnLoad(this.gameObject);
    }
   
    void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        IsOut();
        if (time == 0)
            timer += Time.deltaTime;//(mainCamera.GetComponent<CameraMgr>().bgSpeed + mainCamera.GetComponent<CameraMgr>().dSpeed) * Time.deltaTime;
         timeText.text = timer.ToString("f2");
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Victory()
    {
        Time.timeScale = 0;
        if(ch==0)
        {
            GameBGM.instance.audio.Stop();
            GameBGM.instance.audio.clip = vic;
            GameBGM.instance.audio.Play();
            ch = 1;
        }
        timerText.text = timer.ToString("f2");
        panel.SetActive(true);  
    }

    
    void IsOut()
    {
        if(slime1.transform.position.y > 5)
        {
            //winnerText.text = "slime1";

            who.sprite = slimeImg[1];

            Victory();
        }else if(slime2.transform.position.y > 5)
        {
            who.sprite = GameManager.instance.slimeImg[0];
            //winnerText.text = "slime2";
            Victory();
        }
    }
    

    public void GotoTitle()
    {
        GameEffect.instance.audio.PlayOneShot(GameEffect.instance.btnSound);
        panel.SetActive(false);
        //Time.timeScale = 1;
        time = 1;
        SceneManager.LoadScene("MainScene");
        ch = 0;
        GameBGM.instance.audio.Stop();
        GameBGM.instance.audio.clip = bgm;
        GameBGM.instance.audio.Play();
        title.GetComponent<Title>().Reload();
    }
}
