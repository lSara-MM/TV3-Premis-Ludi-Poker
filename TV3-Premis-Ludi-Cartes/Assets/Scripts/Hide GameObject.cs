using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAndHideGameObject : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] List<GameObject> gameObjects;

    [SerializeField] GameObject goActive;
    [SerializeField] GameObject goInactive;

    // Forgive me
    [SerializeField] GameObject buttonActive;

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
            go.SetActive((!go.activeInHierarchy || (mode > 0)) && !(mode < 0)); //Si mode <0 entonces solo puede desactivar porque siempre sera falso en el and, si mode>0 siempre sera cierto porque true en el or
        }

    }

    public void OpenClose()
    {
        goActive.SetActive(!goActive.activeSelf);
    }

    public void SetActive() 
    {
        goActive.SetActive(true);
        goInactive.SetActive(false);

        gameObject.GetComponent<Button>().interactable = false;
        buttonActive.GetComponent<Button>().interactable = true;
    }
}
