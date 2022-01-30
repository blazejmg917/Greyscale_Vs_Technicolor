using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunRotation : MonoBehaviour
{

    [Tooltip("The Gun Holder to start rotation from")]
    public GameObject gunHolder;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
        pos.z = gunHolder.transform.position.z;
        Vector3 heading = pos - gunHolder.transform.position;
        //Debug.Log(heading);
        Debug.DrawRay(gunHolder.transform.position, heading * 100);
        Debug.DrawRay(gunHolder.transform.position, gunHolder.transform.up * 100, Color.red);
        Debug.DrawRay(gunHolder.transform.position, gunHolder.transform.forward * 100, Color.green);
        Debug.DrawRay(gunHolder.transform.position, gunHolder.transform.right * 100, Color.blue);
        float ang = Vector3.Angle(new Vector3(0, 1, 0), heading);
        if(heading.x > 0)
        {
            ang *= -1;
        }
        //Debug.Log("new ang: " + ang);
        float oldAng = gunHolder.transform.eulerAngles.z;
        //Debug.Log("old ang: " + oldAng);
        //Debug.Log("composite ang: " + (ang - oldAng));
        gunHolder.transform.Rotate(0, 0, (ang - oldAng));
        //gunHolder.transform.rotation = Quaternion.LookRotation(heading) * gunHolder.transform.rotation; //* Quaternion.FromToRotation(Vector3.right, Vector3.forward);
    }
}
