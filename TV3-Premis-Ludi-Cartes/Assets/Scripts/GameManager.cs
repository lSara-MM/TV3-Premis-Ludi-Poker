using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GenerateData csGenerateData;

    [SerializeField] private int goalScore;
    [SerializeField] private int playerScore = 0;

    [SerializeField] private PlayCards cs_PlayCards;

    [SerializeField] GameObject playerScoreCanvas;
    [SerializeField] GameObject scoreCanvas;

    [SerializeField] PlayCards playCards; //We need a reference to the script to see if the player has lost.

    //Value that each scored card gives, this is not a base number due to being able to be upgraded (maybe should go at data)
    public int validatedCardScore = 35;
    public int equalCardScore = 30;


    // Start is called before the first frame update
    void Start()
    {
        csGenerateData = GameObject.FindWithTag("Data").GetComponent<GenerateData>();

        goalScore = SetGoalScore(csGenerateData.playerLvl);

        //Set the text renderer to show score to win
        playerScoreCanvas.GetComponent<TextMeshProUGUI>().text = playerScore.ToString();
        scoreCanvas.GetComponent<TextMeshProUGUI>().text = goalScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void CalculateScore(int validatedCards, int equalCards)
    {
        //We need to add a +1 in each as we are counting combos, but a combo is allways composed of 2 cards (this method don't take into accont more than one combo but is simpler)
        int valCards = validatedCards +1;
        int eqCards = equalCards+1;

        if(valCards == 1) { valCards = 0; } //If there is only one point there is no combo
        if (eqCards == 1) { eqCards = 0; } //If there is only one point there is no combo


        int validatedScoredPoints = valCards * (valCards+1) / 2 * (valCards/2) * validatedCardScore;
        int equalScoredPoints = eqCards * (eqCards + 1) / 2 * equalCardScore;

        playerScore += validatedScoredPoints + equalScoredPoints;

        //Modify current punctution
        playerScoreCanvas.GetComponent<TextMeshProUGUI>().text = playerScore.ToString();
    }

    public void CalculateScore(List<List<Word>> listCombos)
    {
        for(int i = 0; i < listCombos.Count; i++) 
        {
            int n = listCombos[i].Count;
            //Once we detect if it was a equals or validation combo we do things
            if (listCombos[i][0].Validate(listCombos[i][1].type))
            {
                playerScore += n * (n + 1) / 2 * (n / 2) * validatedCardScore;
            }
            else
            {
                playerScore += n * (n + 1) / 2 * equalCardScore;
            }
        }

        //Modify current player score
        playerScoreCanvas.GetComponent<TextMeshProUGUI>().text = playerScore.ToString();

        //See if the player has winned
        CheckWinOrLose();
    }

    int SetGoalScore(int level) 
    {
        int baseScore = 300;
        int linearScaling = level * 45;
        int escalingDificulty = (level / 5) * 150;
        int exponentialDificulty = (level / 3) * (level / 3 + 1) / 2 * 45;

        return baseScore+linearScaling+escalingDificulty+exponentialDificulty;
    }

    public IEnumerator CheckWinOrLose() 
    {
        if (playerScore >= goalScore) 
        {
            yield return 0.5f; //Wait before winning

            this.gameObject.GetComponent<SwitchScene>().ChangeScene("Deck Upgrade Scene");
        }
        else if(playCards.GetNumberPlays() == 0) //Only if we haven't won and we have 0 hands to play we lose.
        {
            yield return 0.5f; //Wait before losing

            //Lose
        }
    }
}
