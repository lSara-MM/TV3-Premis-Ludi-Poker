using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrintScoreByOptions : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GenerateData csGameData;
    public TextMeshProUGUI myText;

    //Values of combos
    int validateScore = 0;
    int equalsScore = 0;

    public enum TYPE_COMBO 
    {
        VALIDATION,
        SAME
    }

    void Start()
    {
        csGameData = GameObject.FindWithTag("Data").GetComponent<GenerateData>();

        validateScore = csGameData.validatedCardScore;
        equalsScore = csGameData.equalCardScore;

        myText.text = "";

        PrintData(0);
    }

    public void PrintData(int printMode) 
    {
        myText.text = "";

        switch ((TYPE_COMBO)printMode)
        {
            case TYPE_COMBO.VALIDATION:
                for (int i = 2; i <= 8; i++) 
                {
                    myText.text += i.ToString();
                    myText.text += " paraules = ";
                    myText.text += (i * (i + 1) / 2 * (i / 2) * csGameData.validatedCardScore);
                    myText.text += "\n";
                }
                break;
            case TYPE_COMBO.SAME:
                for (int i = 2; i <= 8; i++)
                {
                    myText.text += i.ToString();
                    myText.text += " cartes = ";
                    myText.text += (i * (i + 1) / 2 * csGameData.equalCardScore);
                    myText.text += "\n";
                }
                break;
        }
    }
}
