using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace App.Pages.FlashCard
{
public class Trie
{
    class Node
    {
        public bool isWord = false;
        public List<Node> children = new List<Node>(new Node[26]);
    }

    Node root, curr;
    List<string> resultBuffer;

    private void DFSWithPrefix(Node curr, string word)
    {
        if (resultBuffer.Count == 3)
            return;

        if (curr.isWord)
            resultBuffer.Add(word);

        for(char c = 'a'; c <= 'z'; c++)
        {
            if(curr.children[c-'a'] != null)
            {
                DFSWithPrefix(curr.children[c - 'a'], word + c);
            }
        }
    }

    public Trie()
    {
        root = new Node();
    }

    public void insert(string s)
    {
        curr = root;
        foreach(char c in s.ToCharArray())
        {
            if(curr.children[c - 'a'] == null)
            {
                curr.children[c - 'a'] = new Node();
            }

            curr = curr.children[c - 'a'];
        }

        curr.isWord = true;
    }

    public List<string> GetWordsStartingWith(string prefix)
    {
        curr = root;
        resultBuffer = new List<string>();

        foreach(char c in prefix.ToCharArray())
        {
            if (curr.children[c - 'a'] == null)
                return resultBuffer;

            curr = curr.children[c - 'a'];
        }
        DFSWithPrefix(curr, prefix);
        return resultBuffer;
    }
}

}
