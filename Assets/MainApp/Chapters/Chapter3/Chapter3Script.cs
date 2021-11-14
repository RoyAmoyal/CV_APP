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

public class Chapter3Script : MonoBehaviour
{
    public RawImage imgDisplay;
    public RawImage imgDisplayResize;
    public RawImage imgDisplayCrop;  

    // Start is called before the first frame update
    void Start()
    {
        //Load Image
        Texture2D inputTexure = Resources.Load("test2") as Texture2D;

        /* Creating the object handler for opencv Image.(Mat short of Matrix)
         CvType.CV_8UC4 --> means every pixel is in the range of 0-255,
                            using 8 bits [0,2^8-1].
         U -> means unsigned, range of 0-255 while signed is [-127,127]
         C4 -> means 4 channels, RGBA (RED,GREEN,BLUE, A - Transparency) 
         */
        Mat img = new Mat(inputTexure.height, inputTexure.width, CvType.CV_8UC4);

        //convert Texture to Mat
        Utils.texture2DToMat(inputTexure, img);


        /*
            Main Code after handling the image
        */

        // Resize
        Size newSize = new Size(85, 85);
        Mat imgResize = new Mat();
        Imgproc.resize(img, imgResize, newSize, 0, 0);

        //Cropped
        OpenCVForUnity.CoreModule.Rect rectCrop = new OpenCVForUnity.CoreModule.Rect(250, 250, 300, 300);
        Mat imgCrop = new Mat(img, rectCrop);




        /*
           End of Main Code 
       */


        // convert the image back to Texture for the unity mobile app.
        Texture2D outputTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D resizeTexture = new Texture2D(imgResize.cols(), imgResize.rows(), TextureFormat.RGBA32, false);
        Texture2D cropTexture = new Texture2D(imgCrop.cols(), imgCrop.rows(), TextureFormat.RGBA32, false);
        



        //convert Mat back to Texture
        Utils.matToTexture2D(img, outputTexture);
        Utils.matToTexture2D(imgResize, resizeTexture);
        Utils.matToTexture2D(imgCrop, cropTexture,true,1);
        






        // Displaying our image. (we have to link the imgDisplay varaible in Unity in Manager)
        imgDisplay.texture = outputTexture;
        imgDisplayResize.texture = resizeTexture;
        imgDisplayCrop.texture = cropTexture;
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
