using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingSystem : MonoBehaviour {

    public List<string> shouldBeMarkedList = new List<string> ();

    private int randomNumber;
    private Vector3 selectedObjectPosition;

    public GameObject cylinderPrefab;
    private GameObject activeCylinder;
    private Vector3 cylinderSpawnPosition;

    public GameObject dialogueObject;
    private TextMeshProUGUI textComponent;
    
    void Start() {
        cylinderSpawnPosition.y = 0f;
        textComponent = dialogueObject.GetComponent<TextMeshProUGUI> ();
    }

    void Update() {
        if (OVRInput.GetDown (OVRInput.Button.One)) {
            if (shouldBeMarkedList.Count > 0) {
                randomNumber = Random.Range (0, shouldBeMarkedList.Count);
                selectedObjectPosition = GameObject.Find(shouldBeMarkedList[randomNumber]).transform.position;
                cylinderSpawnPosition.x = selectedObjectPosition.x + 4f + Random.Range (-2f, 2f);
                cylinderSpawnPosition.z = selectedObjectPosition.z + Random.Range (-2f, 2f);
                Destroy (activeCylinder);
                activeCylinder = Instantiate (cylinderPrefab, cylinderSpawnPosition, Quaternion.identity);
                textComponent.text = "Misschien is het handig om hier nog even te kijken.";
                Invoke ("ClearText", 3f);
                //Debug.Log (shouldBeMarkedList[randomNumber]);
            } else {
                Destroy (activeCylinder);
                textComponent.text = "Het ziet ernaar uit dat je de belangrijkste sporen hebt gevonden!";
                //Debug.Log ("COMPLETE");
            }
        }
    }

    void ClearText () {
        textComponent.text = "";
    }

    public void ExitYes () {
        if (shouldBeMarkedList.Count == 0) {
            textComponent.text = "Aan deze sporen gaan we zeker wat hebben, goed werk verricht.";
        } else {
            textComponent.text = "Weet je zeker dat we hier nu al klaar zijn? Ik denk dat er nog sporen te vinden zijn.";
        }
        Invoke ("ClearText", 3f);
    }

    public void ExitNo () {
        
    }

}
