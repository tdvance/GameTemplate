using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Crawler : MonoBehaviour
{
    public Text text;
    public int maxLinesDisplayed = 10;
    public int numPaddingLines = 3;
    public float secondsPerLine = 2f;
    int top_line = 0;
    float timeSinceFill = 0f;

    public string[] story =
    {
        "This is a line of the text 1.",
        "This is a line of the text 2.",
        "This is a line of the text 3.",
        "This is a line of the text 4.",
        "This is a line of the text 5.",
        "This is a line of the text 6.",
        "This is a line of the text 7.",
        "This is a line of the text 8.",
        "This is a line of the text 9.",
        "This is a line of the text 10.",
        "This is a line of the text 11.",
        "This is a line of the text 12.",
        "This is a line of the text 13.",
        "This is a line of the text 14.",
        "This is a line of the text 15.",
        "This is a line of the text 16.",
        "This is a line of the text 17.",
        "This is a line of the text 18.",
        "This is a line of the text 19.",
        "This is a line of the text 20."
    };

    // Use this for initialization
    void Start()
    {
        FillText();
    }

    void FillText()
    {
        text.text = "";
        int current_line = top_line;
        for(int i=0; i<maxLinesDisplayed; i++)
        {
            if (current_line >= story.Length)
            {
                text.text += "\n";
            }
            else
            {
                text.text += story[current_line] + "\n";
            }
            current_line++;
            if(current_line >= story.Length + numPaddingLines)
            {
                current_line = 0;
            }
        }
        top_line++;
        if (top_line >= story.Length + numPaddingLines)
        {
            top_line = 0;
        }
        timeSinceFill = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (story.Length > maxLinesDisplayed)// don't craw if story fits on screen.
        {
            timeSinceFill += Time.deltaTime;
            if (timeSinceFill >= secondsPerLine)
            {
                FillText();
            }
        }
    }
}
