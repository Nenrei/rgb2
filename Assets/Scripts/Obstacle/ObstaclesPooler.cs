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

    [SerializeField] PlayerCollision playerCol;

    List<string> pooledColors = new List<string>();
    string lastPooledColor;

    int goldenCounter;
    int goldenGoalCount;
    bool spawnGolden;

    public float Speed { get => speed; set => speed = value; }
    public float MinSpawnDelay { get => minSpawnDelay; set => minSpawnDelay = value; }
    public float MaxSpawnDelay { get => maxSpawnDelay; set => maxSpawnDelay = value; }
    public List<string> PooledColors { get => pooledColors; set => pooledColors = value; }

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
        if (playerCol.Lifes < 3 && PlayerPrefs.GetInt("score") > 10)
        {
            if (goldenGoalCount == 0)
            {
                goldenGoalCount = Random.Range(15, 45);
                //goldenGoalCount = 4;
                spawnGolden = false;
            }
            else if (goldenGoalCount == goldenCounter)
            {
                goldenGoalCount = 0;
                goldenCounter = 0;
                spawnGolden = true;
                //Debug.Log("Pool golden");
            }

            goldenCounter++;
        }

        GameObject availableObstacle = GetAvailableObstacle();
        availableObstacle.GetComponent<RandomColorAssignation>().SpawnGolden = spawnGolden;
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

    public void SetObstacleColor(string newColor)
    {

        if (PooledColors.Count == 0)
        {
            PooledColors.Add(newColor);
            lastPooledColor = newColor;

            //Debug.Log("Se agrega un " + lastPooledColor + " a la lista");
        }
        else if(PooledColors.Count > 0 && PooledColors.Count < 3)
        {
            foreach(string s in PooledColors)
            {
                if(s != newColor)
                {
                    PooledColors.Clear();
                    //Debug.Log("Ha salido un " + newColor + ", se limpia la lista de " + lastPooledColor);
                    break;
                }
            }

            PooledColors.Add(newColor);
            lastPooledColor = newColor;

            //Debug.Log("Se agrega un " + lastPooledColor + " a la lista");
        }
        else // más de 3 del mismo color
        {
            //Debug.Log("Han salido mas de 3 " + lastPooledColor + " seguidos");
            PooledColors.Clear();
        }

    }

}
