using UnityEngine;
using UnityEngine.UI;

public class PlayerMaterialController : MonoBehaviour {


    public Material[] colors;
    public string[] colorNames;

    public GameObject[] walls;

    private int currColor = 0, prevColor = -1;
    public bool changingColor;

    public GameObject cube;

    public float changeSpeed = 5;
    public float changeTime;

	// Use this for initialization
	void Start () {
        ChangeMaterial();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (changingColor)
        {
            cube.GetComponent<Renderer>().material.color = Color.Lerp(cube.GetComponent<Renderer>().material.color, colors[currColor].color, changeSpeed);
            if (walls.Length == 2)
            {
                //walls[0].GetComponent<Renderer>().material.color = Color.Lerp(walls[0].GetComponent<Renderer>().material.color, colors[currColor].color, changeSpeed);
                //walls[1].GetComponent<Renderer>().material.color = Color.Lerp(walls[1].GetComponent<Renderer>().material.color, colors[currColor].color, changeSpeed);

                walls[0].GetComponent<Image>().color = Color.Lerp(walls[0].GetComponent<Image>().color, colors[currColor].color, changeSpeed);
                walls[1].GetComponent<Image>().color = Color.Lerp(walls[1].GetComponent<Image>().color, colors[currColor].color, changeSpeed);
            }
            changeTime += Time.deltaTime;

            if (cube.GetComponent<Renderer>().material.color == colors[currColor].color)
            {
                changingColor = false;
                Invoke("ChangeMaterial", 4f);
                changeTime = 0;
            }
        }
	}

    void ChangeMaterial()
    {
        if (!changingColor)
        {
            prevColor = currColor;
            currColor = ChooseNewColor();
            changingColor = true;
            changeTime = 0;
        }
    }

    int ChooseNewColor()
    {
        int newColor = Random.Range(0, colors.Length);
        if(currColor == newColor)
        {
            newColor = ChooseNewColor();
        }
        return newColor;
    }
    

    public bool DieOnCollision(string obstacleColor)
    {

        GetComponent<AudioSource>().Play();

        if (changingColor)
        {
            if (obstacleColor != colorNames[currColor] && (changeTime > 0.6f))
            {
                return true;
            }
        }
        else
        {
            if (obstacleColor != colorNames[currColor] /*&& !changingColor*/)
            {
                return true;
            }
        }

        return false;
    }

}
