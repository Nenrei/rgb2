using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 target;
    private bool movingPlayer;
    private Camera cam;

    public float speed = 2;
    public Transform targetRight, targetLeft;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingPlayer)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
            transform.position = newPos;
            if (transform.position.x == target.x)
            {
                movingPlayer = false;
            }

        }
    }

    public void MovePlayer()
    {
        float mouseX = cam.ScreenToWorldPoint(Input.mousePosition).x;

        if (mouseX > targetLeft.position.x && mouseX < targetRight.position.x)
        {
            target = new Vector3(mouseX, transform.position.y, transform.position.z);
        }
        movingPlayer = true;
        
    }
   
}
