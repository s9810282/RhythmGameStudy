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

    [SerializeField] RectTransform a;
    [SerializeField] RectTransform b;

    [SerializeField] float speed;
    [SerializeField] float takenTime = 1.0f;

    [SerializeField] protected float pressTime;
    protected LineNumber lineNum;

    Vector2 startPos;
    Vector2 endPos;

    [SerializeField] float lerpCount = 0;

    public float PressTime { get => pressTime; set => pressTime = value; }

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DownNote());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public void Init()
    {
        noteImg = gameObject.GetComponent<Image>();

        rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = startPos;

        switch (lineNum)
        {
            case LineNumber.Line_One:
                noteImg.color = Color.black;
                break;
            case LineNumber.Line_Two:
                noteImg.color = Color.white;
                break;
            case LineNumber.Line_Three:
                noteImg.color = Color.black;
                break;
            case LineNumber.Line_Four:
                noteImg.color = Color.white;
                break;
        }
    }
    public void SetLine(LineNumber lineNumber, Vector2 _startPos, Vector2 _endPos)
    {
        lineNum = lineNumber;

        startPos = _startPos;
        endPos = _endPos;

        Init();
    }


    IEnumerator DownNote()
    {
        while (lerpCount < 30)
        {
            lerpCount++;
            rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, lerpCount / 30);


            //Debug.Log("Lerp Count : " + lerpCount + " time : " + (time) + " y : " + rectTransform.anchoredPosition.y);

            //if (lerpCount == 15)
            //{
            //    Debug.Log(time);
            //    Time.timeScale = 0f;
            //}

            yield return new WaitForSeconds(takenTime / 30);
        }

        ObjectPool.Instance.ReturnObject(gameObject);
    }
}
