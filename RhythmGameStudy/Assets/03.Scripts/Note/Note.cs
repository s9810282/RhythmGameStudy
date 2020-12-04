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
    [SerializeField] protected Image noteImg;
    [SerializeField] protected RectTransform rectTransform;

    [SerializeField] protected RectTransform a;
    [SerializeField] protected RectTransform b;

    [SerializeField] protected float speed;
    [SerializeField] protected float takenTime = 1.0f;

    [SerializeField] protected float pressTime;
    [SerializeField] protected LineNumber lineNum;

    [SerializeField] protected float lerpCount = 0;

    protected Vector2 startPos;
    protected Vector2 endPos;

    protected float time = 0;

    public float PressTime { get => pressTime; set => pressTime = value; }



    // Start is called before the first frame update
    void OnEnable()
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
                noteImg.color = Color.red;
                break;
            case LineNumber.Line_Two:
                noteImg.color = Color.yellow;
                break;
            case LineNumber.Line_Three:
                noteImg.color = Color.blue;
                break;
            case LineNumber.Line_Four:
                noteImg.color = Color.green;
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


    public IEnumerator DownNote()
    {
        while (lerpCount < 100)
        {
            lerpCount++;
            rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, lerpCount / 100);


            //Debug.Log("Lerp Count : " + lerpCount + " time : " + (time) + " y : " + rectTransform.anchoredPosition.y);

            //if (lerpCount == 15)
            //{
            //    Debug.Log(time);
            //    Time.timeScale = 0f;
            //}

            yield return new WaitForSeconds(takenTime / 100);
        }

        ObjectPool.Instance.ReturnObject(gameObject);
    }
}
