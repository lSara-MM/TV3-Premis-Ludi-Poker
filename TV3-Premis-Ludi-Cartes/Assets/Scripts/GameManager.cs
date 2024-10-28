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

    [SerializeField] PlayCards playCards; // We need a reference to the script to see if the player has lost.

    [SerializeField] private float delay;

    // UI
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI goalScoreText;

    // Audio
    [SerializeField] AudioClip winClip;
    [SerializeField] AudioClip loseClip;


    //Value that each scored card gives, this is not a base number due to being able to be upgraded (maybe should go at data)


    // Start is called before the first frame update
    void Start()
    {
        csGenerateData = GameObject.FindWithTag("Data").GetComponent<GenerateData>();

        goalScore = SetGoalScore(csGenerateData.playerLvl);

        //Set the text renderer to show score to win
        //playerScoreCanvas.GetComponent<TextMeshProUGUI>().text = playerScore.ToString();
        playerScoreText.text = playerScore.ToString();
        goalScoreText.text = goalScore.ToString();

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


        int validatedScoredPoints = valCards * (valCards+1) / 2 * (valCards/2) * csGenerateData.validatedCardScore;
        int equalScoredPoints = eqCards * (eqCards + 1) / 2 * csGenerateData.equalCardScore;

        playerScore += validatedScoredPoints + equalScoredPoints;

        //Modify current punctution
        playerScoreText.text = playerScore.ToString();
    }

    public void CalculateScore(List<List<Word>> listCombos)
    {
        for(int i = 0; i < listCombos.Count; i++) 
        {
            int n = listCombos[i].Count;
            //Once we detect if it was a equals or validation combo we do things
            if (listCombos[i][0].Validate(listCombos[i][1].type))
            {
                playerScore += n * (n + 1) / 4 * (n / 2) * csGenerateData.validatedCardScore;
            }
            else
            {
                playerScore += n * (n + 1) / 4 * csGenerateData.equalCardScore;
            }
        }

        //Modify current player score
        playerScoreText.text = playerScore.ToString();

        // DeleteCards
        StartCoroutine(cs_PlayCards.DeletePlayed());

        // See if the player has winned
        StartCoroutine(CheckWinOrLose());
    }

    int SetGoalScore(int level) 
    {
        int baseScore = 500;
        int linearScaling = level * 70;
        int escalingDificulty = (level / 5) * 250;
        int exponentialDificulty = (level / 3) * (level / 3 + 1) / 2 * 60;

        return baseScore+linearScaling+escalingDificulty+exponentialDificulty;
    }

    public IEnumerator CheckWinOrLose() 
    {
        if (playerScore >= goalScore) 
        {
            yield return new WaitForSeconds(delay); // Wait before winning

            winScreen.SetActive(true);
            winScreen.GetComponent<AudioSource>().PlayOneShot(winClip);

            yield return new WaitForSeconds(delay); // Wait before losing

            csGenerateData.playerLvl++;
            this.gameObject.GetComponent<SwitchScene>().ChangeScene("DeckUpgradeScene");
        }
        else if(playCards.GetNumberPlays() == 0) // Only if we haven't won and we have 0 hands to play we lose.
        {
            yield return new WaitForSeconds(delay); // Wait before losing

            loseScreen.SetActive(true);
            loseScreen.GetComponent<AudioSource>().PlayOneShot(loseClip);

            yield return new WaitForSeconds(delay); // Wait before losing

            csGenerateData.Reset();
            this.gameObject.GetComponent<SwitchScene>().ChangeScene("IntroScene");
            //Lose
        }

        yield return 0;
    }
}
