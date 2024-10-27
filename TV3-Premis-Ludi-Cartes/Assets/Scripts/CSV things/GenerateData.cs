using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateData : MonoBehaviour
{
    public List<Word> wordsList = new List<Word>();
    public int playerLvl = 1;

    //Card values
    //Value that each scored card gives, this is not a base number due to being able to be upgraded (maybe should go at data)
    public int validatedCardScore = 35;
    public int equalCardScore = 30;

    public int numPlays = 4;
    public int numDiscards = 4;

    void Awake()
    {
        List<WordsCombinationCheck> wordsCheckerDataList = ReadCSV.Read<WordsCombinationCheck>("CSV/WordsCheck");

        foreach (WordsCombinationCheck data in wordsCheckerDataList)
        {
            Globals.CreateWordsCombinationCheck(data.type,
                data.wSubstantiu, data.wAdjectiu, data.wVerb, data.wAdverbi, data.wArticle, data.wPronom);
        }

        List<WordData> wordsDataList = ReadCSV.Read<WordData>("CSV/WordsList");

        foreach (WordData data in wordsDataList)
        {
            Globals.wordsList.Add(data.CreateWord(data.word, data.type));
        }

        // DEBUG
        wordsList = Globals.wordsList;
    }

    public void Reset()
    {
        playerLvl = 1;

        //Set all cards upgrades to 0.

        //Value that each scored card gives, this is not a base number due to being able to be upgraded (maybe should go at data)
        validatedCardScore = 35; 
        equalCardScore = 30;
    }
}
