using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private bool initilized = false;
    private Transform target;

  //  private float distanceToTarget;

    private NeuralNetwork net;
    //private CharacterController controller;
    private Rigidbody rb;
  //  public float speed = 2;
  //  public float RotationalSpeed = 5;

    void Start() {
        //controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        //distanceToTarget = Vector3.Distance(transform.position, target.position);
    }

    void FixedUpdate() {
        if (initilized == true)
        {
            // Angle Calculation, input 0
            // distance isnt used for anything currently
          //  float distance = Vector3.Distance(transform.position, target.position);
            float[] inputs = new float[1];
            // orig
            float angle = transform.eulerAngles.y % 360f - 90;
            if (angle < 0f)
            {
                angle += 360f;
            }
            Vector3 deltaVector = (target.position - transform.position).normalized;
            float rad = Mathf.Atan2(deltaVector.x, deltaVector.z);
            rad *= Mathf.Rad2Deg;
            // rad represents the angle of the line between the bot and the target in degrees
            rad = rad % 360;
            if (rad < 0)
            {
                rad = 360 + rad;
            }
            // rad is a value between 0 and 359
            rad = 90f - rad;
            if (rad < 0f)
            {
                rad += 360f;
            }
            rad = 360 - rad;
            rad -= angle;
            if (rad < 0)
                rad = 360 + rad;
            if (rad >= 180f)
            {
                rad = 360 - rad;
                rad *= -1f;
            }
            rad *= Mathf.Deg2Rad;
            // END OF INPUT 0
            inputs[0] = rad / (Mathf.PI);       

            float[] output = net.FeedForward(inputs);

            //controller.SimpleMove(controller.transform.forward * speed);
            //controller.transform.Rotate(0, output[0]*RotationalSpeed, 0);

            rb.velocity = 2.5f * transform.forward;
            rb.angularVelocity = new Vector3(0f, 500f * output[0], 0f);

            //net.AddFitness(distanceToTarget-distance);
            //distanceToTarget = distance;
            net.AddFitness(1f - Mathf.Abs(inputs[0]));
        }
    }

    public void Init(NeuralNetwork net, Transform thetarget) {
        this.target = thetarget;
        this.net = net;
        initilized = true;
    }
}
