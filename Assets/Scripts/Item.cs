using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Item : MonoBehaviour
{
    //public List<GameObject> Items = new List<GameObject>();
    public AudioClip good, bad;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 0: sizeup, 1: attack, 2:speedup, 3: obstacle
        if (collision.CompareTag("SizeUp"))
        {
            transform.GetComponent<AudioSource>().PlayOneShot(good);
            StartCoroutine("Sizeup", gameObject);
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("Attack"))
        {
            transform.GetComponent<AudioSource>().PlayOneShot(good);
            StartCoroutine("Attack", gameObject);
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("SpeedUp"))
        {
            transform.GetComponent<AudioSource>().PlayOneShot(good);
            StartCoroutine("SpeedUp", gameObject);
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("Obstacle"))
        {
            transform.GetComponent<AudioSource>().PlayOneShot(bad);
            StartCoroutine("Obstacle", gameObject);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator Attack(GameObject _ob)
    {

        GameObject ob = _ob.GetComponent<SlimeControl>().enemy;
        float init_speed = ob.GetComponent<SlimeControl>().stdSpeed;
        ob.GetComponent<SlimeControl>().movingSpeed -= init_speed * 0.3f;

        yield return new WaitForSeconds(1.2f);

        for(int i = 0; i < 5; i++)
        {
            ob.GetComponent<SlimeControl>().movingSpeed += init_speed * 0.06f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator SpeedUp(GameObject _ob)
    {
        if (GameManager.instance.time == 0)
        {
            float init_speed = _ob.GetComponent<SlimeControl>().stdSpeed;
            _ob.GetComponent<SlimeControl>().movingSpeed += init_speed * 0.2f;

            yield return new WaitForSeconds(1.4f);

            for (int i = 0; i < 5; i++)
            {
                _ob.GetComponent<SlimeControl>().movingSpeed -= init_speed * 0.04f;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    IEnumerator Obstacle(GameObject _ob)
    {


        float init_speed = _ob.GetComponent<SlimeControl>().stdSpeed;
        _ob.GetComponent<SlimeControl>().movingSpeed -= init_speed * 0.3f;

        yield return new WaitForSeconds(1.8f);

        for (int i = 0; i < 5; i++)
        {
            _ob.GetComponent<SlimeControl>().movingSpeed += init_speed * 0.06f;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    IEnumerator Sizeup(GameObject _ob)
    {

        Transform temp = _ob.transform;
        for(int i = 0; i < 25; i++)
        {
            temp.localScale += Vector3.one * 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
    }


    bool IsOut()
    {
        if (transform.position.y > 6)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
