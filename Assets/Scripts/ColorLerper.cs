using UnityEngine;
using UnityEngine.UI;

public class ColorLerper : MonoBehaviour
{
    //public GameObject mineDetectorViewer;
    public Image imageMineDetector;
    public float lerpingTime;
    private Color colorA;
    private Color colorB;

    public float currentTime;

    private void Start()
    {
        currentTime = 5;
        lerpingTime = 5;
    }

    public void Initializer(Color pColor1, Color pColor2)
    {
        colorA = pColor1;
        colorB = pColor2;
    }

    public void UpdateLerp(float pCurrentTime)
    {
        currentTime = pCurrentTime;
        float perc = currentTime / lerpingTime;

        imageMineDetector.color = Color.Lerp(colorA, colorB, perc);
    }
}