using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToastScript : MonoBehaviour
{
    [SerializeField] private Text toastTxt;

    [SerializeField] private float duration = 2.0f;

    private void OnEnable()
    {
        GameEvents.Toast += showToast;
    }

    public void showToast(string text)
    {
        StartCoroutine(showToastC(text));
    }

    private IEnumerator showToastC(string text)
    {
        Color orginalColor = toastTxt.color;

        toastTxt.text = text;
        toastTxt.enabled = true;

        //Fade in
        yield return fadeInAndOut(toastTxt, true, 0.5f);

        //Wait for the duration
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //Fade out
        yield return fadeInAndOut(toastTxt, false, 0.5f);

        toastTxt.enabled = false;
        toastTxt.color = orginalColor;
    }

    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
    {
        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }

        Color currentColor = targetText.color;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
    }

    private void OnDisable()
    {
        GameEvents.Toast -= showToast;
    }
}
