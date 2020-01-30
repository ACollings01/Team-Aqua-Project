using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButton(0))
            RotateToMouse();
    }

    void RotateToMouse()
    {
        Vector3 mouseposition = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z);

        RaycastHit hit;

        //if (Physics.Raycast(mouseposition, out hit))
        //{
         //   print("There is something in front of the object!");
       // }

        /*Vector3 v3T = Input.mousePosition;
          v3T.z = Mathf.Abs(Camera.main.transform.position.y - transform.position.y);
          v3T = Camera.main.ScreenToWorldPoint(v3T);
          v3T -= transform.position;
          v3T = v3T * 10000.0f + transform.position;
          transform.LookAt(v3T);*/
    }
}
