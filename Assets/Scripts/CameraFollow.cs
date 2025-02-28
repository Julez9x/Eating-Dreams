using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Determines how fast the camera will move to the target
    public float FollowSpeed = 2f;
    //This changes the Y Offset of the camera
    public float yOffset = 1f;
    //This gives the position of the player
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        //Vector3 because camera is in a 3d space despite being made in a 2d game
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        //Slerp interpolates two Vectors within a sphereical radius
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);

    }
}
