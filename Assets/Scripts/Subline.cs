using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subline : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOut())
        {
            Destroy(gameObject);
        }
    }

    bool IsOut()
    {
        if(transform.position.y > 6)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
