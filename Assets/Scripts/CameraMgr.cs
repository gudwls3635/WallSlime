using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    public GameObject Slime1, Slime2, Lines, Subline, BG1, BG2, Mouse;
    public List<GameObject> Items = new List<GameObject>();
    public List<GameObject> Sting = new List<GameObject>();
    private SlimeControl Script1, Script2;
    private float interval, progress;
    [HideInInspector]
    public float bgSpeed, dSpeed;
    public GameObject title;
    public static CameraMgr instance;
    // Start is called before the first frame update
    void Start()
    {
        Script1 = Slime1.GetComponent<SlimeControl>();
        Script2 = Slime2.GetComponent<SlimeControl>();
        interval = 1.5f;
        progress = 0;

        //StartCoroutine(Timer());
        StartCoroutine(SpeedUp());
    }

    /* 같은 속도로 움직일 때 : 슬라임의 속도 
     * 한쪽만 내려갈 때: 속도 / 2
     * 둘 다 안내려갈 때 : 0
     * 
     */

    // Update is called once per frame
    void Update()
    {
        MoveLines();
        BGMove();
        NewLine();
    }

    void MoveLines()
    {
        bgSpeed = (Script1.movingSpeed * IsHorizontal(Script1.state) + Script2.movingSpeed * IsHorizontal(Script2.state)) / 2;
        dSpeed = Mathf.Abs((Script1.movingSpeed * IsHorizontal(Script1.state) - Script2.movingSpeed * IsHorizontal(Script2.state)) / 2);
        Lines.transform.Translate(Vector2.up * bgSpeed * Time.deltaTime);
        
    }

    int IsHorizontal(int num)
    {
        // 0: 세로, 1: 왼쪽, 2: 오른쪽
        if(num == 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    void NewLine()
    {
        if (GameManager.instance.time != 0)
        { return; }

        if (progress < interval)
        {
            progress += bgSpeed * Time.deltaTime;
            return;
        }

            // -2, 0, 2
        int num = Random.Range(0, 5);
        GameObject newOb;
        if (num >= 3)
        {
            newOb = Instantiate(Subline, Lines.transform);
            newOb.transform.position = new Vector2(-3.1f, -6);
            newOb = Instantiate(Subline, Lines.transform);
            newOb.transform.position = new Vector2(-3.1f + 2 * 3.1f, -6);
        }
        else
        {
            newOb = Instantiate(Subline, Lines.transform);
            newOb.transform.position = new Vector2(-3.1f + num * 3.1f, -6);
        }

        // 아이템 생성
        float num2 = Random.Range(0.0f, 1.0f);
        if (num2 < 0.8f)
        {
            StartCoroutine(NewItem());
        }

        // 쥐 생성
        num2 = Random.Range(0.0f, 1.0f);
        if (num2 < 0.1f)
        {
            StartCoroutine(NewMouse());
        }else if(num2 > 0.5f)
        {
            StartCoroutine(NewSting());
        }
        progress = 0;
        
    }
    private IEnumerator NewMouse()
    {
        float num2 = Random.Range(0.1f, 1.2f);
        yield return new WaitForSeconds(num2);
        num2 = Random.Range(0.0f, 1.0f);
        GameObject newOb = Instantiate(Mouse, Lines.transform);
        newOb.transform.position = new Vector2(num2*15 - 7.5f, -6);
    }

    private IEnumerator NewItem()
    {
        yield return new WaitForSeconds(interval / 2);
        int num = Random.Range(0, 4);
        GameObject newOb = Instantiate(Items[num], Lines.transform);
        num = Random.Range(0, 4);
        newOb.transform.position = new Vector2(-4.65f + num * 3.1f, -6);
    }

    private IEnumerator NewSting()
    {
        float num2 = Random.Range(0.1f, 1.2f);
        yield return new WaitForSeconds(num2);
        num2 = Random.Range(0.0f, 1.0f);
        int num1 = Random.Range(0, 3);
        GameObject newOb;
        if (num1 == 2)
        {
            newOb = Instantiate(Sting[num1], Lines.transform);
            newOb.transform.position = new Vector2(num2 * 15 - 7.5f, -6);
        }
        else
        {
            newOb = Instantiate(Sting[num1], Lines.transform);
            newOb.transform.position = new Vector2(num2 * 15 - 7.5f, -6);
            if (Random.Range(0.0f, 1.0f) < 0.5)
            {
                newOb.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }
        
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            if(GameManager.instance.time==0)
                NewLine();
        }
    }

    private IEnumerator SpeedUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.2f);
            interval += 0.1f;
        }
    }

    void BGMove()
    {
        if (title.GetComponent<Title>().ch==1)
        {
            BG1.transform.Translate(Vector2.up * bgSpeed * Time.deltaTime);
            BG2.transform.Translate(Vector2.up * bgSpeed * Time.deltaTime);

            if (BG1.transform.position.y > 10)
            {
                BG1.transform.Translate(Vector2.down * 20);
            }
            else if (BG2.transform.position.y > 10)
            {
                BG2.transform.Translate(Vector2.down * 20);
            }
        }
    }
}
