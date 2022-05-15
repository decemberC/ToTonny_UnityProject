using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour
{
    public void jumpScan() {
        SceneManager.LoadScene(1);
    }
    public void jumpMenu()
    {
        ContentSearch.ClearSearch();
        SceneManager.LoadScene(0);
    }
}
