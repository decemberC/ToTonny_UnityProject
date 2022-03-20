using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void CloseDisplayPannel(WebCamTextureToCloudVision target){
        ShowData._showData.ClosePannel();
        target.RebootCapture();
    }
    public void ToSceneByName(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public void StartButton() {
        SceneManager.LoadScene(2);
    }
    public void HomeButton() {
        SceneManager.LoadScene(1);
    }
    /*public void WarningButton() {
        SceneManager.LoadScene(2);
    }*/
    public void QuitButton() {
        Application.Quit();
    }
}
