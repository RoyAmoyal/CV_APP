using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// lets us use raw images
using UnityEngine.UI; 
// Basic imports requied to use the opencvforunity asset.
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
// requied for all the chapters expect chapter 1
using OpenCVForUnity.ImgprocModule;

public class Chapter6Script : MonoBehaviour
{
    public RawImage imgDisplay;
    public RawImage imgDisplayMask;

    // Start is called before the first frame update
    void Start()
    {
        //Load Image
        Texture2D inputTexure = Resources.Load("lambo") as Texture2D;
        Mat img = new Mat(inputTexure.height, inputTexure.width, CvType.CV_8UC4);
        Utils.texture2DToMat(inputTexure, img);



        // 
        Mat imgHSV = new Mat();
        Mat mask = new Mat();

        Imgproc.cvtColor(img, imgHSV, Imgproc.COLOR_RGB2HSV);
        // Core is part of the opencvforunity.
        Core.inRange(imgHSV, new Scalar(0,110,153), new Scalar(19,240,255), mask);


        // convert the image back to Texture for the unity mobile app.
        Texture2D outputTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D maskTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);


      
        //convert Mat back to Texture
        Utils.matToTexture2D(img, outputTexture);
        Utils.matToTexture2D(mask, maskTexture);
       


        // Displaying our image. (we have to link the imgDisplay varaible in Unity in Manager)
        imgDisplay.texture = outputTexture;
        imgDisplayMask.texture = maskTexture;
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
