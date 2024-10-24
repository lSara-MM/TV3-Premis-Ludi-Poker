using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GenerateData csGenerateData;

    [SerializeField] private int goalScore;
    [SerializeField] private int playerScore;
    public int currentLevel = 1;

    [SerializeField] private PlayCards cs_PlayCards;

    // Start is called before the first frame update
    void Start()
    {
        //csGenerateData = GameObject.Find("Data").GetComponent<GenerateData>();

        //goalScore = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void CalculateScore()
    {
        //for (int i = 0; i < cs_PlayCards.playedCards.co; i++)
        //{

        //}
    }

    int SetGoalScore(int level) 
    {
        int baseScore = 300;
        int linearScaling = level * 45;
        int escalingDificulty = (level / 5) * 150;
        int exponentialDificulty = (level / 3) * (level / 3 + 1) / 2 * 45;

        return baseScore+linearScaling+escalingDificulty+exponentialDificulty;
    }
}
