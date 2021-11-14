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

public class Chapter1Script : MonoBehaviour
{
    // NOTE: we have to write public before the RawImage to be able to show it in Unity app.
    public RawImage imgDisplay;


    // Start is called before the first frame update
    void Start()
    {
        //Load Image
        Texture2D inputTexure = Resources.Load("test2") as Texture2D;

        /* Creating the object handler for opencv Image. 
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
