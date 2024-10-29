using System.Collections;
using System.Collections.Generic;
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
        if (picture.anchoredPosition.y < -endOfCanvas*2.9) //Con tres le asoma la puntita
        {
            dt += Time.deltaTime;

            if (dt > waitMove)
            {
                picture.anchoredPosition = new Vector2(picture.anchoredPosition.x, picture.anchoredPosition.y + jumps);
                dt = 0;
            }
        }
        
    }
}
