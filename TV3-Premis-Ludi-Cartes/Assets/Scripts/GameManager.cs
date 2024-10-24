using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GenerateData csGenerateData;

    [SerializeField] private int goalScore;
    [SerializeField] private int playerScore;

    [SerializeField] private PlayCards cs_PlayCards;

    [SerializeField] GameObject scoreCanvas;

    // Start is called before the first frame update
    void Start()
    {
        csGenerateData = GameObject.FindWithTag("Data").GetComponent<GenerateData>();

        goalScore = SetGoalScore(csGenerateData.playerLvl);

        //Set the text renderer to show score to win
        scoreCanvas.GetComponent<TextMeshProUGUI>().text = goalScore.ToString();
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
