using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHideGameObject : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] List<GameObject> gameObjects;

    [SerializeField] GameObject goActive;
    [SerializeField] GameObject goInactive;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideGO(int mode = 0) //Default activa y desactiva
    {
        
        foreach (GameObject go in gameObjects)
        {
            go.SetActive( (!go.activeInHierarchy || (mode>0)) && !(mode<0)); //Si mode <0 entonces solo puede desactivar porque siempre sera falso en el and, si mode>0 siempre sera cierto porque true en el or
        }

    }

    public void SetActive() // No entiendo HideGO, hago esto feo
    {
        goActive.SetActive(true);
        goInactive.SetActive(false);
    }
}
