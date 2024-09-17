using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class button_functions : MonoBehaviour
{
// firt start of the application
    public GameObject digitalPenUIPanel;

    void Start()
    {
        digitalPenUIPanel.SetActive(false);
    }




// for debugging of button elements
    public void checkIfClickable_for_debbugging(Button button)
    {
        Debug.Log("Button "+ button +" is Clicked");
    }

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
