﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Manager : MonoBehaviour {

    public GameObject botPrefab;
    public GameObject target;

    private bool isTraining = false;
    private int[] layers = new int[] { 1, 10, 10, 10, 1 };
    private List<NeuralNetwork> networks;
    private List<Bot> botList = null;

    [Header("Genetic Settings")]
    public int populationSize = 2;
    public int generationNumber = 0;
    public int numberOfSelectedWinners = 10;

    [Header("Run Settings")]
    public float simSpeed = 2;
    public float GenTime = 15;

    [Header("Spawn Settings")]
    public Vector3 CircleCenter;
    public float CircleRadius;

    void Timer() {
        isTraining = false;
    }

	// Use this for initialization
	void Start () {
        CircleCenter = target.transform.position;
        CircleCenter.y = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = simSpeed;
        if (isTraining == false)
        {
            if(generationNumber == 0)
            {
                InitBotNeuralNetworks();
            }else
            {
                networks.Sort();

                for(int i = 0; i < populationSize; i++)
                {
                    if(i < numberOfSelectedWinners)
                    {
                        // these are the winners
                    }else
                    {
                        // losers get replaced with a random network that succeeded
                        networks[i] = new NeuralNetwork(networks[Random.Range(0, numberOfSelectedWinners)]);
                        networks[i].Mutate();                        
                    }
                }

                for(int i = 0; i < populationSize; i++)
                {
                    networks[i].SetFitness(0f);
                }
            }
            generationNumber++;
            isTraining = true;
            Invoke("Timer", GenTime);
            CreateBotBodies();
        }        
	}

    private void CreateBotBodies() {
        if(botList != null)
        {
            for(int i = 0; i < botList.Count; i++)
            {
                GameObject.Destroy(botList[i].gameObject);
            }
        }

        botList = new List<Bot>();

        for(int i = 0; i < populationSize; i++)
        {
            float SpawnAngle = Random.Range(0, 2f * Mathf.PI);
            //Bot bot = ((GameObject)Instantiate(botPrefab, new Vector3(UnityEngine.Random.Range(1f, 50f), 0.5f, UnityEngine.Random.Range(1f, 99f)), botPrefab.transform.rotation)).GetComponent<Bot>();
            Bot bot = ((GameObject)Instantiate(botPrefab, CircleCenter + new Vector3(CircleRadius * Mathf.Cos(SpawnAngle), 0.5f, CircleRadius * Mathf.Sin(SpawnAngle)), botPrefab.transform.rotation)).GetComponent<Bot>();
            bot.Init(networks[i], target.transform);
            botList.Add(bot);
        }
    }

    void InitBotNeuralNetworks() {
        /*
        if(populationSize %2 != 0)
        {
            populationSize = 20;
        }
        */

        networks = new List<NeuralNetwork>();

        for(int i = 0; i < populationSize; i++)
        {
            NeuralNetwork net = new NeuralNetwork(layers);
            net.Mutate();
            networks.Add(net);
        }
    }
}
