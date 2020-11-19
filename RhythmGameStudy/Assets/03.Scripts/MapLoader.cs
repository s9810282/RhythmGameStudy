﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MapLoader : MonoBehaviour
{
    [SerializeField] MapData mapData;

    [SerializeField] Image backGround;
    [SerializeField] AudioSource audioSource;
    [SerializeField] VideoPlayer videoSource;

    [SerializeField] RectTransform[] noteStartPos;
    [SerializeField] RectTransform[] noteEndPos;

    [SerializeField] GameObject notePrefab;

    Dictionary<LineNumber, List<Note>> noteMap = new Dictionary<LineNumber, List<Note>>();


    int currentLine;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Debug.Log(Vector3.Lerp(new Vector3(2, 0, 0), new Vector3(10,10,10), 0.5f));

        LoadMap();

        yield return new WaitForEndOfFrame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<LineNumber, List<Note>> LoadMap(int keyCount = 4)
    {
        audioSource.clip = mapData.Bgm;

        if (!mapData.VideoClip)
            videoSource.clip = mapData.VideoClip;


        TextAsset textData = Resources.Load(mapData.MapName) as TextAsset;
        string strData = textData.text;
        string[] lineData = strData.Split('\n');

        currentLine = 0;

        List<Note>[] lines = new List<Note>[keyCount];

        string[] data = new string[6];
        string duration;

        while (currentLine < lineData.Length)
        {
            data = lineData[currentLine].Split(','); //라인을 읽어와서 저장
            duration = data[5].Split(':')[0]; //롱노트가 끊나는 시간

            GameObject noteObj = Instantiate(notePrefab); //노트 생성
            Note note;

            if (duration != null) //스크맆트 할당
                note = noteObj.AddComponent<Note>();
            else
            {
                note = noteObj.AddComponent<LongNote>();
                note.GetComponent<LongNote>().DurationTime = float.Parse(duration) / 1000;
            }

            note.PressTime = float.Parse(data[0]) / 1000;


            switch ((LineNumber)int.Parse(data[0])) //위치 세팅
            {
                case LineNumber.Line_One:
                    note.SetLine(LineNumber.Line_One, noteStartPos[0].anchoredPosition, noteEndPos[0].anchoredPosition);
                    lines[0].Add(note);
                    break;
                case LineNumber.Line_Two:
                    note.SetLine(LineNumber.Line_Two, noteStartPos[1].anchoredPosition, noteEndPos[1].anchoredPosition);
                    lines[1].Add(note);
                    break;
                case LineNumber.Line_Three:
                    note.SetLine(LineNumber.Line_Three, noteStartPos[2].anchoredPosition, noteEndPos[2].anchoredPosition);
                    lines[2].Add(note);
                    break;
                case LineNumber.Line_Four:
                    note.SetLine(LineNumber.Line_Four, noteStartPos[3].anchoredPosition, noteEndPos[3].anchoredPosition);
                    lines[3].Add(note);
                    break;
            }
        }

        noteMap.Add(LineNumber.Line_One, lines[0]); //리스트에 추가
        noteMap.Add(LineNumber.Line_Two, lines[1]);
        noteMap.Add(LineNumber.Line_Three, lines[2]);
        noteMap.Add(LineNumber.Line_Four, lines[3]);

        return noteMap;
    }
}