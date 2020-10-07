using UnityEngine;

public class Random2DRotation : MonoBehaviour {
    [SerializeField] float speed = 10f;
    [SerializeField] Transform[] objectsToRotate;

    private Vector3[] rotatioTypes =
    {
        new Vector3(0,0,-1),
        new Vector3(0,0,1)
    };

    private Vector3 rotationType;

    private void OnEnable()
    {
        rotationType = rotatioTypes[Random.Range(0, 2)];
    }
	
	void FixedUpdate () {
        if (rotationType != null && rotationType != Vector3.zero)
        {
            foreach (Transform t in objectsToRotate)
            {
                t.Rotate(rotationType, speed * Time.deltaTime);
            }
        }
    }
}
