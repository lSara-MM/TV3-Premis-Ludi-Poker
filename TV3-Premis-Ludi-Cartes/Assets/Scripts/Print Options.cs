using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrintScoreByOptions : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GenerateData csGameData;
    private TextMeshProUGUI myText;

    //Values of combos
    int validateScore = 0;
    int equalsScore = 0;

    [SerializeField] int _printmode; // 0 = phrase combo -- 1 = same combo

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

        myText = GetComponent<TextMeshProUGUI>();

        PrintData(_printmode);
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
                    myText.text += (csGameData.validatedCardScore * i * (i + 1) * (i / 2) / 4);
                    myText.text += "\n";
                }
                break;
            case TYPE_COMBO.SAME:
                for (int i = 2; i <= 8; i++)
                {
                    myText.text += i.ToString();
                    myText.text += " cartes = ";
                    myText.text += (csGameData.equalCardScore * i * (i + 1) / 4);
                    myText.text += "\n";
                }
                break;
        }
    }
}
