using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    public Text display;
    public AudioSource type1;
    public AudioSource type2;
    public void ApplyTextToScreen(string text, float duration)
    {
        StartCoroutine(ApplyTextToScreen2(text, duration));
    }
    public IEnumerator ApplyTextToScreen2(string text, float duration)
    {
        foreach (char letter in text.ToCharArray())
        {
            display.text += letter;
            PlayRandom();
            yield return new WaitForSeconds(0.2f);
            if (text[text.Length - 1] == letter)
            {
                yield return new WaitForSeconds(duration);
                display.text = "";
            }
        }
    }
    private void PlayRandom()
    {
        int num = Random.Range(1,2);
        if (num == 1)
        {
            type1.Play();
        }
        else
        {
            type2.Play();
        }
    }
}
 