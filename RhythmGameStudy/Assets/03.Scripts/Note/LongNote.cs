using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : Note
{
    [SerializeField] float durationTime;



    public float DurationTime { get => durationTime; set => durationTime = value; }

    // Start is called before the first frame update
    void OnEnable()
    {
        SetContent();
        StartCoroutine(DownLongNote());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public void SetContent()
    {
        //872ms ~ 3923ms = 3052ms

        int diffence = (int)(durationTime * 1000) - (int)(pressTime * 1000);
        int count = diffence / 33;
        int other = diffence % 33;

        float tmp = 81.33f / 33f;

        Debug.Log(diffence); //3052
        Debug.Log(count); //92
        Debug.Log(other); //16

        //rectTransform.sizeDelta = new Vector2(300, 7013.333f);
    }

    public IEnumerator DownLongNote()
    {
        rectTransform.sizeDelta -= new Vector2(0f, (startPos.y - endPos.y) / 30) * 2;

        while (lerpCount < 30)
        {
            lerpCount++;
            rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, lerpCount / 30);

            if (TicManager.Instance.Time - 2 <= durationTime)
                rectTransform.sizeDelta += new Vector2(0f, (startPos.y - endPos.y) / 30);

            //Debug.Log("Lerp Count : " + lerpCount + " time : " + (time) + " y : " + rectTransform.anchoredPosition.y);

            //if (lerpCount == 15)
            //{
            //    Debug.Log(time);
            //    Time.timeScale = 0f;
            //}


            yield return new WaitForSeconds(takenTime / 30);
        }

        startPos = endPos;
        endPos = new Vector2(endPos.x, endPos.y - 2440f);

        lerpCount = 0;

        if (TicManager.Instance.Time - 2 <= durationTime)
            StartCoroutine(DownLongNote());

        //ObjectPool.Instance.ReturnObject(gameObject);
    }
}
