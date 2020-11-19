using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


[CreateAssetMenu(fileName = "new MapData", menuName = "MapData", order = 2)]
public class MapData : ScriptableObject
{
    [SerializeField] string mapName; //텍스트 파일만 받아온다 이름을 통해 Resoruce.Load

    [SerializeField] Sprite backGroundImg;
    [SerializeField] VideoClip videoClip;
    [SerializeField] AudioClip bgm;

    public string MapName { get => mapName; set => mapName = value; }
    public Sprite BackGroundImg { get => backGroundImg; set => backGroundImg = value; }
    public VideoClip VideoClip { get => videoClip; set => videoClip = value; }
    public AudioClip Bgm { get => bgm; set => bgm = value; }
}
