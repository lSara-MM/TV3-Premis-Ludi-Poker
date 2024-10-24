using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    static public Dictionary<WORD_TYPES, Dictionary<WORD_TYPES, bool>> wordsCheckDictionary =
        new Dictionary<WORD_TYPES, Dictionary<WORD_TYPES, bool>>();

    static public List<Word> wordsList = new List<Word>();

    static public void CreateWordsCombinationCheck(WORD_TYPES type, bool wSubstantiu, bool wAdjectiu, bool wVerb, bool wAdverbi, bool wArticle, bool wPronom)
    {
        Dictionary<WORD_TYPES, bool> temp = new Dictionary<WORD_TYPES, bool>
        {
            { WORD_TYPES.SUBSTANTIU, wSubstantiu },
            { WORD_TYPES.ADJECTIU, wAdjectiu },
            { WORD_TYPES.VERB, wVerb } ,
            { WORD_TYPES.ADVERBI, wAdverbi },
            { WORD_TYPES.ARTICLE, wArticle},
            { WORD_TYPES.PRONOM, wPronom }
        };

        wordsCheckDictionary.Add(type, temp);
    }
}
