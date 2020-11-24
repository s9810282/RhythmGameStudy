using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteCreator : MonoBehaviour
{
    [SerializeField] MapLoader mapLoader;

    [SerializeField] List<Note> noteMap_1;
    [SerializeField] List<Note> noteMap_2;
    [SerializeField] List<Note> noteMap_3;
    [SerializeField] List<Note> noteMap_4;

    [SerializeField] float time;

    [SerializeField] float spaceTime = 2f;
    [SerializeField] float takenTime = 1f;

    [SerializeField] AudioSource audioSource;

    [SerializeField] Text text;

    Dictionary<LineNumber, List<Note>> noteMap = new Dictionary<LineNumber, List<Note>>();

    bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        isStart = false;

        NoteRead();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        //if (!isStart)
        //    return;

        text.text = time.ToString();

        if (spaceTime - (takenTime / 2) + noteMap_1[0].PressTime < time)
        {
            Debug.Log(time);
            noteMap_1[0].gameObject.SetActive(true);
            noteMap_1.RemoveAt(0);
        }
        if (spaceTime - (takenTime / 2) + noteMap_2[0].PressTime < time)
        {
            noteMap_2[0].gameObject.SetActive(true);
            noteMap_2.RemoveAt(0);
        }
        if (spaceTime - (takenTime / 2) + noteMap_3[0].PressTime < time)
        {
            noteMap_3[0].gameObject.SetActive(true);
            noteMap_3.RemoveAt(0);
        }
        if (spaceTime - (takenTime / 2) + noteMap_4[0].PressTime < time)
        {
            noteMap_4[0].gameObject.SetActive(true);
            noteMap_4.RemoveAt(0);
        }
    }

    public void NoteRead()
    {
        noteMap = mapLoader.LoadMap(4);

        noteMap_1 = noteMap[LineNumber.Line_One];
        noteMap_2 = noteMap[LineNumber.Line_Two];
        noteMap_3 = noteMap[LineNumber.Line_Three];
        noteMap_4 = noteMap[LineNumber.Line_Four];

        StartCoroutine(SpaceTime());
    }

    IEnumerator SpaceTime()
    {
        yield return new WaitForSeconds(2f);

        isStart = true;
        audioSource.Play();
    }
}
