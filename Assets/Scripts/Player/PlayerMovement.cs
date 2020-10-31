using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 target;
    private bool movingPlayer;
    private Camera cam;

    [SerializeField] float speed = 2;
    [SerializeField] Transform targetRight, targetLeft;
    [SerializeField] Transform particles;

    [SerializeField] Transform imageToRotate;

    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingPlayer)
        {
            if (!GameController.Instance.GameStarted)
                GameController.Instance.StartGame();

            Vector3 newPos = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
            transform.position = newPos;
            particles.position = newPos;

            if (Vector3.Distance(transform.position, target) < 0.2)
            {
                movingPlayer = false;
                //anim.SetBool("moving", false);
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

        if (Vector3.Distance(transform.position, target) > 0.2)
        {
            //anim.SetBool("moving", true);
            movingPlayer = true;
        }
        
    }

}
