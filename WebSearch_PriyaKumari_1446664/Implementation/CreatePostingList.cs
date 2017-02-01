using System;
using System.Linq;
using System.Collections.Generic;
using WebSearch_PriyaKumari_1446664.Class;

namespace WebSearch_PriyaKumari_1446664.Implementation
{
    public class CreatePostingList
    {
        public List<PostingList> CreatPostingList(String[] indexedDoc, String[] vocab, String qryType, String qryStr)
        {
            List<Vocab> vb = CreatVocab(vocab);
            List<PostingList> psList = new List<PostingList>();
            List<PostingList> retnPSList = new List<PostingList>();
            List<String> stringsInQry = qryStr.Split(' ').ToList();
            stringsInQry = CleanUpQry(stringsInQry);

            char[] delimiters = new char[] { ',', '[', ']' };

            for (int i = 0; i < indexedDoc.Length; i++)
            {
                String[] wordsListPerLine = indexedDoc[i].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in wordsListPerLine)
                    if (psList.Count == 0)
                    {
                        PostingList ps = new PostingList();
                        ps.PositioninVocab = Int32.Parse(str);
                        ps.Word = vb.Where(x => x.position == Int32.Parse(str)).Select(x => x.word).First();
                        ps.documentList = new List<int>();
                        ps.documentList.Add(i + 1);
                        psList.Add(ps);
                    }
                    else
                    {
                        var found = psList.Where(x => x.PositioninVocab == Int32.Parse(str)).ToList();
                        if (found.Count() == 0)
                        {
                            PostingList ps = new PostingList();
                            ps.PositioninVocab = Int32.Parse(str);
                            ps.Word = vb.Where(x => x.position == Int32.Parse(str)).Select(x => x.word).First();
                            ps.documentList = new List<int>();
                            ps.documentList.Add(i + 1);
                            psList.Add(ps);
                        }
                        else
                        {
                            var baseqry = found.FirstOrDefault().documentList;
                            if (baseqry.Where(x => x == i + 1).ToList().Count() == 0) { baseqry.Add(i + 1); }

                        }
                    }

            }

            if (qryType.ToUpper().Equals("AND_NOT"))
            {

                return psList;
            }

            int wordIndex = 0;
            foreach (String qry in stringsInQry)
                {
                    wordIndex++;
                    foreach (var c in psList)
                    {
                        if (c.Word.TrimStart().TrimEnd().Equals(qry.TrimStart().TrimEnd()))
                        {
                            c.wordIndex = wordIndex;
                            retnPSList.Add(c);
                        }
                    }
                }

            return retnPSList;
      }

        public List<Vocab> CreatVocab(String[] vocab)
        {
            List<Vocab> vocabList = new List<Vocab>();
            char[] delimiters = new char[] { '=' };
            foreach (string i in vocab)
            {
                String[] wordsPerLine = i.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                Vocab v = new Vocab();
                v.position = Int32.Parse(wordsPerLine[0]);
                v.word = wordsPerLine[1];
                vocabList.Add(v);
            }
            return vocabList;
        }

        public List<String> CleanUpQry(List<String> qryStringList) {
            qryStringList.ToList();
            List<String> constants = new List<string>(new string[] { "and", "or", "not","(" ,")"});
                foreach (String c in constants) {
                    if (qryStringList.IndexOf(c) > 0) {
                        qryStringList.Remove(c);
                    }
                }
            return qryStringList;
        }

    }
}
