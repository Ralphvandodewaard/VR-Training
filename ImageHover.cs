using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHover : MonoBehaviour {
    
    public GameObject objectHightlight;
    private ObjectHighlight highlighting;
    private RectTransform rectTransform;
    private bool hovered;

    public bool imageOne;
    public bool imageTwo;
    public bool imageThree;
    public bool imageFour;
    public bool exitYes;
    public bool exitNo;
    
    void Start() {
        highlighting = objectHightlight.GetComponent<ObjectHighlight> ();
        rectTransform = GetComponent<RectTransform> ();
    }

    void Update() {
        if ((imageOne && highlighting.imageOneSelected) ||
            (imageTwo && highlighting.imageTwoSelected) ||
            (imageThree && highlighting.imageThreeSelected) ||
            (imageFour && highlighting.imageFourSelected) ||
            (exitYes && highlighting.exitYesSelected) ||
            (exitNo && highlighting.exitNoSelected)) {
            hovered = true;
            rectTransform.sizeDelta = new Vector2 (0.07f, 0.07f);
        } else if (hovered) {
            hovered = false;
        }

        if (!hovered) {
            rectTransform.sizeDelta = new Vector2 (0.05f, 0.05f);
        }  
    }
}
