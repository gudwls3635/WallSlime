using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject mainCamera, startLine, gameMgr;
    public GameObject slime1, slime2, logo,timer;
    public float movingSpeed;
    public int ch;
    public int start;
    // Start is called before the first frame update

    void Start()
    {
        movingSpeed = 1f;
        //Reload();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.anyKey && ch==0)
        {
            ch = 1;
            GameManager.instance.time = 1;
        }
        if (ch == 1)
        {
            Vector2 plusVector = (Vector2.up * mainCamera.GetComponent<CameraMgr>().bgSpeed) * Time.deltaTime;
            logo.gameObject.transform.Translate(plusVector);//Vector2.up * movingSpeed * Time.deltaTime + plusVector);

            if (start==0)
            {
                slime1.transform.Translate(Vector2.up * movingSpeed * Time.deltaTime + plusVector);
                slime2.transform.Translate(Vector2.up * movingSpeed * Time.deltaTime + plusVector);
            }
            if(logo.transform.position.y > 10)
            {
                logo.SetActive(false);
            }
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Slime"))
        {
            timer.gameObject.SetActive(true);
            start = 1;
            GameManager.instance.time = 0;
        }
    }

    public void Reload()
    {
        Time.timeScale = 1;
        logo.SetActive(true);
        slime1.transform.position = new Vector2(-4.65f, 1);
        slime2.transform.position = new Vector2(4.65f, 1);

        slime1.SendMessage("Start");
        slime2.SendMessage("Start");
        gameMgr.SendMessage("Start");
        mainCamera.SendMessage("Start");
    }
}
