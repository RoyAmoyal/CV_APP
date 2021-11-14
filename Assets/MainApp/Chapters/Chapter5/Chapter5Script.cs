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

public class Chapter5Script : MonoBehaviour
{
    public RawImage imgDisplay;
    public RawImage imgDisplayWarp;

    float w = 500, h = 700;


    // Start is called before the first frame update
    void Start()
    {
        //Load Image
        Texture2D inputTexure = Resources.Load("cards") as Texture2D;

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

        // 
        Mat imgWarp = img.clone();

        //creating matrix 4x1, when each cell has 2 values (C2) and this values are floating point (32F).
        Mat src = new Mat(4, 1, CvType.CV_32FC2);
        Mat dst = new Mat(4, 1, CvType.CV_32FC2);


        // Values of source and destination points
        src.put(0, 0, 529.0,142.0, 771.0,190.0, 405.0,395.0, 674.0,457.0);
        dst.put(0, 0, 0.0, 0.0, w, 0, 0, h, w, h);

        //Finding the transformation matrix
        Mat matrix = Imgproc.getPerspectiveTransform(src, dst);
        //Warp the Image
        Imgproc.warpPerspective(img, imgWarp,matrix,new Size((int)w,(int)h));

        /*
           End of Main Code 
       */


        // convert the image back to Texture for the unity mobile app.
        Texture2D outputTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D warpTexture = new Texture2D((int)w, (int)h, TextureFormat.RGBA32, false);


        // Rect Transfrom is unity component in rawimage.
        // Change Raw Image size to match the warp image size.
        imgDisplayWarp.GetComponent<RectTransform>().sizeDelta = new Vector2((int)w, (int)h);

        //convert Mat back to Texture
        Utils.matToTexture2D(img, outputTexture);
        Utils.matToTexture2D(imgWarp, warpTexture);
        






        // Displaying our image. (we have to link the imgDisplay varaible in Unity in Manager)
        imgDisplay.texture = outputTexture;
        imgDisplayWarp.texture = warpTexture;
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
