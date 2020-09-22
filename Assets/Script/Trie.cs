using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Trie
{
    //默认敏感词替换符
    private const String DEFAULT_REPLACEMENT = "敏感词";
    //根节点
    private TrieNode rootNode = new TrieNode();

    /// <summary>
    /// 判断是否是一个符号
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private bool IsSymbol(char c)
    {
        int ic = c;
        // 0x2E80-0x9FFF 东亚文字范围
        return !((c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')) && (ic < 0x2E80 || ic > 0x9FFF);
    }

    /// <summary>
    /// 根据输入的字符串列表构造字典树
    /// </summary>
    /// <param name="words"></param>
    public void AddWords(List<string> words)
    {
        if (words == null || words.Count == 0) return;
        for (int i = 0, count = words.Count; i < count; i++)
        {
            AddWord(words[i]);
        }
    }

    /// <summary>
    ///  根据输入的字符串构造字典树
    /// </summary>
    /// <param name="word"></param>
    public void AddWord(string word)
    {
        if (string.IsNullOrEmpty(word))
            return;
        TrieNode tempNode = rootNode;
        // 循环每个字节
        for (int i = 0; i < word.Length; ++i)
        {
            char c = word[i];
            // 过滤空格
            if (IsSymbol(c))
            {
                continue;
            }
            TrieNode node = tempNode.GetSubNode(c);

            if (node == null)
            { // 没初始化
                node = new TrieNode();
                tempNode.AddSubNode(c, node);
            }

            tempNode = node;

            if (i == word.Length - 1)
            {
                // 关键词结束， 设置结束标志
                tempNode.isKeyWordEnd = true;
            }
        }
    }

    /// <summary>
    /// 过滤敏感词
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string Filter(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }
        String replacement = DEFAULT_REPLACEMENT;
        StringBuilder result = new StringBuilder();

        TrieNode tempNode = rootNode;
        int begin = 0; // 回滚数
        int position = 0; // 当前比较的位置

        while (position < text.Length)
        {
            char c = text[position];
            // 空格直接跳过
            if (IsSymbol(c))
            {
                if (tempNode == rootNode)
                {
                    result.Append(c);
                    ++begin;
                }
                ++position;
                continue;
            }

            tempNode = tempNode.GetSubNode(c);

            // 当前位置的匹配结束
            if (tempNode == null)
            {
                // 以begin开始的字符串不存在敏感词
                result.Append(text[begin]);
                // 跳到下一个字符开始测试
                position = begin + 1;
                begin = position;
                // 回到树初始节点
                tempNode = rootNode;
            }
            else if (tempNode.isKeyWordEnd)
            {
                // 发现敏感词， 从begin到position的位置用replacement替换掉
                result.Append(replacement);
                position = position + 1;
                begin = position;
                tempNode = rootNode;
            }
            else
            {
                ++position;
            }
        }

        result.Append(text.Substring(begin));

        return result.ToString();
    }
}

