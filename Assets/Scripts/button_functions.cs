using UnityEngine;
using UnityEngine.SceneManagement;

public class button_functions : MonoBehaviour
{
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
}
