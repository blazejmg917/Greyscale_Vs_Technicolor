using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    [Tooltip("the player")]
    public GameObject player;
    [Tooltip("the camera's z offset")]
    public float zOffset;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, zOffset);
    }
}
