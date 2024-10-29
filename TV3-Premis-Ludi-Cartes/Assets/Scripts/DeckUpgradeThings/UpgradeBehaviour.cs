using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeBehaviour : MonoBehaviour
{
    public GenerateData csGenerateData;
    public UnityEvent functionSelected;

    public Upgrade upgrade;
    public TMP_Text title;
    public TMP_Text description;

    // Start is called before the first frame update
    void Start()
    {
        csGenerateData = GameObject.FindWithTag("Data").GetComponent<GenerateData>();
        title.text = upgrade.title;
        description.text = upgrade.description;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Llama a la funci�n seleccionada
    public void ExecuteSelectedFunction()
    {
        functionSelected.Invoke();

        if (csGenerateData.upgradesMap.ContainsKey(upgrade))
        {
            csGenerateData.upgradesMap[upgrade]++;
        }
        else
        {
            csGenerateData.upgradesMap.Add(upgrade, 1);
        }
    }

    public void UpgradeValidation()
    {
        Debug.Log("UpgradeValidation");
        csGenerateData.validatedCardScore += 20;
    }

    public void UpgradeSameCard()
    {
        Debug.Log("UpgradeSameCard");
        csGenerateData.validatedCardScore += 15;
    }

    public void MorePlays()
    {
        Debug.Log("MorePlays");
        csGenerateData.numPlays++;
    }

    public void MoreDiscards()
    {
        Debug.Log("MoreDiscards");
        csGenerateData.numDiscards++;
    }
}
