using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<WordsCombinationCheck> propsDataList = ReadCSV.Read<WordsCombinationCheck>("CSV/Props");

        foreach (WordsCombinationCheck data in propsDataList)
        {
            Globals.CreateWordsCombinationCheck(data.type,
                data.wSubstantiu, data.wAdjectiu, data.wVerb, data.wAdverbi, data.wArticle, data.wPronom);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
