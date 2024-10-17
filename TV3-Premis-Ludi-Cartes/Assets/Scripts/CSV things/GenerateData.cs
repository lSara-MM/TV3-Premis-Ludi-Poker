using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateData : MonoBehaviour
{
    public List<Word> wordsList = new List<Word>();

    // Start is called before the first frame update
    void Start()
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
}
