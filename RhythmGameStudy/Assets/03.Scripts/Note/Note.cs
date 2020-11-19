using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LineNumber
{
    Line_One = 64,
    Line_Two = 192,
    Line_Three = 320,
    Line_Four = 448,
}


public class Note : MonoBehaviour
{
    [SerializeField] Image noteImg;
    [SerializeField] RectTransform rectTransform;

    protected float pressTime;
    protected LineNumber lineNum;

    Vector2 startPos;
    Vector2 endPos;

    public float PressTime { get => pressTime; set => pressTime = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        rectTransform.anchoredPosition = startPos;
    }

    public void SetLine(LineNumber lineNumber, Vector2 _startPos, Vector2 _endPos)
    {
        lineNum = lineNumber;

        startPos = _startPos;
        endPos = _endPos;
    }    
}
