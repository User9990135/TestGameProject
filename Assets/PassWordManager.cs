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
                SystemStr = "�ȳ��ϼ���?\n ���� PassWordMaker �ý��� �Դϴ� \n�Է�â �ȿ� ������ �Է��Ͻø� ���� ��ɾ ����մϴ�.";
                break;
            case 1:
                SystemStr = "���� PassWordMaker �ý��� �Դϴ�.";
                break;
            case 2:
                SystemStr = "�Է�â �ȿ� ������ �Է��Ͻø� ���� ��ɾ ����մϴ�.";
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
