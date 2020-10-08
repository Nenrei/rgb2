using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesPooler : MonoBehaviour
{

    [SerializeField] Transform MinStartX;
    [SerializeField] Transform MaxStartX;
    [SerializeField] Transform MaxY;


    [SerializeField] GameObject cubesStorage;

    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] List<GameObject> obstacles;

    [SerializeField] float speed = 10f;

    [SerializeField, Range(0f, 3f)] float minSpawnDelay = 0.5f;
    [SerializeField, Range(0f, 3f)] float maxSpawnDelay = 1.5f;

    public float Speed { get => speed; set => speed = value; }
    public float MinSpawnDelay { get => minSpawnDelay; set => minSpawnDelay = value; }
    public float MaxSpawnDelay { get => maxSpawnDelay; set => maxSpawnDelay = value; }

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
        availableObstacle.GetComponent<ObstacleMovement>().Speed = Random.Range( Speed - 0.5f, Speed + 1f );
        availableObstacle.GetComponent<ObstacleMovement>().MaxY = MaxY.position.y;
        availableObstacle.GetComponent<ObstacleMovement>().CanMove = true;
    }

    public IEnumerator WaitAndRespawn()
    {
        yield return new WaitForSeconds(Random.Range(MinSpawnDelay, MaxSpawnDelay));
        SpawnObstacle();

        StartCoroutine(WaitAndRespawn());
    }

}
