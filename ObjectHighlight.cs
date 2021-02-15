//SOURCE: Unity- Interacting with objects with Oculus touch controller (https://www.reddit.com/r/learnVRdev/comments/ag7889/unity_interacting_with_objects_with_oculus_touch/)
//BY: Alihammza

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlight : MonoBehaviour {
    
    public LineRenderer line;
    public Transform controller;
    public LayerMask ignoreLayer;
    private GameObject selectedObject;

    public GameObject menu;
    private Canvas menuCanvas;
    private Vector2 thumbstickInput;

    public GameObject exitMenu;
    private Canvas exitMenuCanvas;

    public bool imageOneSelected;
    public bool imageTwoSelected;
    public bool imageThreeSelected;
    public bool imageFourSelected;

    public bool exitYesSelected;
    public bool exitNoSelected;

    private bool choiceLocked;

    private Outline objectOutline;
    private ObjectState objectState;

    public GameObject notepad;
    private Canvas notepadCanvas;

    public GameObject trainingSystemObject;
    private TrainingSystem trainingSystemComponent;

    void Start() {
        ignoreLayer = ~ignoreLayer;
        menuCanvas = menu.GetComponent<Canvas> ();
        exitMenuCanvas = exitMenu.GetComponent<Canvas> ();
        notepadCanvas = notepad.GetComponent<Canvas> ();
        trainingSystemComponent = trainingSystemObject.GetComponent<TrainingSystem> ();
    }

    void Update() {
        line.SetPosition (0, controller.position);

        RaycastHit hit;

        if (Physics.Raycast (controller.position, controller.forward, out hit, Mathf.Infinity, ignoreLayer)) {

            //If raycast hits object with tag 'Object'
            if (hit.collider.gameObject.tag == "Object") {
                //If hit object is different than previous selected object
                if (hit.collider.gameObject != selectedObject) {
                    //If selected object is not null
                    if (selectedObject != null) {
                        //If selected object doesn't have state
                        if (!objectState.markedForInterest && !objectState.markedForTaking && !objectState.markedForSpecialist) {
                            //Disable selected object outline
                            objectOutline.enabled = false;
                        }
                    }
                    //Set new selected object
                    selectedObject = hit.collider.gameObject;
                    objectState = selectedObject.GetComponent<ObjectState> ();
                    objectOutline = selectedObject.GetComponent<Outline> ();
                    
                    //If selected object doesn't have state
                    if (!objectState.markedForInterest && !objectState.markedForTaking && !objectState.markedForSpecialist) {
                        //Enable selected object outline
                        objectOutline.enabled = true;
                    }

                    //Close menu
                    //ResetHoverStates ();
                    //menuCanvas.enabled = false;
                //If hit object is same as previous selected object
                } else {
                    //If selected object doesn't have state
                    if (!objectState.markedForInterest && !objectState.markedForTaking && !objectState.markedForSpecialist) {
                        //Enable selected object outline
                        objectOutline.enabled = true;
                    }
                }

                //If button gets pressed
                //if (OVRInput.GetDown (OVRInput.Button.One)) {
                    //If menu is closed
                    if (menuCanvas.enabled == false) {
                        if (!notepadCanvas.enabled) {
                        //Open menu
                        if (selectedObject.name == "Deur") {
                            exitMenuCanvas.enabled = true;
                        } else {
                            menuCanvas.enabled = true;
                        }
                        }
                    //If menu is open
                    } /*else {
                        //Close menu
                        ResetHoverStates ();
                        menuCanvas.enabled = false;
                    }*/
                //}
            //If raycast hits 'non-object'
            } else {
                //If selected object is not null 
                if (selectedObject != null) {
                    //If selected object doesn't have state
                    if (!objectState.markedForInterest && !objectState.markedForTaking && !objectState.markedForSpecialist) {
                        //Disable selected object outline
                        objectOutline.enabled = false;
                    }
                }
                //Close menu
                ResetHoverStates ();
                menuCanvas.enabled = false;
                exitMenuCanvas.enabled = false;
            }
            
            //Set raycast endpoint at hit point
            line.SetPosition (1, hit.point);
        }

        if (menuCanvas.enabled == true) {
            thumbstickInput = (OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch));

            if (thumbstickInput.x >= -1f && thumbstickInput.x < -0.2f && thumbstickInput.y >= -0.71f && thumbstickInput.y < 0.71f) {
                imageOneSelected = true;
                imageTwoSelected = false;
                imageThreeSelected = false;
                imageFourSelected = false;
                choiceLocked = true;
            } else if (thumbstickInput.x >= -0.71f && thumbstickInput.x <= 0.71f && thumbstickInput.y > 0.2f && thumbstickInput.y <= 1f) {
                imageOneSelected = false;
                imageTwoSelected = true;
                imageThreeSelected = false;
                imageFourSelected = false;
                choiceLocked = true;
            } else if (thumbstickInput.x <= 1f && thumbstickInput.x > 0.2f && thumbstickInput.y >= -0.71f && thumbstickInput.y < 0.71f) {
                imageOneSelected = false;
                imageTwoSelected = false;
                imageThreeSelected = true;
                imageFourSelected = false;
                choiceLocked = true;
            } else if (thumbstickInput.x <= 0.71f && thumbstickInput.x > -0.71f && thumbstickInput.y >= -1f && thumbstickInput.y < -0.2f) {
                imageOneSelected = false;
                imageTwoSelected = false;
                imageThreeSelected = false;
                imageFourSelected = true;
                choiceLocked = true;
            }
        }

        if (exitMenuCanvas.enabled == true) {
            thumbstickInput = (OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch));

            if (thumbstickInput.x >= -1f && thumbstickInput.x < -0.2f && thumbstickInput.y >= -0.71f && thumbstickInput.y < 0.71f) {
                exitYesSelected = true;
                exitNoSelected = false;
                choiceLocked = true;
            } else if (thumbstickInput.x <= 1f && thumbstickInput.x > 0.2f && thumbstickInput.y >= -0.71f && thumbstickInput.y < 0.71f) {
                exitYesSelected = false;
                exitNoSelected = true;
                choiceLocked = true;
            }
        }

        if (choiceLocked) {
            if (thumbstickInput.x == 0 && thumbstickInput.y == 0) {
                if (imageOneSelected) {
                    objectOutline.OutlineColor = Color.yellow;
                    objectState.MarkForInterest ();
                    ResetHoverStates ();
                    menuCanvas.enabled = false;
                } else if (imageTwoSelected) {
                    objectOutline.OutlineColor = Color.green;
                    objectState.MarkForTaking ();
                    ResetHoverStates ();
                    menuCanvas.enabled = false;
                } else if (imageThreeSelected) {
                    objectOutline.OutlineColor = Color.cyan;
                    objectState.MarkForSpecialist ();
                    ResetHoverStates ();
                    menuCanvas.enabled = false;
                } else if (imageFourSelected) {
                    objectOutline.OutlineColor = Color.white;
                    objectState.MarkForNothing ();
                    ResetHoverStates ();
                    menuCanvas.enabled = false;
                } else if (exitYesSelected) {
                    trainingSystemComponent.ExitYes ();
                    ResetHoverStates ();
                    exitMenuCanvas.enabled = false;
                } else if (exitNoSelected) {
                    trainingSystemComponent.ExitNo ();
                    ResetHoverStates ();
                    exitMenuCanvas.enabled = false;
                }
            }
        }
    }

    void ResetHoverStates () {
        imageOneSelected = false;
        imageTwoSelected = false;
        imageThreeSelected = false;
        imageFourSelected = false;
        exitYesSelected = false;
        exitNoSelected = false;
        choiceLocked = false;
    }
}
