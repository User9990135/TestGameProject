using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PassWordManager : MonoBehaviour
{
    public Text Systemtext;
    public string SystemStr;
    public int SystemNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        SystemNum = 0;
        Systemtextprint(SystemNum);
    }

    public void Systemtextprint(int Snum)
    {
        SystemMessage(Snum);
        //Systemtext.text = SystemStr;
        StartCoroutine(Typing(SystemStr));

    }

    public void SystemMessage(int Num)
    {
        switch (Num)
        {
            case 0:
                SystemStr = "안녕하세요?\n 저는 PassWordMaker 시스템 입니다 \n입력창 안에 도움을 입력하시면 관련 명령어를 출력합니다.";
                break;
            case 1:
                SystemStr = "저는 PassWordMaker 시스템 입니다.";
                break;
            case 2:
                SystemStr = "입력창 안에 도움을 입력하시면 관련 명령어를 출력합니다.";
                break;
        }

    }

    IEnumerator Typing(string text)
    {
        foreach (char letter in text.ToCharArray())
        {
            Systemtext.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
