using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager
{
    Dictionary<int, string[]> talk;

    private void Awake()
    {
        talk = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talk.Add((int)Define.NPC.Tutorial, new string[] {"¿∏æ—!!", "ªÏ∑¡¡‡!!"});
    }

    public string GetTalk(int define, int talkindex)
    {
        return talk[define][talkindex];
    }
}
