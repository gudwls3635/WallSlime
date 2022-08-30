using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeControl : MonoBehaviour
{
 // direction: 0:아래 1: 왼쪽 2:오른쪽  사다리 만나면 갈 방향
 // state: 0:아래 1: 왼쪽 2:오른쪽  지금 가고있는 방향
    public float movingSpeed, stdSpeed;
    public GameObject mainCamera, standardLine, enemy;
    private int check;
    public GameObject[] pipes = new GameObject[4];
    public int pipeN, state, direction;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // 초기 아래방향
        movingSpeed = 2.0f;
        stdSpeed = 2.0f;
        check = 0;
        state = 0; 
        direction = 0;
        if (transform.name.Equals("Slime1"))
        {
            pipeN = 3;
        }
        else if (transform.name.Equals("Slime2"))
        {
            pipeN = 0;
        }
        StartCoroutine(Accelerate());
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.time == 0)
        {
            KeyDown();
            Move();
        }
    }

    public void KeyDown()
    {
        /* 1P 방향키
        if (Input.GetKeyDown(KeyCode.A)) //왼쪽
        {
            direction = 1;
        }
        if (Input.GetKeyDown(KeyCode.D)) // 오른쪽
        {
            direction = 2;
        }
        if (Input.GetKeyDown(KeyCode.S)) // 아래
        {
            direction = 0;
        }
        */
        if(transform.name.Equals("Slime1"))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) //왼쪽
            {
                    direction = 1;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) // 오른쪽
            {
                    direction = 2;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow)) // 아래
            {
                    direction = 0;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow)) // 아래
            {
                    direction = 0;
            }
        }
        else if(transform.name.Equals("Slime2"))
        {
            if (Input.GetKeyDown(KeyCode.A)) //왼쪽
            {
                    direction = 1;
            }
            if (Input.GetKeyDown(KeyCode.D)) // 오른쪽
            {
                    direction = 2;
            }
            if (Input.GetKeyUp(KeyCode.A)) // 아래
            {
                    direction = 0;
            }
            if (Input.GetKeyUp(KeyCode.D)) // 아래
            {
                    direction = 0;
            }
        }
        
    }

    public void Move()
    {
        if (transform.position.y <= standardLine.transform.position.y)
            check = 1;
        Vector2 plusVector = Vector2.up * mainCamera.GetComponent<CameraMgr>().bgSpeed * Time.deltaTime;

        if (check == 0)
            plusVector = Vector3.zero;

        if (state == 0) // 아래
        {
            transform.Translate((Vector2.down * movingSpeed * Time.deltaTime) + plusVector);
        }
        else if (state == 1) // 왼쪽
        {
            transform.Translate((Vector2.left * movingSpeed * Time.deltaTime * 1.6f) + plusVector);

            if (transform.position.x <= pipes[pipeN].transform.position.x)
            {
                transform.position = new Vector3(pipes[pipeN].transform.position.x, transform.position.y);
                state = 0;
                anim.SetInteger("state", 0);
                anim.SetInteger("check", 1);
            }
        }
        else if (state == 2) // 오른쪽
        {
            transform.Translate((Vector2.right * movingSpeed * Time.deltaTime * 1.6f) + plusVector);

            if (transform.position.x > pipes[pipeN].transform.position.x)
            {
                transform.position = new Vector3(pipes[pipeN].transform.position.x, transform.position.y);
                state = 0;
                anim.SetInteger("state", 0);
                anim.SetInteger("check", 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Slime"))
        {
            if (gameObject.transform.localScale.y > collision.transform.localScale.y)
            {
                if (transform.name.Equals("Slime1"))
                    GameManager.instance.who.sprite = GameManager.instance.slimeImg[0];
                else
                    GameManager.instance.who.sprite = GameManager.instance.slimeImg[1];
               
                GameManager.instance.Victory();
            }
            else
            {
                return;
            }
        }

        if (collision.gameObject.CompareTag("Ladder"))
        {
            if(direction == 0)
            {
                return;
            }else if(direction == 1 && state == 0 && collision.transform.position.x < transform.position.x)
            {
                state = 1;
                anim.SetInteger("state", 1);
                pipeN--;
                return;
            }else if(direction == 2 && state == 0 && collision.transform.position.x > transform.position.x)
            {
                state = 2;
                anim.SetInteger("state", 2);
                pipeN++;
                return;
            }
        }

        
    }

    IEnumerator Accelerate()
    {
        while (true)
        {
            
            {
                yield return new WaitForSeconds(2.2f);
                if (GameManager.instance.time == 0)
                {
                    stdSpeed += 0.2f;
                    movingSpeed += 0.2f;
                }
            }
        }
    }
}
