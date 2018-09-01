using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]       // NT: added
public class DNA {

    List<int> genes = new List<int>();  // can set your own length of chromosome
    int dnaLength = 0;      
    int maxValues = 0;      // used to set the random val in the gene when it's initialized

    public DNA(int len, int maxVal)
    {
        dnaLength = len;
        maxValues = maxVal;
        SetRandom();
    }

    // reset the gene and assign the random values
    public void SetRandom()
    {
        genes.Clear();
        for (int i = 0; i < dnaLength; i++)
        {
            genes.Add(Random.Range(0, maxValues));
        }
    }

    public void SetInt(int pos, int value)
    {
        genes[pos] = value;
    }

    // splitting sequences from parents and put them together.
    public void Combine(DNA d1, DNA d2)
    {
        for (int i = 0; i < dnaLength; i++)
        {
            if (i < dnaLength/2.0)
            {
                int c = d1.genes[i];
                genes[i] = c;
            }
            else
            {
                int c = d2.genes[i];
                genes[i] = c;
            }
        }
    }

    public void Mutate()
    {
        genes[Random.Range(0, dnaLength)] = Random.Range(0, maxValues);
    }

    public int GetGene(int pos)
    {
        return genes[pos];
    }
}
