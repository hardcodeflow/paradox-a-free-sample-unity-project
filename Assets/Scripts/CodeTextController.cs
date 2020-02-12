using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeTextController : MonoBehaviour
{
    public TextMeshProUGUI codeText;
    public int mistakes=1024;
    string[] codeTextString;
    public int index;
    public float speedCoefficient;
    Vector3 lastpos;
    Vector3 vel;
    public float scaleY=0.1f;
    float i;
    void Start()
    {
        var c = Resources.Load<TextAsset>("allCode") as TextAsset;
        string s = c.text;
        s = s.Replace("int", "<color=#0000ff>int</color>");
        s = s.Replace("using", "<color=#0000ff>using</color>");
        s = s.Replace("void", "<color=#0000ff>void</color>");
        s = s.Replace("return", "<color=#0000ff>return</color>");
        s = s.Replace("public", "<color=#0000ff>public</color>");
        s = s.Replace("internal", "<color=#0000ff>internal</color>");
        s = s.Replace("string", "<color=#0000ff>string</color>");
        s = s.Replace("float", "<color=#0000ff>float</color>");
        s = s.Replace("class", "<color=#0000ff>class</color>");
        codeTextString = s.Split('\n');
        codeText.text =s;
        // codeText.text = c.text;
        // codeText.text = codeText.text.Replace(codeText.ToString(), "<color=#0000ff>" + codeText.text[10].ToString() + "</color>");
        //  codeText.text =codeText.text.Substring(0,10)+"<color=#FF0000>" + codeText.text.Substring(10, 10) + "</color>"+ codeText.text.Substring(20, codeText.text.Length-21);


    }
    internal void ResetCode()
    {
        codeText.text = "";
       index = 0;
    }
    internal void WriteCode() {
        index++;
        codeText.text += codeTextString[index]+"\n";
    }

    // Update is called once per frame
    void Update()
    {
        if (References.gameManager.gameState == GameState.InGame)
        {
            //    transform.position -= ((lastpos - Camera.main.transform.position+ new Vector3(0, index * 10, 0)) * speedCoefficient) ;
            //lastpos = Camera.main.transform.position+ new Vector3(0, index * 10, 0);
            this.transform.position = Vector3.SmoothDamp(this.transform.position, Camera.main.transform.position + new Vector3(0, index * scaleY, 0), ref vel, 0.4f);
            if (index < 20 && index >= 0)
            {
                scaleY = 1f;
            }
            if (index < 40 && index >= 20)
            {
                scaleY = 0.5f;
            }
            if (index < 70 && index >= 40)
            {
                scaleY = 0.4f;
            }
            if (index < 1000 && index >= 70)
            {
                scaleY = 0.1f;
            }
            if (index > 1000)
            {
                scaleY = 0.01f;
            }
        }
        else {
            i += 0.009f;
        
            this.transform.position = Vector3.SmoothDamp(this.transform.position, Camera.main.transform.position + new Vector3(0, 19+i, 0), ref vel, 0.4f);


        }
    }
}
