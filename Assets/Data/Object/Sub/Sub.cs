using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sub : MonoBehaviour
{
    public string[] paragraphs;
    private Text subDisplay;
    private int paragrapthIndex = 0, wordIndex;
    private IntroSound introSound;
    private void Awake() {
        subDisplay = GetComponent<Text>();
        introSound = GetComponent<IntroSound>();
    }   
    private void Start() {
        RunSub();
    }
    public void RunSub(){
        RunParagraph(paragraphs[paragrapthIndex]);
        Debug.Log(paragraphs[paragrapthIndex]);
    }
    private void RunParagraph(string paragraph){
        subDisplay.text = "";
        introSound.PlayTypingSound();
        StartCoroutine(RunWord(paragraph.Length, paragraph));
    }
    IEnumerator RunWord(int length, string paragraph){
        if(wordIndex >= length){
            paragrapthIndex++;
            if(paragrapthIndex < paragraphs.Length){
                wordIndex = 0;
                introSound.StopPlaySound();
                yield return new WaitForSeconds(1);
                RunParagraph(paragraphs[paragrapthIndex]);
            } else {
                introSound.StopPlaySound();
            }
        } else {
            yield return new WaitForSeconds(0.05f);
            subDisplay.text += paragraph[wordIndex];
            wordIndex++;
            StartCoroutine(RunWord(length, paragraph));
        }

    }
}
