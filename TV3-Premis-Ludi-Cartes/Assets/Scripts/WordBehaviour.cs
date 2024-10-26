using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordBehaviour : MonoBehaviour
{
    public Word word;

    // Sprites
    public List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Sprite SetSprite()
    {
        return sprites[(int)word.type];
    }
}
