using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour {

    // Use this for initialization
    private CharacterController controller;
    public float speed = 2;
    public float RotationalSpeed = 5;

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update () {
        controller.SimpleMove(controller.transform.forward * speed);
        controller.transform.Rotate(0, RotationalSpeed, 0);

    }
}
