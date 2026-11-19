using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScript : MonoBehaviour
{
    // How long one of the ~20 fade steps lasts when the scene opens.
    // 0.15 = about 3 seconds, 0.03 = a short fade of about half a second
    public float startFadeTime = 0.15f;

    Image img;
    Color tempColor;

    void Start()
    {
        img = GetComponent<Image>();
        tempColor = img.color;
        tempColor.a = 1f;
        img.color = tempColor;
        StartCoroutine(FadeIn(startFadeTime));
    }

    public IEnumerator FadeIn(float fadeTime)
    {
       for(float a=1f; a>= -0.05f; a -= 0.05f)
        {
            tempColor = img.color;
            tempColor.a = a;
            img.color = tempColor;
            yield return new WaitForSecondsRealtime(fadeTime); //!!!
        }
       img.raycastTarget = false;
    }

    public IEnumerator FadeOut(float fadeTime)
    {
        img.raycastTarget = true;
        for (float a = 0f; a <= 1.05f; a += 0.05f)
        {
            tempColor = img.color;
            tempColor.a = a;
            img.color = tempColor;
            yield return new WaitForSecondsRealtime(fadeTime); //!!!
        }
        img.raycastTarget = false;
    }
}
