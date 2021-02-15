using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notepad : MonoBehaviour {
    
    public List<string> markedObjectsForInterest = new List<string> ();
    public List<string> markedObjectsForTaking = new List<string> ();
    public List<string> markedObjectsForSpecialist = new List<string> ();

    public GameObject notepad;
    private Canvas notepadCanvas;
    public GameObject textObject;
	private TextMeshProUGUI textComponent;

    void Start() {
        textComponent = textObject.GetComponent<TextMeshProUGUI> ();
        notepadCanvas = notepad.GetComponent<Canvas> ();
    }

    void Update() {
        if (OVRInput.GetDown (OVRInput.Button.Two)) {
            if (!notepadCanvas.enabled) {
                notepadCanvas.enabled = true;
                
                textComponent.text = "<#FFFF00>" + "Interessant:" + "</color>" + "\n";
                foreach (string markedObject in markedObjectsForInterest) {
                    textComponent.text += "- " + markedObject + "\n";
                }

                textComponent.text += "\n" + "<#00FF00>" + "In beslag nemen:" + "</color>" + "\n";
                foreach (string markedObject in markedObjectsForTaking) {
                    textComponent.text += "- " + markedObject + "\n";
                }

                textComponent.text += "\n" + "<#00BEFF>" + "Specialist:" + "</color>" + "\n";
                foreach (string markedObject in markedObjectsForSpecialist) {
                    textComponent.text += "- " + markedObject + "\n";
                }
            } else {
                notepadCanvas.enabled = false;
            }
        }
    }
}
