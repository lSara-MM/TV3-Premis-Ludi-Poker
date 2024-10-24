using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WORD_TYPES //Eric: Maybe it would be a good idea to add a MAX value to be able to iterate a for of this enum in a generic way
{
    SUBSTANTIU,
    ADJECTIU,
    VERB,
    ADVERBI,
    ARTICLE,
    PRONOM
}

[System.Serializable]
public class WordData
{
    public string word { get; set; }
    public WORD_TYPES type { get; set; }

    public Word CreateWord(string word, WORD_TYPES type)
    {
        Word w = new Word(word, type);
        return w;
    }
}

[System.Serializable]
public class Word
{
    public string word;
    public WORD_TYPES type;
    public Dictionary<WORD_TYPES, bool> dictionary;

    public Word()
    {

    }

    public Word(string word, WORD_TYPES type)
    {
        this.word = word;
        this.type = type;

        this.dictionary = Globals.wordsCheckDictionary[type];   // Assign the dictionary checker
    }

    // A method to create a deep copy of the Word object (used for having a copy of the deck)
    public Word DeepCopy()
    {
        return new Word(this.word, this.type);
    }

    public bool Validate(Word w1, WORD_TYPES w2Type)
    {
        return w1.dictionary[w2Type];   // Check word2's type in dictionary
    }

    public bool Validate(WORD_TYPES w2Type)
    {
        return this.dictionary[w2Type];   // Check word2's type in dictionary
    }

    public bool Same(WORD_TYPES w2Type) 
    {
        return this.type == w2Type; //Check if is the same type
    }
}