using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 offset;
    Vector3 skyViewPos;
    Vector3 origPos;

    // Start is called before the first frame update
    void Start()
    {
        skyViewPos = new Vector3 (0,15,0);
        origPos = transform.position;
        offset = origPos - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = player.transform.position + offset;
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = skyViewPos;
            transform.eulerAngles = new Vector3(90, 0, 0);
        }
        else
        {
            transform.position = player.transform.position + offset;
            transform.eulerAngles = new Vector3(45, 0, 0);
        }


    }

}

