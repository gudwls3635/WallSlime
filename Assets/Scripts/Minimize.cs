using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("mini");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator mini()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (GameManager.instance.time==0)
            {
                if(transform.localScale.y >1f)
                {
                    transform.localScale -= Vector3.one * 0.02f;
                }
                    
            }
        }
            

    }
}
