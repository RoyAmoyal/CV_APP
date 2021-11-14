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

public class Chapter4Script : MonoBehaviour
{
    public RawImage imgDisplay;
    // Start is called before the first frame update
    void Start()
    {

        // Create an empty Mat
        // Scalar is 3 numbers for the colors
        Mat img = new Mat(512, 512, CvType.CV_8UC3, new Scalar(255, 255, 255));
        // Draw Circle
        Imgproc.circle(img, new Point(256, 256), 150, new Scalar(255, 69, 0), Imgproc.FILLED);
        // Draw Rectangle
        Imgproc.rectangle(img, new Point(130, 226), new Point(382, 286), new Scalar(255, 255, 255), Imgproc.FILLED);
        // Draw Line
        Imgproc.line(img, new Point(130, 296), new Point(382, 296), new Scalar(255, 255, 255), 2);
        // Add Text
        Imgproc.putText(img, "Roy App", new Point(200, 262), Imgproc.FONT_HERSHEY_DUPLEX, 0.75, new Scalar(255, 69, 0), 2);




        // convert the image back to Texture for the unity mobile app.
        Texture2D outputTexure = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);

        //convert Mat back to Texture
        Utils.matToTexture2D(img, outputTexure);

        // Displaying our image. (we have to link the imgDisplay varaible in Unity in Manager)
        imgDisplay.texture = outputTexure;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
