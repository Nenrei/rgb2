using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    public GameObject player;
    public bool mouseEntered;
	
	// Update is called once per frame
	void Update () {
        if (mouseEntered)
        {
            /*if (Input.GetMouseButtonDown(0) && !player.GetComponent<PlayerMovement>().gameStarted)
            {
                player.GetComponent<PlayerMovement>().StartGame();

            }*/

            if (/*player.GetComponent<PlayerMovement>().gameStarted && */(Input.touches.Length > 0 || Input.GetMouseButton(0)))
            {

                // player.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, player.transform.position.y, player.transform.position.z);
                player.GetComponent<PlayerMovement>().MovePlayer();
            }
        }
	}

    private void OnDisable()
    {
        mouseEntered = false;
    }

    public void OnMouseEnter()
    {
        mouseEntered = true;
    }

    public void OnMouseExit()
    {
        mouseEntered = false;
    }
}
