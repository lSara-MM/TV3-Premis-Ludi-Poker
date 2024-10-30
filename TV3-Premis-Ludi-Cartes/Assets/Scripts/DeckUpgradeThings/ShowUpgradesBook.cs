using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ShowUpgradesBook : MonoBehaviour
{
    public GenerateData csGenerateData;
    public List<GameObject> visualsList = new List<GameObject>();

    // Sprites
    public List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        csGenerateData = GameObject.FindWithTag("Data").GetComponent<GenerateData>();
        SetUpgradeVisual();
    }

    public void SetUpgradeVisual()
    {
        int i = 0;
        foreach (var upgrade in csGenerateData.upgradesList)
        {
            if (i < visualsList.Count)
            {
                visualsList[i].SetActive(true);

                if (upgrade.id < sprites.Count)
                {
                    visualsList[i].GetComponent<UpgradeVisual>().SetVisual(upgrade, sprites[upgrade.id]);
                }
                i++;
            }
        }
    }
}
