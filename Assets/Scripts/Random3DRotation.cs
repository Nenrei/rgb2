using UnityEngine;

public class Random3DRotation : MonoBehaviour {
    public float speed = 10f;

    private Vector3[] rotatioTypes =
    {
        Vector3.left,
        Vector3.left+Vector3.back,
        Vector3.left+Vector3.forward
    };

    private Vector3 rotationType;
    // Use this for initialization
    void Awake() {
        rotationType = rotatioTypes[Random.Range(0, rotatioTypes.Length)];
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotationType, speed * Time.deltaTime);
    }
}
