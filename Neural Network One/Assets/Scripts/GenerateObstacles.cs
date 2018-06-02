using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour {

    public GameObject obstacle;
    public int numberofobstacles;
    public float xMax;
    public float xMin;
    public float zMax;
    public float zMin;
    
    
	// Use this for initialization
	void Start () {
		for(int i = 0; i < numberofobstacles; i++)
        {
            Vector3 position = new Vector3(Random.Range(xMin, xMax), 0f, Random.Range(zMin, zMax));
            Instantiate(obstacle, position, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
