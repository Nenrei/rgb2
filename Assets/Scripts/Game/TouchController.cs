using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] bool mouseEntered;
	
	// Update is called once per frame
	void Update () {
        if (mouseEntered)
        {
            if ((Input.touches.Length > 0 || Input.GetMouseButton(0)))
            {
                if (GameController.instance.GamePaused)
                {
                    GameController.instance.UnPauseGame();
                }
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
