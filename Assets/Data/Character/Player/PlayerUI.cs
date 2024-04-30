using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : Player
{
    public GameObject[] skills;
    private GameObject interactButton, noteUI;
    public GameObject noteContent;
    private const float DISABLE_COLOR = 0.6509434f;
    private float dashCoolDownTime = 1;
    [SerializeField] private GameObject camera;
    private void Start() {
        if (!photonView.IsMine)
        {
            camera.SetActive(false);
        }
        else
        {
            SetUI();
        }

        playerPunCallBack.photonView.RPC("SetUIOther", Photon.Pun.RpcTarget.Others);
    }
    public void SetUI()
    {
        interactButton = GameObject.FindWithTag("InteractButton");
        interactButton.SetActive(false);
        noteUI = GameObject.FindWithTag("NoteUI");
        skills[0] = GameObject.FindGameObjectWithTag("Dash");
        skills[1] = GameObject.FindGameObjectWithTag("HighJump");
        skills[2] = GameObject.FindGameObjectWithTag("LowGravity");
        noteContent = GameObject.FindGameObjectWithTag("Content");
    }
    private void Update() {
        if(dashCoolDownTime < 1){
            dashCoolDownTime += 0.2f * Time.deltaTime;
            CoolDownDash(dashCoolDownTime);
        } else {
            playerSkillController.ActiveDashing();
        }
    }
    public void UpdateSkill(int activeSkill){
        if (photonView.IsMine)
        {
            for (int i = 1; i < 3; i++)
            {
                if (i != activeSkill)
                {
                    skills[i].GetComponentsInChildren<Image>()[0].color = new Color(DISABLE_COLOR, DISABLE_COLOR, DISABLE_COLOR, 1);
                    skills[i].GetComponentsInChildren<Image>()[1].color = new Color(DISABLE_COLOR, DISABLE_COLOR, DISABLE_COLOR, 1);
                }
                else
                {
                    skills[i].GetComponentsInChildren<Image>()[0].color = new Color(255, 255, 255, 1);
                    skills[i].GetComponentsInChildren<Image>()[1].color = new Color(255, 255, 255, 1);
                }
            }
        }
    }
    public void DisableDash(){
        if (photonView.IsMine)
        {
            skills[0].GetComponentsInChildren<Image>()[0].color = new Color(DISABLE_COLOR, DISABLE_COLOR, DISABLE_COLOR, 1);
            skills[0].GetComponentsInChildren<Image>()[1].color = new Color(DISABLE_COLOR, DISABLE_COLOR, DISABLE_COLOR, 1);
            dashCoolDownTime = DISABLE_COLOR;
        }
    }
    public void CoolDownDash(float step){
        if (photonView.IsMine)
        {
            skills[0].GetComponentsInChildren<Image>()[0].color = new Color(step, step, step, 1);
            skills[0].GetComponentsInChildren<Image>()[1].color = new Color(step, step, step, 1);
        }
    }
    public void DisplayInterractButton(){
        interactButton.SetActive(true);
    }
    public void HideInteractButton(){
        interactButton.SetActive(false);
    }
    public void ReadNote(bool toggle,string noteContentString){
        if(toggle){
            noteContent.GetComponent<Text>().text = noteContentString;
            noteUI.GetComponent<Animator>().SetTrigger("Appear");
        } else {
            noteUI.GetComponent<Animator>().SetTrigger("Disappear");
        }

    }
}
