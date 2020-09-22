using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrieNode
{
    /**
     * 标识当前结点是否是一个“关键词”的最后一个结点
     * true 关键词的终结 false 继续
     */
    private bool _isEnd = false;

    /**
     * 用map来存储当前结点的所有子节点，非常的方便
     * key 下一个字符 value 对应的结点
     */
    private Dictionary<char, TrieNode> subNodes = new Dictionary<char, TrieNode>();

    /// <summary>
    /// 向指定位置添加结点树
    /// </summary>
    /// <param name="key"></param>
    /// <param name="node"></param>
    public void AddSubNode(char key, TrieNode node)
    {
        subNodes.Add(key, node);
    }

    /// <summary>
    /// 根据key获得相应的子节点
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public TrieNode GetSubNode(char key)
    {
        if (subNodes.ContainsKey(key))
            return subNodes[key];
        return null;
    }

    /// <summary>
    /// 判断是否是关键字的结尾
    /// </summary>
    public bool isKeyWordEnd
    {
        get { return _isEnd; }
        set
        {
            _isEnd = value;
        }
    }
}
