using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Diagnostics;

public class button_functions : MonoBehaviour
{
// firt start of the application
    public GameObject UIPanel;
    public GameObject texts;
    public GameObject digitalPenUIPanel;

    void Start()
    {
        digitalPenUIPanel.SetActive(false);
        texts.SetActive(true);
        UIPanel.SetActive(false);
    }


//START : opens a build in keyboard for windows 10 or 11
    public void openKeyboardOnClick()
    {
        if (Input.GetMouseButtonDown(0)) {
            OpenWindowsKeyboard();
        }
    }

    private void OpenWindowsKeyboard() {
        Process.Start("osk.exe"); 
    }

    private void CloseWindowsKeyboard() {
        Process[] oskProcess = Process.GetProcessesByName("osk");
        foreach (Process p in oskProcess)
        {
            p.Kill();  
        }
    }
// END : opens a build in keyboard for windows 10 or 11



// for debugging of button elements
    // public void checkIfClickable_for_debbugging(Button button)
    // {
    //     Debug.Log("Button "+ button +" is Clicked");
    // }

// on click open url and close the app
    public void on_click_open_then_close(string url)
    {
        Application.OpenURL(url); 
        Application.Quit(); 
    }


// change scene on click
    public void on_click_change_scene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public GameObject container;

// hide / show UI when toggle
    public void onc_toggle_hide_ui()
    {
        if(container != null) {
            Animator animate = container.GetComponent<Animator>();
            if(animate != null) {
                bool isToggle = animate.GetBool("show");

                animate.SetBool("show", !isToggle);
            }
        }
    }

    public void onClickShowPanel()
    {
        texts.SetActive(!texts.activeSelf);
        UIPanel.SetActive(!UIPanel.activeSelf);
    }



// for digital pen UI to hide or display.
    public GameObject onClickAnimate1, onClickAnimate2;
    public Button btnToDisable1, btnToDisable2;
    public bool isClicked = false;

    public void on_click_use_digital_pen()
    {    
        digitalPenUIPanel.SetActive(!digitalPenUIPanel.activeSelf);
        if ((onClickAnimate1 != null) && (onClickAnimate2 != null)) 
        {
            Animator animate1 = onClickAnimate1.GetComponent<Animator>();
            Animator animate2 = onClickAnimate2.GetComponent<Animator>();


            if ((animate1 != null) && (animate2 != null)) 
            {
                bool isClicked_1 = animate1.GetBool("show");
                bool isClicked_2 = animate2.GetBool("show");

                animate1.SetBool("show", !isClicked_1);           
                animate2.SetBool("show", !isClicked_2); 

                isClicked = !isClicked;
                btnToDisable1.interactable = !isClicked;
                btnToDisable2.interactable = !isClicked;
            }
        }
    }

}
