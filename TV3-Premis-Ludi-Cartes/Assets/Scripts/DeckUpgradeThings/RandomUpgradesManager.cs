using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] List<GameObject> UIobjects;
    [SerializeField] List<int> upgradeOptions; //Esto es un WIP, la idea seria cojer de una lista de structs con un ENUM para la funcion y rareza
    int op1 = -1, op2 = -1; //To not repeat type of upgrades

    public enum UPGRADES_RARITIES 
    {
        COMMON,
        RARE,
        LEGENDARY
    }
    public UPGRADES_RARITIES ownRarity; //Esto es temporal, tendra que ser asignarle a cada 

    void Start()
    {
        foreach(GameObject go in UIobjects) 
        {
            int rarity = Random.Range(1, 20); 
            if (rarity < 12) //Aqui habria que setearlo en el go no lo de ownRarity
            {
                ownRarity = UPGRADES_RARITIES.COMMON; 
                
            }
            else if (rarity > 19)
            {
                ownRarity = UPGRADES_RARITIES.LEGENDARY;
            }
            else 
            {
                ownRarity = UPGRADES_RARITIES.RARE;
            }

            //Here would be a switch based on rarity
            switch (ownRarity)
            {
                case UPGRADES_RARITIES.COMMON:
                    go.GetComponent<Image>().color = Color.green; //Green
                    break;
                case UPGRADES_RARITIES.RARE:
                    go.GetComponent<Image>().color = Color.cyan; //Cyan
                    break;
                case UPGRADES_RARITIES.LEGENDARY:
                    go.GetComponent<Image>().color = new Color(1,0,1); //Purple
                    break;
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
