using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    //static public Dictionary<WORD_TYPES, bool> wordsCheckDictionarySubstantiu = new Dictionary<WORD_TYPES, bool>();
    //static public Dictionary<WORD_TYPES, bool> wordsCheckDictionaryAdjectiu = new Dictionary<WORD_TYPES, bool>();
    //static public Dictionary<WORD_TYPES, bool> wordsCheckDictionaryVerb  = new Dictionary<WORD_TYPES, bool>();
    //static public Dictionary<WORD_TYPES, bool> wordsCheckDictionaryAdverbi = new Dictionary<WORD_TYPES, bool>();
    //static public Dictionary<WORD_TYPES, bool> wordsCheckDictionaryArticle = new Dictionary<WORD_TYPES, bool>();
    //static public Dictionary<WORD_TYPES, bool> wordsCheckDictionaryPronom = new Dictionary<WORD_TYPES, bool>();
     
    static public Dictionary<WORD_TYPES, Dictionary<WORD_TYPES, bool>> wordsCheckDictionary =
        new Dictionary<WORD_TYPES, Dictionary<WORD_TYPES, bool>>();

    static public void CreateWordsCombinationCheck(WORD_TYPES type, bool wSubstantiu, bool wAdjectiu, bool wVerb, bool wAdverbi, bool wArticle, bool wPronom)
    {
        wordsCheckDictionary.Add(type, new Dictionary<WORD_TYPES, bool>() { { WORD_TYPES.SUBSTANTIU, wSubstantiu } });
        wordsCheckDictionary.Add(type, new Dictionary<WORD_TYPES, bool>() { { WORD_TYPES.ADJECTIU, wAdjectiu } });
        wordsCheckDictionary.Add(type, new Dictionary<WORD_TYPES, bool>() { { WORD_TYPES.VERB, wVerb } });
        wordsCheckDictionary.Add(type, new Dictionary<WORD_TYPES, bool>() { { WORD_TYPES.ADVERBI, wAdverbi } });
        wordsCheckDictionary.Add(type, new Dictionary<WORD_TYPES, bool>() { { WORD_TYPES.ARTICLE, wArticle } });
        wordsCheckDictionary.Add(type, new Dictionary<WORD_TYPES, bool>() { { WORD_TYPES.PRONOM, wPronom } });

        //Globals.wordsCheckDictionarySubstantiu.Add(WORD_TYPES.SUSTANTIU, wSubstantiu);
        //Globals.wordsCheckDictionaryAdjectiu.Add(WORD_TYPES.ADJECTIU, wAdjectiu);
        //Globals.wordsCheckDictionaryVerb.Add(WORD_TYPES.VERB, wVerb);
        //Globals.wordsCheckDictionaryAdverbi.Add(WORD_TYPES.ADVERBI, wAdverbi);
        //Globals.wordsCheckDictionaryArticle.Add(WORD_TYPES.ARTICLE, wArticle);
        //Globals.wordsCheckDictionaryPronom.Add(WORD_TYPES.PRONOM, wPronom);
    }
}
