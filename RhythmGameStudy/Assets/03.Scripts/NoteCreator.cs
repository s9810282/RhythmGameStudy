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

    [SerializeField] int maxNoteCount_1;
    [SerializeField] int maxNoteCount_2;
    [SerializeField] int maxNoteCount_3;
    [SerializeField] int maxNoteCount_4;

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
        text.text = time.ToString("N4");
    }

    public void NoteRead()
    {
        noteMap = mapLoader.LoadMap(4);

        noteMap_1 = noteMap[LineNumber.Line_One];
        noteMap_2 = noteMap[LineNumber.Line_Two];
        noteMap_3 = noteMap[LineNumber.Line_Three];
        noteMap_4 = noteMap[LineNumber.Line_Four];

        maxNoteCount_1 = noteMap_1.Count;
        maxNoteCount_2 = noteMap_2.Count;
        maxNoteCount_3 = noteMap_3.Count;
        maxNoteCount_4 = noteMap_4.Count;


        StartCoroutine(NoteCreate());
    }
    IEnumerator SpaceTime()
    {
        yield return new WaitForSeconds(2f);
    }
    IEnumerator NoteCreate()
    {
        yield return StartCoroutine(SpaceTime());

        isStart = true;
        audioSource.Play();

        while (isStart)
        {
            yield return null;

            if (maxNoteCount_1 > 0)
            {
                if (spaceTime - (takenTime / 2) + noteMap_1[0].PressTime < time)
                {
                    noteMap_1[0].gameObject.SetActive(true);
                    noteMap_1.RemoveAt(0);

                    maxNoteCount_1--;
                }
            }

            if (maxNoteCount_2 > 0)
            {
                if (spaceTime - (takenTime / 2) + noteMap_2[0].PressTime < time)
                {
                    if (noteMap_2.Count <= 0)
                        break;

                    noteMap_2[0].gameObject.SetActive(true);
                    noteMap_2.RemoveAt(0);

                    maxNoteCount_2--;
                }
            }

            if (maxNoteCount_3 > 0)
            {
                if (spaceTime - (takenTime / 2) + noteMap_3[0].PressTime < time)
                {
                    if (noteMap_3.Count <= 0)
                        yield break;

                    noteMap_3[0].gameObject.SetActive(true);
                    noteMap_3.RemoveAt(0);

                    maxNoteCount_3--;
                }
            }

            if (maxNoteCount_4 > 0)
            {
                if (spaceTime - (takenTime / 2) + noteMap_4[0].PressTime < time)
                {
                    if (noteMap_4.Count <= 0)
                        yield break;

                    noteMap_4[0].gameObject.SetActive(true);
                    noteMap_4.RemoveAt(0);

                    maxNoteCount_4--;
                }
            }

            if (maxNoteCount_1 + maxNoteCount_2 + maxNoteCount_3 + maxNoteCount_4 <= 0)
            {
                isStart = false;
            }
        }
    }
}
