using System.Collections;
using System.Collections.Generic;
using System;

public class NeuralNetwork : IComparable<NeuralNetwork>{
    private int[] layers;
    private float[][] neurons; // matrix of all the neurons
    private float[][][] weights; // matrix of all the weights between the neurons

    private float fitness;

    // Constructor starts by creating random weights
    public NeuralNetwork(int[] sentlayers) {
        this.layers = new int[sentlayers.Length];
        for(int i = 0; i < sentlayers.Length; i++)
        {
            this.layers[i] = sentlayers[i];
        }

        InitNeurons();
        InitWeights();
    }

    public NeuralNetwork(NeuralNetwork copyNetwork) {
        layers = new int[copyNetwork.layers.Length];
        for(int i = 0; i< copyNetwork.layers.Length; i++)
        {
            layers[i] = copyNetwork.layers[i];
        }

        InitNeurons();
        InitWeights();
        CopyWeights(copyNetwork.weights);
    }

    private void CopyWeights(float[][][] copyWeights) {
        for (int i = 0; i < weights.Length; i++) // iterate through all the layers
        {
            for (int j = 0; j < weights[i].Length; j++) // iterate through all nuerons in current layer
            {
                for (int k = 0; k < weights[i][j].Length; k++) // iterate through all connections this node has to previous layer
                {
                    weights[i][j][k] = copyWeights[i][j][k];
                }
            }
        }
    }

    private void InitNeurons() {
        List<float[]> neuronsList = new List<float[]>();
        for(int i = 0; i < layers.Length; i++) // run through all layers
        {
            neuronsList.Add(new float[layers[i]]); // add the layer to the neuron list
        }

        neurons = neuronsList.ToArray(); // switch the neuron list to an array
    }
    private void InitWeights() {
        List<float[][]> weightsList = new List<float[][]>(); // weights list which will later convert into 3d array

        for (int i = 1; i < layers.Length; i++) // iterate over all neurons that have a weight value
        {
            List<float[]> layerWeightsList = new List<float[]>();

            int neuronsInPreviousLayer = layers[i - 1];

            for(int j = 0; j < neurons[i].Length; j++) // iterate over all neurons in the current layer
            {
                float[] neuronWeights = new float[neuronsInPreviousLayer];
                // set the weights radomly between 1 and -1
                for(int k = 0; k < neuronsInPreviousLayer; k++) // iterate over all neurons in the previous layers and set weights radomly
                {
                    // randomize weights to neuron weights
                    neuronWeights[k] = UnityEngine.Random.Range(-0.5f, 0.5f);
                }
                layerWeightsList.Add(neuronWeights); // add neuron weights of this current layer to layer weights
            }

            weightsList.Add(layerWeightsList.ToArray()); // add this layers weights converted into 2d array into weights list
        }

        weights = weightsList.ToArray(); // convert list to 3d array
    }

    public float[] FeedForward(float[] inputs) {
        // add inputs to the neuron matrix
        for(int i = 0; i < inputs.Length; i++)
        {
            neurons[0][i] = inputs[i];
        }
        // iterate over all neurons and compute feedforward values
        for (int i = 1; i < layers.Length; i++)
        {
            for (int j = 0; j < neurons[i].Length; j++)
            {
                float value = 0f;

                for (int k = 0; k < neurons[i-1].Length; k++) // sum of all weights connections of this neuron weights in previous layer
                {
                    value += weights[i - 1][j][k] * neurons[i - 1][k]; 
                }

                neurons[i][j] = (float)Math.Tanh(value); // hyperbolic tangent activation
            }
        }

        return neurons[neurons.Length-1];
    }

    public void Mutate() {
        for (int i = 0; i < weights.Length; i++) // iterate through all the layers
        {
            for (int j = 0; j < weights[i].Length; j++) // iterate through all nuerons in current layer
            {
                for (int k = 0; k < weights[i][j].Length; k++) // iterate through all connections this node has to previous layer
                {
                    float weight = weights[i][j][k]; // that weight value for that connection
                    // mutate
                    float randomNumber = UnityEngine.Random.Range(0f, 100f);

                    if (randomNumber <= 2f)
                    {
                        weight *= -1;
                    }else if(randomNumber <= 4f)
                    {
                        weight = UnityEngine.Random.Range(-0.5f, 0.5f);
                    }else if(randomNumber <= 6f)
                    {
                        float factor = UnityEngine.Random.Range(0f, 1f) + 1f;
                        weight *= factor;
                    }else if(randomNumber <= 8f)
                    {
                        float factor = UnityEngine.Random.Range(0f, 1f);
                        weight *= factor;
                    }
                    // return that back as the newly mutated weight
                    weights[i][j][k] = weight;
                }
            }
        }
    }

    public void SetFitness(float fit) {
        fitness = fit;
    }
    public float GetFitness() {
        return fitness;
    }
    public void AddFitness(float fit) {
        fitness += fit;
    }

    public int CompareTo(NeuralNetwork other) {
        if (other == null) return 1;

        if (fitness > other.fitness)
            return 1;
        else if (fitness < other.fitness)
            return -1;
        else
            return 0;
    }
}
