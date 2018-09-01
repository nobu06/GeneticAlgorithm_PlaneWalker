/*
 * Make distance travelled to be the fitness function
 */ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class Brain : MonoBehaviour
{
    public int DNALength = 1;
    public float timeAlive;
    public DNA dna;

    public float distanceTravelled;     // NT: the Distance Challenge
    private Vector3 initialPosition;
    private Vector3 endPosition;

    private ThirdPersonCharacter m_Character;
    private Vector3 m_Move;
    private bool m_Jump = false;          //NT: added a default value
    private bool m_Crouch = false;        //NT: added
    bool alive = true;


    public int numActions = 6;      

  private void OnCollisionEnter(Collision obj)
  {
        if (obj.gameObject.tag == "death")
        {
            alive = false;
        }
  }

  public void Init()
  {
        //initialize DNA
        //0 forward
        //1 backward
        //2 left
        //3 right
        //4 jump
        //5 crouch
        dna = new DNA(DNALength, numActions);
        m_Character = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        alive = true;

        distanceTravelled = 0;
        initialPosition = gameObject.transform.position;
  }

  private void Update()
  {

  }

  private void FixedUpdate()
  {
        float xDir = 1f;
        float zDir = 1f;

        int cond = dna.GetGene(0);
        switch (cond)
        {
            case 0:
                m_Move = new Vector3(0f, 0f, zDir);
                break;

            case 1:
                m_Move = new Vector3(0f, 0f, -zDir);
                break;

            case 2:
                m_Move = new Vector3(xDir, 0f, 0f);
                break;

            case 3:
                m_Move = new Vector3(-xDir, 0f, 0f);
                break;

            case 4:
                m_Move = new Vector3(0, 0, 0);
                m_Jump = true;
                break;

            case 5:
                m_Crouch = true;
                break;
        }

        m_Character.Move(m_Move, m_Crouch, m_Jump);

        m_Jump = false;         // NT: for what? A) to prevent it from jumping continuously (c?)

        if (alive)
        {
            CalculateDistanceTravelled();

            timeAlive += Time.deltaTime;
        }
    }

    void CalculateDistanceTravelled()
    {
        endPosition = gameObject.transform.position;
        distanceTravelled = Vector3.Distance(initialPosition, endPosition);

        //Debug.Log("Distance travelled: " + distanceTravelled);
    }
}



/*
 * Ones that are fit are those that jump up and down or crouches
 */ 
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityStandardAssets.Characters.ThirdPerson;

//[RequireComponent(typeof (ThirdPersonCharacter))]
//public class Brain : MonoBehaviour
//{
//    public int DNALength = 1;
//    public float timeAlive;
//    public DNA dna;

//    private ThirdPersonCharacter m_Character;
//    private Vector3 m_Move;
//    private bool m_Jump = false;          //NT: added a default value
//    private bool m_Crouch = false;        //NT: added
//    bool alive = true;


//    public int numActions = 6;      

//	private void OnCollisionEnter(Collision obj)
//	{
//        if (obj.gameObject.tag == "death")
//        {
//            alive = false;
//        }
//	}

//	public void Init()
//	{
//        //initialize DNA
//        //0 forward
//        //1 backward
//        //2 left
//        //3 right
//        //4 jump
//        //5 crouch
//        dna = new DNA(DNALength, numActions);
//        m_Character = GetComponent<ThirdPersonCharacter>();
//        timeAlive = 0;
//        alive = true;
//	}

//	private void Update()
//	{
		
//	}

//	private void FixedUpdate()
//	{
//        float xDir = 1f;
//        float zDir = 1f;

//        int cond = dna.GetGene(0);
//        switch (cond)
//        {
//            case 0:
//                m_Move = new Vector3(0f, 0f, zDir);
//                break;

//            case 1:
//                m_Move = new Vector3(0f, 0f, -zDir);
//                break;

//            case 2:
//                m_Move = new Vector3(xDir, 0f, 0f);
//                break;

//            case 3:
//                m_Move = new Vector3(-xDir, 0f, 0f);
//                break;

//            case 4:
//                m_Move = new Vector3(0, 0, 0);
//                m_Jump = true;
//                break;

//            case 5:
//                m_Crouch = true;
//                break;
//        }

//        m_Character.Move(m_Move, m_Crouch, m_Jump);

//        m_Jump = false;         // NT: for what? A) to prevent it from jumping continuously (c?)

//        if (alive)
//        {
//            timeAlive += Time.deltaTime;
//        }
//	}
//}
