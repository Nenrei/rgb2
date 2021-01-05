using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClassicObstacle : MonoBehaviour
{
    [SerializeField] string color;
    [SerializeField] bool canMove;
    [SerializeField] float speed;
    [SerializeField] float maxY;
    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject particles;
    [SerializeField] GameObject trailParticles;
    [SerializeField] ChangeColorOverTime changeColor;
    [SerializeField] TutorialClassicController tuto;

    public bool CanMove { get => canMove; set => canMove = value; }
    public float Speed { get => speed; set => speed = value; }
    public float MaxY { get => maxY; set => maxY = value; }
    public string Color { get => color; set => color = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            Vector3 targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 10f, transform.localPosition.z);

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, Time.deltaTime * Speed);

            if (transform.position.y < MaxY)
            {
                StartCoroutine(ResetObstacle());
            }
        }
    }

    private void OnEnable()
    {
        transform.localPosition = new Vector3(Random.Range(-2, 3), 0f, 0f);
        CanMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Color == changeColor.ColorNames[changeColor.GoldIndex]) 
                StartCoroutine(tuto.GoldenOK());
            else if (DieOnCollision(Color))
                StartCoroutine(tuto.ColorKO());
            else
                StartCoroutine(tuto.ColorOK());
        }
    }

    public IEnumerator ResetObstacle()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        particles.SetActive(true);
        obstacle.SetActive(false);
        trailParticles.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        transform.localPosition = new Vector3(Random.Range(-2, 3), 0f, 0f);
        particles.SetActive(false);
        trailParticles.SetActive(true);
        obstacle.SetActive(true);
        GetComponent<CircleCollider2D>().enabled = true;
    }

    public bool DieOnCollision(string obstacleColor)
    {
        if (changeColor == null)
            changeColor = GameObject.Find("GameController").GetComponent<ChangeColorOverTime>();

        if (changeColor.ChangingColor)
        {
            if (obstacleColor != changeColor.GetCurrentColor() && (changeColor.ChangeTime > 0.6f))
            {
                return true;
            }
        }
        else
        {
            if (obstacleColor != changeColor.GetCurrentColor())
            {
                return true;
            }
        }

        return false;
    }

}
