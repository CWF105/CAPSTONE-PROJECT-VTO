using UnityEngine;
using System.Collections;
using System.IO;

public class capture : MonoBehaviour
{
    public Camera cameraToCapture; // The camera from which the photo will be taken
    public GameObject[] uiElementsToHide; // UI elements to hide before taking the photo
    public string customFolderPath = "Photos"; // Set your custom folder path here

    private void Awake()
    {
        // Set the full path for the photos folder
        string projectPath = Application.dataPath; // Change this to Application.persistentDataPath for different platforms
        customFolderPath = Path.Combine(projectPath, customFolderPath);

        // Ensure the directory exists; if not, create it
        if (!Directory.Exists(customFolderPath))
        {
            Directory.CreateDirectory(customFolderPath);
        }
    }

    public void TakePhoto()
    {
        StartCoroutine(CapturePhoto());
    }

    private IEnumerator CapturePhoto()
    {
        // Hide UI elements
        foreach (GameObject uiElement in uiElementsToHide)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(false);
            }
        }

        // Wait for the end of the frame to ensure the UI is hidden
        yield return new WaitForEndOfFrame();

        // Capture the photo from the camera
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraToCapture.targetTexture = renderTexture;
        Texture2D photo = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        cameraToCapture.Render();

        RenderTexture.active = renderTexture;
        photo.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        photo.Apply();

        cameraToCapture.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // Determine the filename
        string photoFileName = GetUniquePhotoName("Photo.png");

        // Save the photo as a PNG file
        byte[] bytes = photo.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(customFolderPath, photoFileName), bytes);

        // Wait for a frame to ensure the photo is taken
        yield return null;

        // Show UI elements again
        foreach (GameObject uiElement in uiElementsToHide)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(true);
            }
        }

        Debug.Log("Photo saved as " + Path.Combine(customFolderPath, photoFileName));
    }

    private string GetUniquePhotoName(string baseFileName)
    {
        string filePath = Path.Combine(customFolderPath, baseFileName);
        while (File.Exists(filePath))
        {
            // Generate a random set of numbers
            string randomNumbers = Random.Range(1000, 9999).ToString();
            // Append the numbers to the base filename
            string newFileName = Path.GetFileNameWithoutExtension(baseFileName) + "_" + randomNumbers + Path.GetExtension(baseFileName);
            filePath = Path.Combine(customFolderPath, newFileName);
        }

        return Path.GetFileName(filePath); // Return just the filename
    }
}






// below code is for taking screen shot instead of capturing a photo

// using System.Collections;
// using System.IO; // Add this directive
// using UnityEngine;

// public class capture : MonoBehaviour
// {
//     public GameObject[] uiElementsToHide; // UI elements to hide before taking the screenshot
//     public string customFolderPath = "Screenshots"; // Set your custom folder path here

//     private void Awake()
//     {
//         // Set the full path for the screenshots folder
//         string projectPath = Application.dataPath; // Change this to Application.persistentDataPath for different platforms
//         customFolderPath = Path.Combine(projectPath, customFolderPath);

//         // Ensure the directory exists; if not, create it
//         if (!Directory.Exists(customFolderPath))
//         {
//             Directory.CreateDirectory(customFolderPath);
//         }
//     }

//     public void TakeScreenshot()
//     {
//         StartCoroutine(CaptureScreenshot());
//     }

//     private IEnumerator CaptureScreenshot()
//     {
//         // Hide UI elements
//         foreach (GameObject uiElement in uiElementsToHide)
//         {
//             uiElement.SetActive(false);
//         }

//         // Wait for the end of the frame to ensure the UI is hidden
//         yield return new WaitForEndOfFrame();

//         // Determine the filename
//         string screenshotFileName = GetUniqueScreenshotName("Screenshot.png");

//         // Take the screenshot and save it to the specified folder
//         ScreenCapture.CaptureScreenshot(Path.Combine(customFolderPath, screenshotFileName));

//         // Wait for a frame to ensure the screenshot is taken
//         yield return null;

//         // Show UI elements again
//         foreach (GameObject uiElement in uiElementsToHide)
//         {
//             uiElement.SetActive(true);
//         }

//         Debug.Log("Screenshot saved as " + Path.Combine(customFolderPath, screenshotFileName));
//     }

//     private string GetUniqueScreenshotName(string baseFileName)
//     {
//         string filePath = Path.Combine(customFolderPath, baseFileName);
//         while (File.Exists(filePath))
//         {
//             // Generate a random set of numbers
//             string randomNumbers = Random.Range(1000, 9999).ToString();
//             // Append the numbers to the base filename
//             string newFileName = Path.GetFileNameWithoutExtension(baseFileName) + "_" + randomNumbers + Path.GetExtension(baseFileName);
//             filePath = Path.Combine(customFolderPath, newFileName);
//         }

//         return Path.GetFileName(filePath); // Return just the filename
//     }
// }
