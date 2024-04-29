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
    }
    private void RunParagraph(string paragraph){
        subDisplay.text = "";
        StartCoroutine(RunWord(paragraph.Length, paragraph));
    }
    IEnumerator RunWord(int length, string paragraph){
        if(wordIndex >= length){
            paragrapthIndex++;
            if(paragrapthIndex < paragraphs.Length){
                wordIndex = 0;
                yield return new WaitForSeconds(1);
                RunParagraph(paragraphs[paragrapthIndex]);
            }
        } else {
            yield return new WaitForSeconds(0.05f);
            subDisplay.text += paragraph[wordIndex];
            introSound.PlayKeyBoardSound();
            wordIndex++;
            StartCoroutine(RunWord(length, paragraph));
        }

    }
}
