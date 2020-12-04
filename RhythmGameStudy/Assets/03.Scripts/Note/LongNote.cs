using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : Note
{
    [SerializeField] float durationTime;


    Vector2 realStartPos;

    public float DurationTime { get => durationTime; set => durationTime = value; }

    // Start is called before the first frame update
    void OnEnable()
    {
        realStartPos = startPos;

        SetContent();
        StartCoroutine(DownLongNote());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetContent()
    {
        //847ms ~ 3923ms = 3076 
        //1초당 100fps 기준

        float diffence = (durationTime - pressTime) * 1000; //ms 차이
        float fps = diffence / 10f;                 //fps

        //시작 지점과 끝지점의 거리 2460 / 100 = 24.6

        //1fps 당 24.6f만큼 움직임
        float x = (24.6f * fps) / 100;  // 길이
        float size = 100 * x;

        Debug.Log(diffence); //3076
        Debug.Log(fps);      //92.28001


        rectTransform.sizeDelta = new Vector2(300f, size);
    }

    public IEnumerator DownLongNote()
    {

        while (lerpCount <= 100)
        {
            if (TicManager.Instance.Time - 2 >= durationTime)
            {
                ObjectPool.Instance.ReturnObject(gameObject);
                yield break;
            }

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

        startPos = endPos;
        endPos = new Vector2(endPos.x, endPos.y - 2460);

        lerpCount = 0;

        if (TicManager.Instance.Time - 2 <= durationTime)
            StartCoroutine(DownLongNote());

        //ObjectPool.Instance.ReturnObject(gameObject);
    }
}
