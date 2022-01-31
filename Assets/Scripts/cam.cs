using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public Transform player;
    Vector3 offset;

    public KeyCode left;
    public KeyCode right;

    private int rot = 1 ;
    public float RotSpeed = 1;
    private float offsetX;
    private float offsetZ;

    private Vector3 offsetTemp;

    private Vector3 toRotation;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
        offsetX = offset.x;
        offsetZ = offset.z;
        offsetTemp = offset;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(right) && rot == 1)
        {
            rot = 2;
        }
        else if (Input.GetKeyDown(right) && rot == 2)
        {
            rot = 3;
        }
        else if (Input.GetKeyDown(right) && rot == 3)
        {
            rot = 4;
        }
        else if (Input.GetKeyDown(right) && rot == 4)
        {
            rot = 1;
        }

        if (Input.GetKeyDown(left) && rot == 1)
        {
            rot = 4;
        }
        else if (Input.GetKeyDown(left) && rot == 2)
        {
            rot = 1;
        }
        else if (Input.GetKeyDown(left) && rot == 3)
        {
            rot = 2;
        }
        else if (Input.GetKeyDown(left) && rot == 4)
        {
            rot = 3;
        }

        switch (rot)
        {
            case 1:
                offset = offsetTemp;
                toRotation = new Vector3(20.5f, 0, 0);
                break;
            case 2:
                offset.x = offsetTemp.z;
                offset.z = 0;
                toRotation = new Vector3(20.5f, 90, 0);
                break;
            case 3:
                offset.x = 0;
                offset.z = -offsetTemp.z;
                toRotation = new Vector3(20.5f, 180, 0);
                break;
            case 4:
                offset.x = -offsetTemp.z;
                offset.z = 0;
                toRotation = new Vector3(20.5f, 270, 0);
                break;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(toRotation), Time.deltaTime * RotSpeed);
        transform.position = Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime*RotSpeed);
         

        
    }
}
