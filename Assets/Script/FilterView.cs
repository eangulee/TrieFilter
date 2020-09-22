using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterView : MonoBehaviour
{
    public InputField inputField;
    public Text resultTxt;
    private List<string> words;
    private Trie trie;

    // Start is called before the first frame update
    void Start()
    {
        trie = new Trie();
        words = new List<string>() { "shit", "傻逼", "笨蛋" };
        trie.AddWords(words);
    }

    #region Event Handler
    public void OnFilterBtnClickHandler()
    {
        string text = trie.Filter(inputField.text);
        Debug.Log("过滤结果：" + text);
        resultTxt.text = text;
    }
    #endregion
}
