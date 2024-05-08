using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapButton : MonoBehaviour
{
    public GameObject bee, fla;
    public RectTransform beeName, flaName;
    private void Awake() {
        // bee = GameObject.FindWithTag("BeeAnchor");
        // fla = GameObject.FindWithTag("FlaAnchor");
        beeName = GameObject.FindWithTag("BeeName").GetComponent<RectTransform>();
        flaName = GameObject.FindWithTag("FlaName").GetComponent<RectTransform>();
    }
    public void Swap(){
        // swap model
        Vector3 beePos = bee.transform.position;
        bee.transform.position = fla.transform.position;
        fla.transform.position = beePos;
        //swap text
        Vector3 beeNamePos = beeName.position;
        beeName.position = flaName.position;
        flaName.position = beeNamePos;
    }
}
