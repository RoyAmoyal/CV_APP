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

public class Chapter7Script : MonoBehaviour
{
    public RawImage imgDisplay;
    public RawImage imgDisplayEdges;
    public RawImage imgDisplayContours;  

    // Start is called before the first frame update
    void Start()
    {
        //Load Image
        Texture2D inputTexure = Resources.Load("shapes") as Texture2D;
        Mat img = new Mat(inputTexure.height, inputTexure.width, CvType.CV_8UC4);
        Utils.texture2DToMat(inputTexure, img);

        Mat imgContours = img.clone();

        Mat imgGray = new Mat(inputTexure.height, inputTexure.width, CvType.CV_8UC1);
        Mat imgBlur = new Mat(inputTexure.height, inputTexure.width, CvType.CV_8UC1);
        Mat imgCanny = new Mat(inputTexure.height, inputTexure.width, CvType.CV_8UC1);
        Mat imgEdges = new Mat(inputTexure.height, inputTexure.width, CvType.CV_8UC1);



        /* Preproccessing of the image*/

        // Convert Image to grayscale
        Imgproc.cvtColor(img, imgGray, Imgproc.COLOR_RGB2GRAY);
        // Add blur
        Imgproc.GaussianBlur(imgGray, imgBlur, new Size(5, 5), 5, 0);
        // Find the Edge Detector
        Imgproc.Canny(imgBlur, imgCanny, 20, 70);
        // Increase/Decrease edge thickness
        // In python to create kernel we are using numpy, but here we will use getStructuringElement.
        Mat dilatElement = Imgproc.getStructuringElement(Imgproc.MORPH_CROSS, new Size(3, 3));
        Imgproc.dilate(imgCanny, imgEdges, dilatElement);

        /* End of Preproccessing of the image*/

        // Finding contours
        List<MatOfPoint> contours = new List<MatOfPoint>();
        Mat hierarchy = new Mat();
        // Imgproc.CHAIN_APPROX_SIMPLE returns only the simple points of the contours.
        // for example: for rectangle it will return only the 4 edges points and not all the points on the rectangle itself.
        // for Imgproc.CHAIN_APPROX_NONE we will get all the points.
        Imgproc.findContours(imgEdges, contours, hierarchy, Imgproc.RETR_EXTERNAL, Imgproc.CHAIN_APPROX_SIMPLE);


        // if we want to draw only specific contour we can write index 1 or 2. if we want to draw on all the contours
        // we will write -1.
        // In the new scalar we need transparency too, so 4th value is 255 for it.
        //Imgproc.drawContours(imgContours, contours, i, new Scalar(255, 0, 255, 255), 5);

        for (int i = 0; i < contours.Count; i++)
        {
            double area = Imgproc.contourArea(contours[i]);
            if (area > 2000)
            {
                MatOfPoint2f cntF = new MatOfPoint2f(contours[i].toArray());
                MatOfPoint2f approx = new MatOfPoint2f();
                double peri = Imgproc.arcLength(cntF, true);

                // this function finds how much corners the contour has.
                Imgproc.approxPolyDP(cntF, approx, 0.02 * peri, true);

                OpenCVForUnity.CoreModule.Rect bbox = Imgproc.boundingRect(approx);

                // bbox.tl() - top-left values,  bbox.br() - bottom-right values
                Imgproc.rectangle(imgContours, bbox.tl(), bbox.br(), new Scalar(0, 255, 0, 255), 2);
                Imgproc.drawContours(imgContours, contours, i, new Scalar(255, 0, 255, 255), 5);



                //if (approx.toArray().Length == 3)
                //{
                //    // if we want to draw only specific contour we can write index i.
                //    // In the new scalar we need transparency too, so 4th value is 255 for it.
                //    Imgproc.drawContours(imgContours, contours, i, new Scalar(255, 0, 255, 255), 5);
                //}
            }
        }


        //convert Texture to Mat
        Utils.texture2DToMat(inputTexure, img);


        // convert the image back to Texture for the unity mobile app.
        Texture2D outputTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D edgesTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D contoursTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        



        //convert Mat back to Texture
        Utils.matToTexture2D(img, outputTexture);
        Utils.matToTexture2D(imgEdges, edgesTexture);
        Utils.matToTexture2D(imgContours, contoursTexture);
    


        // Displaying our image. (we have to link the imgDisplay varaible in Unity in Manager)
        imgDisplay.texture = outputTexture;
        imgDisplayEdges.texture = edgesTexture;
        imgDisplayContours.texture = contoursTexture;
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
