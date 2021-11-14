using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // lets us load new scences


public class PageNavigation : MonoBehaviour
{
    /* 
     Load Scenes
     */ 
    public void StartUpPage()
    {
        SceneManager.LoadScene("StartUpPage");
    }

    public void HomePage()
    {
        SceneManager.LoadScene("HomePage");
    }

    public void Chapter1()
    {
        SceneManager.LoadScene("Chapter1");
    }

    public void Chapter2()
    {
        SceneManager.LoadScene("Chapter2");
    }

    public void Chapter3()
    {
        SceneManager.LoadScene("Chapter3");
    }

    public void Chapter4()
    {
        SceneManager.LoadScene("Chapter4");
    }

    public void Chapter5()
    {
        SceneManager.LoadScene("Chapter5");
    }

    public void Chapter6()
    {
        SceneManager.LoadScene("Chapter6");
    }

    public void Chapter7()
    {
        SceneManager.LoadScene("Chapter7");
    }

    // Load Apps

    public void AppsHomePage()
    {
        SceneManager.LoadScene("AppsHomePage");
    }

    public void FaceDetection()
    {
        SceneManager.LoadScene("FaceDetection");
    }

    public void DocumentScanner()
    {
        SceneManager.LoadScene("DocumentScanner");
    }

    public void ObjectDetection()
    {
        SceneManager.LoadScene("ObjectDetectionYoloV4");
        //SceneManager.LoadScene("ObjectDetectionMNSSD");
    }

    public void ColorPicker()
    {
        SceneManager.LoadScene("ColorPicker");
    }

    public void QrReader()
    {
        SceneManager.LoadScene("QrReader");
    }
}
