using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{

    [SerializeField] bool canMove;
    [SerializeField] float speed;
    [SerializeField] float maxY;

    public bool CanMove { get => canMove; set => canMove = value; }
    public float Speed { get => speed; set => speed = value; }
    public float MaxY { get => maxY; set => maxY = value; }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            Vector3 targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 10f, transform.localPosition.z);

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, Time.deltaTime * Speed);

            if(transform.position.y < MaxY)
            {
                gameObject.SetActive(false);
                CanMove = false;
                transform.localPosition = Vector3.zero;
            }
        }
    }
}
