using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

    CharacterController controller;
    public float speed;
    // Use this for initialization
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        controller.SimpleMove(Vector3.forward * speed);
    }
}
