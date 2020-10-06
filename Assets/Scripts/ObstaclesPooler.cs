using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesPooler : MonoBehaviour
{

    public Transform MinStartX;
    public Transform MaxStartX;
    public Transform MaxY;


    public GameObject cubesStorage;

    public GameObject obstaclePrefab;
    public List<GameObject> obstacles;

    public float speed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", 0.5f, 2f);
    }

    GameObject GetAvailableObstacle()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (!obstacles[i].activeInHierarchy)
            {
                return obstacles[i];
            }
        }

        GameObject newObstacle = GameObject.Instantiate(obstaclePrefab, Vector3.zero, Quaternion.identity, cubesStorage.transform);
        return newObstacle;
    }

    void SpawnObstacle()
    {
        GameObject availableObstacle = GetAvailableObstacle();
        availableObstacle.SetActive(true);

        Vector3 position = Vector3.zero;
        position.x = Random.Range(MinStartX.position.x, MaxStartX.position.x);

        availableObstacle.transform.localPosition = position;
        availableObstacle.GetComponent<ObstacleMovement>().Speed = speed;
        availableObstacle.GetComponent<ObstacleMovement>().MaxY = MaxY.position.y;
        availableObstacle.GetComponent<ObstacleMovement>().CanMove = true;
    }

}
