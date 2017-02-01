using System;
using System.Linq;
using System.Collections.Generic;
using WebSearch_PriyaKumari_1446664.Class;

namespace WebSearch_PriyaKumari_1446664.Implementation
{
    public class AndNot
    {
        public List<int> evaluateAndNOTQuery(String[] indexedDoc, String[] vocab, String qryType, String qryStr)
        {
           
            CreatePostingList cr = new CreatePostingList();
            List<PostingList> psList = new List<PostingList>();
            List<PostingList> retnPSList = new List<PostingList>();
            List<int> firstList = new List<int>();
            List<int> secondList = new List<int>();

            // Get complete posting list
            psList = cr.CreatPostingList(indexedDoc, vocab, qryType, qryStr);

            // Split query to get query terms
            List<String> stringsInQry = qryStr.Split(' ').ToList();
            stringsInQry = cr.CleanUpQry(stringsInQry);

            // Find inverted index of each query term. Except  PLIST query it will always have 2 terms
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

            // For ease of manipulation storing document List in separate items
            if (retnPSList.Count > 0)
            {
                if (retnPSList.Count == 1)
                {
                    if (retnPSList[0].wordIndex == 1)
                    {
                        firstList = retnPSList[0].documentList;
                    }
                    if (retnPSList[0].wordIndex == 2)
                    {
                        secondList = retnPSList[0].documentList;
                    }
                }

                if (retnPSList.Count == 2)
                {
                    firstList = retnPSList[0].documentList;
                    secondList = retnPSList[1].documentList;
                }
            }

            List<int> finalList = new List<int>();

            if (firstList.Count == 0) {
                return finalList;
            }
            else
            {

                    // First remove items common between both query term's document list
                    int j = 0;
                    while (j < secondList.Count)
                    {
                            firstList.Remove(secondList[j]);
                        j++;
                    }

                        if (secondList.Count > 0) {
                            // Create NOT List for the second query term
                            List<int> notList = Not(retnPSList[1], psList);
                            secondList = notList;
                        }

                        if (secondList.Count == 0) {
                            // Create NOT List for the second query term
                            List<int> notList = Not(new PostingList(), psList);
                            secondList = notList;
                        }
            


                    // Once both Lists are ready perform and operation on both query terms list
                    Intersection andLogic = new Intersection();
                    finalList = andLogic.evaluateAndQuery(firstList, secondList);
                    finalList = retnPSList[0].documentList;

                    return finalList;
            }
        }


        public List<int> Not(PostingList secondList, List<PostingList> pstList)
        {
            secondList.Word = "NOT" + secondList.Word;
            List<int> notList = new List<int>();

            if(secondList.documentList != null) {
                foreach (var listItem in pstList)
                {
                    foreach (var doc in listItem.documentList)
                    {
                        foreach (var docI in secondList.documentList)
                        {
                            if (doc != docI && !(notList.IndexOf(doc) > -1))
                            {
                                notList.Add(doc);
                            }
                        }
                    }
                }
            }


            if (secondList.documentList == null)
            {
                foreach (var listItem in pstList)
                {
                    foreach (var doc in listItem.documentList)
                    {
                            if (!(notList.IndexOf(doc) > -1))
                            {
                                notList.Add(doc);
                            }
                    }
                }
            }

            notList.Sort();
            return notList;
        }
    }
}
