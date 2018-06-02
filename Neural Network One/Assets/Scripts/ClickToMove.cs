using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    public static bool isattacking;

    public float speed;
    public float howcloseyouwanttobe;
    private Vector3 position;
    public CharacterController controller;
    

    // Use this for initialization
    void Start() {
        isattacking = false;
        position = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (!isattacking)
        {
            if (Input.GetMouseButton(1))
            {
                LocatePosition();
            }
            MoveToPosition();
        }


    }
    void LocatePosition() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.tag != "Player")
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
        }
    }
    void MoveToPosition() {
        if (Vector3.Distance(transform.position, position) > howcloseyouwanttobe)
        {
            Quaternion newRotation = Quaternion.LookRotation(position - transform.position);
            newRotation.x = 0f;
            newRotation.z = 0f;
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 10);

            controller.SimpleMove(transform.forward * speed);
        }

    }
}
