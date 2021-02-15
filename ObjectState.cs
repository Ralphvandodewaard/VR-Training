using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState : MonoBehaviour {
    
    public bool markedForInterest;
    public bool markedForTaking;
    public bool markedForSpecialist;

    public bool shouldBeMarked;

    private Notepad notepad;
    private TrainingSystem trainingSystem;

    private bool isFound;

    void Start() {
        notepad = GameObject.FindWithTag ("Player").GetComponentInChildren<Notepad> ();

        if (shouldBeMarked) {
            trainingSystem = GameObject.FindWithTag ("Player").GetComponentInChildren<TrainingSystem> ();
            trainingSystem.shouldBeMarkedList.Add (gameObject.name);
        }
    }

    void Update() {
        if (shouldBeMarked && (markedForTaking || markedForSpecialist)) {
            if (!isFound) {
                isFound = true;
                trainingSystem.shouldBeMarkedList.Remove (gameObject.name);
            }  
        } else if (shouldBeMarked && !markedForTaking && !markedForSpecialist) {
            if (isFound) {
                isFound = false;
                trainingSystem.shouldBeMarkedList.Add (gameObject.name);
            }
        }
    }

    public void MarkForInterest() {
        if (markedForTaking) {
            notepad.markedObjectsForTaking.Remove (gameObject.name);
        } else if (markedForSpecialist) {
            notepad.markedObjectsForSpecialist.Remove (gameObject.name);
        }

        if (!markedForInterest) {
            notepad.markedObjectsForInterest.Add (gameObject.name);
        }
        
        markedForInterest = true;
        markedForTaking = false;
        markedForSpecialist = false;
    }

    public void MarkForTaking() {
        if (markedForInterest) {
            notepad.markedObjectsForInterest.Remove (gameObject.name);
        } else if (markedForSpecialist) {
            notepad.markedObjectsForSpecialist.Remove (gameObject.name);
        }

        if (!markedForTaking) {
            notepad.markedObjectsForTaking.Add (gameObject.name);
        }

        markedForInterest = false;
        markedForTaking = true;
        markedForSpecialist = false;
    }

    public void MarkForSpecialist() {
        if (markedForInterest) {
            notepad.markedObjectsForInterest.Remove (gameObject.name);
        } else if (markedForTaking) {
            notepad.markedObjectsForTaking.Remove (gameObject.name);
        }

        if (!markedForSpecialist) {
            notepad.markedObjectsForSpecialist.Add (gameObject.name);
        }

        markedForInterest = false;
        markedForTaking = false;
        markedForSpecialist = true;
    }

    public void MarkForNothing() {
        if (markedForInterest) {
            notepad.markedObjectsForInterest.Remove (gameObject.name);
        } else if (markedForTaking) {
            notepad.markedObjectsForTaking.Remove (gameObject.name);
        } else if (markedForSpecialist) {
            notepad.markedObjectsForSpecialist.Remove (gameObject.name);
        }

        markedForInterest = false;
        markedForTaking = false;
        markedForSpecialist = false;
    }
}
