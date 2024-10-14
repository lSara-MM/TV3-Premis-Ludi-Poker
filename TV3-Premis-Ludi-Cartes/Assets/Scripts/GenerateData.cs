using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateData : MonoBehaviour
{
    public List<WordsCombinationCheck> wordsCheckerDataList;
    public List<WordData> wordsDataList;

    // Start is called before the first frame update
    void Start()
    {
        wordsCheckerDataList = ReadCSV.Read<WordsCombinationCheck>("CSV/WordsCheck");

        foreach (WordsCombinationCheck data in wordsCheckerDataList)
        {
            Globals.CreateWordsCombinationCheck(data.type,
                data.wSubstantiu, data.wAdjectiu, data.wVerb, data.wAdverbi, data.wArticle, data.wPronom);
        }

        wordsDataList = ReadCSV.Read<WordData>("CSV/Props");

        foreach (WordData data in wordsDataList)
        {
            data.CreateWord(data.word, data.type);
        }
    }
}
