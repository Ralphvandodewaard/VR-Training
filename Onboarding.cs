using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Onboarding : MonoBehaviour {
    
    public GameObject dialogueObject;
    private TextMeshProUGUI textComponent;

    private Vector2 thumbstickInput;
    private bool choiceLocked;

    private int counter = 0;
    
    void Start() {
        textComponent = dialogueObject.GetComponent<TextMeshProUGUI> ();
    }

    void Update() {
        if (counter == 0) {
            Invoke ("StartOnboarding", 5f);
        }

        if (counter == 1) {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == 1) {
                choiceLocked = true;
                textComponent.text = "Wijs een voorwerp aan en gebruik vervolgens de thumbstick om te kiezen wat je met het voorwerp wilt doen.";
                counter = counter + 1;
            }
            if (choiceLocked) {
                if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == 0) {
                    textComponent.text = "Wijs een voorwerp aan en gebruik vervolgens de thumbstick om te kiezen wat je met het voorwerp wilt doen.";
                    choiceLocked = false;
                    counter = counter + 1;
                }
            }
        }

        if (counter == 2) {
            thumbstickInput = (OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch));
            if (thumbstickInput.x != 0 && thumbstickInput.y != 0) {
                choiceLocked = true;
            }
            if (choiceLocked) {
                if (thumbstickInput.x == 0 && thumbstickInput.y == 0) {
                    textComponent.text = "Druk op de B-knop om te bekijken welke voorwerpen je hebt gemarkeerd.";
                    choiceLocked = false;
                    counter = counter + 1;
                }
            }
        }
        
        if (OVRInput.GetDown (OVRInput.Button.Two)) {
            if (counter == 3) {
                textComponent.text = "Druk nog een keer op de B-knop om dit overzicht weer te sluiten.";
            } else if (counter == 4) {
                textComponent.text = "Druk op de A-knop om in de goede richting gestuurd te worden.";
            }
            counter = counter + 1;
        }

        if (OVRInput.GetDown (OVRInput.Button.One)) {
            if (counter == 5) {
                textComponent.text = "Gebruik de deur als je klaar bent om naar het plaats delict te gaan.";
            }
        }
    }

    void StartOnboarding () {
        textComponent.text = "Houd de trigger ingedrukt en wijs naar een locatie om er naartoe te bewegen.";
        counter = counter + 1;
    }
}
