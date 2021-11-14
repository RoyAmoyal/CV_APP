using UnityEngine;


// lets us use raw images
using UnityEngine.UI;
// Basic imports requied to use the opencvforunity asset.
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
// requied for all the chapters expect chapter 1
using OpenCVForUnity.ImgprocModule;

public class Chapter2Script : MonoBehaviour
{
    public RawImage imgDisplay;
    public RawImage imgDisplayGray;
    public RawImage imgDisplayBlur;
    public RawImage imgDisplayCanny;
    public RawImage imgDisplayDil;
    public RawImage imgDisplayErode;

    // Start is called before the first frame update
    void Start()
    {
        //Load Image
        Texture2D inputTexure = Resources.Load("test2") as Texture2D;
        print(inputTexure);
        if (inputTexure == null)
        {
            print("omg");
        }
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

        // Only Channel 1 because it's Gray Image
        Mat imgGray = new Mat(inputTexure.height, inputTexure.width, CvType.CV_8UC1);
        Mat imgBlur = new Mat(inputTexure.height, inputTexure.width, CvType.CV_8UC1);
        Mat imgCanny = new Mat(inputTexure.height, inputTexure.width, CvType.CV_8UC1);
        Mat imgDil = new Mat(inputTexure.height, inputTexure.width, CvType.CV_8UC1);
        Mat imgErode = new Mat(inputTexure.height, inputTexure.width, CvType.CV_8UC1);

        // Convert Image to grayscale
        Imgproc.cvtColor(img, imgGray, Imgproc.COLOR_RGB2GRAY);
        // Add blur
        Imgproc.GaussianBlur(imgGray, imgBlur, new Size(9, 9), 15, 0);
        // Find the Edge Detector
        Imgproc.Canny(imgBlur, imgCanny, 20, 70);
        // Increase/Decrease edge thickness
        // In python to create kernel we are using numpy, but here we will use getStructuringElement.
        Mat erodeElement = Imgproc.getStructuringElement(Imgproc.MORPH_CROSS, new Size(9, 9));
        Imgproc.dilate(imgCanny, imgDil, erodeElement);
        Imgproc.erode(imgDil, imgErode, erodeElement);





        /*
           End of Main Code 
       */


        // convert the image back to Texture for the unity mobile app.
        Texture2D outputTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D grayTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D blurTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D cannyTexure = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D dilTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D erodeTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);






        //convert Mat back to Texture
        Utils.matToTexture2D(img, outputTexture);
        Utils.matToTexture2D(imgGray, grayTexture);
        Utils.matToTexture2D(imgBlur, blurTexture);
        Utils.matToTexture2D(imgCanny, cannyTexure);
        Utils.matToTexture2D(imgDil, dilTexture);
        Utils.matToTexture2D(imgErode, erodeTexture);






        // Displaying our image. (we have to link the imgDisplay varaible in Unity in Manager)
        imgDisplay.texture = outputTexture;
        imgDisplayGray.texture = grayTexture;
        imgDisplayBlur.texture = blurTexture;
        imgDisplayCanny.texture = cannyTexure;
        imgDisplayDil.texture = dilTexture;
        imgDisplayErode.texture = erodeTexture;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
