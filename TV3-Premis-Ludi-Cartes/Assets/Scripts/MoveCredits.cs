using System.Collections;
using System.Collections.Generic;
using UnityEditor.DeviceSimulation;
using UnityEngine;

public class MoveCredits : MonoBehaviour
{
    [SerializeField] RectTransform picture;
    [SerializeField] Camera screenCamera;
    float dt = 0;
    public float waitMove = 0.1f;
    public int jumps = 20;

    float endOfCanvas; //This asumes that is on the border
    // Start is called before the first frame update
    void Start()
    {
        if(picture == null) 
        {
            picture = this.gameObject.GetComponent<RectTransform>();
        }
        endOfCanvas = picture.rect.yMin;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.gameObject.GetComponent<SwitchScene>().ChangeScene("IntroScene");
        }

        int mult = 1; //Make credits go faster

        if (Input.GetKey(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Stationary)) { mult = 3; } //If we press space or we tap
        
        dt += Time.deltaTime;

        if (picture.anchoredPosition.y < -endOfCanvas*3.4) //Con tres le asoma la puntita
        {
            if (dt > waitMove)
            {
                picture.anchoredPosition = new Vector2(picture.anchoredPosition.x, picture.anchoredPosition.y + jumps*mult);
                dt = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)) 
        {
            this.gameObject.GetComponent<SwitchScene>().ChangeScene("IntroScene");
        }
        else if(dt > 8f) 
        {
            this.gameObject.GetComponent<SwitchScene>().ChangeScene("IntroScene");
        }
    }
}
