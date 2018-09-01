/*
 * Fitness is defined by the distance it managed to travel
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PopulationManager : MonoBehaviour
{

    public GameObject botPrefab;
    public int populationSize = 50;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    public float trialTime = 5;
    int generation = 1;

    public Text generationText;
    public Text trialTimeText;
    public Text populationText;

    private void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 startingPos = new Vector3(this.transform.position.x + Random.Range(-2, 2),
                                             this.transform.position.y,
                                              this.transform.position.z + Random.Range(-2, 2));
            GameObject bot = Instantiate(botPrefab, startingPos, this.transform.rotation);
            bot.GetComponent<Brain>().Init();
            population.Add(bot);
        }
    }

    private GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 startingPos = new Vector3(this.transform.position.x + Random.Range(-2, 2),
                                         this.transform.position.y,
                                          this.transform.position.z + Random.Range(-2, 2));

        GameObject offspring = Instantiate(botPrefab, startingPos, Quaternion.identity);
        Brain brainOffspring = offspring.GetComponent<Brain>();

        if (Random.Range(1, 100) == 1)  // happens once in 100 times
        {
            brainOffspring.Init();
            brainOffspring.dna.Mutate();
        }
        else
        {
            brainOffspring.Init();
            brainOffspring.dna.Combine(parent1.GetComponent<Brain>().dna, parent2.GetComponent<Brain>().dna);
        }

        return offspring;
    }

    private void BreedNewPopulation()
    {
        //// sort by time alive
        //List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Brain>().timeAlive).ToList();

        //// sort by distance travelled
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Brain>().distanceTravelled).ToList();
                                                
        population.Clear();
        // breed upper half of the sorted list
        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        // destroy all parents and previous population
        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }

        generation++;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }

        UpdateUI();
    }


    private void UpdateUI()
    {
        generationText.text = "Generation: " + generation.ToString();
        trialTimeText.text = "Trial Time: " + ((int)elapsed).ToString();
        populationText.text = "Population: " + population.Count.ToString();
    }

}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System.Linq;

//public class PopulationManager : MonoBehaviour {

//    public GameObject botPrefab;
//    public int populationSize = 50;
//    List<GameObject> population = new List<GameObject>();
//    public static float elapsed = 0;
//    public float trialTime = 5;
//    int generation = 1;

//    public Text generationText;
//    public Text trialTimeText;
//    public Text populationText;

//	private void Start()
//	{
//        for (int i = 0; i < populationSize; i++)
//        {
//            Vector3 startingPos = new Vector3(this.transform.position.x + Random.Range(-2, 2),
//                                             this.transform.position.y,
//                                              this.transform.position.z + Random.Range(-2, 2));
//            GameObject bot = Instantiate(botPrefab, startingPos, this.transform.rotation);
//            bot.GetComponent<Brain>().Init();
//            population.Add(bot);
//        }
//	}

//    private GameObject Breed(GameObject parent1, GameObject parent2)
//    {
//        Vector3 startingPos = new Vector3(this.transform.position.x + Random.Range(-2, 2),
//                                         this.transform.position.y,
//                                          this.transform.position.z + Random.Range(-2, 2));

//        GameObject offspring = Instantiate(botPrefab, startingPos, Quaternion.identity);
//        Brain brainOffspring = offspring.GetComponent<Brain>();

//        if (Random.Range(1, 100) == 1)  // happens once in 100 times
//        {
//            brainOffspring.Init();
//            brainOffspring.dna.Mutate();
//        }
//        else
//        {
//            brainOffspring.Init();
//            brainOffspring.dna.Combine(parent1.GetComponent<Brain>().dna, parent2.GetComponent<Brain>().dna);
//        }

//        return offspring;
//    }

//    private void BreedNewPopulation()
//    {
//        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Brain>().timeAlive).ToList();

//        population.Clear();
//        // breed upper half of the sorted list
//        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
//        {
//            population.Add(Breed(sortedList[i], sortedList[i + 1]));
//            population.Add(Breed(sortedList[i+1], sortedList[i]));
//        }

//        // destroy all parents and previous population
//        for (int i = 0; i < sortedList.Count; i++)
//        {
//            Destroy(sortedList[i]);
//        }

//        generation++;
//    }

//	private void Update()
//	{
//        elapsed += Time.deltaTime;
//        if (elapsed >= trialTime)
//        {
//            BreedNewPopulation();
//            elapsed = 0;
//        }

//        UpdateUI();
//	}


//	private void UpdateUI()
//    {
//        generationText.text = "Generation: " + generation.ToString();
//        trialTimeText.text = "Trial Time: " + ((int)elapsed).ToString();
//        populationText.text = "Population: " + population.Count.ToString();
//    }

//}
