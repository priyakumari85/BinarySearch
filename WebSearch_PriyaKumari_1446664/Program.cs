using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using WebSearch_PriyaKumari_1446664.Class;
using WebSearch_PriyaKumari_1446664.Implementation;

namespace WebSearch_PriyaKumari_1446664
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Variable declaration
            // Variable declaration
            String[] indexedDoc = File.ReadAllLines(Path.GetFullPath("C:\\Users\\priyaharish\\Google Drive\\UH\\Sem 1 Paper\\Data Mining\\Assignments\\Assignment2\\WebSearch_PriyaKumari_1446664\\docs.txt"));
            String[] vocab = File.ReadAllLines(Path.GetFullPath("C:\\Users\\priyaharish\\Google Drive\\UH\\Sem 1 Paper\\Data Mining\\Assignments\\Assignment2\\WebSearch_PriyaKumari_1446664\\vocab_map.txt"));
            List<int> finalList = new List<int>();
            CreatePostingList cr = new CreatePostingList();
            List<int> firstList = new List<int>();
            List<int> secondList = new List<int>(); 
            #endregion

            #region Accept Argument
            //Accepting Command Line Argument
            Console.WriteLine("Please enter query type");
            String qryType = Console.ReadLine();
            Console.WriteLine("Please enter query");
            String qry = Console.ReadLine();
            Console.WriteLine("Processing");
            #endregion

            #region Creating Posting List
            // Creating Posting List
            List<PostingList> returnedPostingList = new List<PostingList>();
            if (qryType.ToUpper() != "AND_NOT")
            {
                returnedPostingList = cr.CreatPostingList(indexedDoc, vocab, qryType, qry);
                if (returnedPostingList.Count > 0)
                {
                    if (returnedPostingList.Count == 1) {
                        if (returnedPostingList[0].wordIndex == 1)
                        {
                            firstList = returnedPostingList[0].documentList;
                        }
                        if (returnedPostingList[0].wordIndex == 2)
                        {
                            secondList = returnedPostingList[0].documentList;
                        }
                    }

                    if (returnedPostingList.Count == 2)
                    {
                        firstList = returnedPostingList[0].documentList;
                        secondList = returnedPostingList[1].documentList;
                    }
                }
            } 
            #endregion

            #region PLIST
            if (qryType.ToUpper() == "PLIST")
            {
                if(returnedPostingList.Count > 0) {
                    finalList = returnedPostingList[0].documentList;
                }
            } 
            #endregion

            #region AND
            if (qryType.ToUpper() == "AND")
            {
                Intersection andLogic = new Intersection();
                finalList = andLogic.evaluateAndQuery(firstList, secondList);
            } 
            #endregion

            #region OR
            if (qryType.ToUpper() == "OR")
            {
                Union orLogic = new Union();
                finalList = orLogic.evaluateORQuery(firstList, secondList);
            }
            #endregion

            #region AND_NOT
            if (qryType.ToUpper() == "AND_NOT")
            {
                AndNot orLogic = new AndNot();
                finalList = orLogic.evaluateAndNOTQuery(indexedDoc, vocab, qryType, qry);
            }
            #endregion

            #region Print
            finalList.Sort();
            StringBuilder final = new StringBuilder();
            final.Append(qry).Append(" ->").Append(" [");
            foreach (int i in finalList)
            {
                final.Append(i).Append(", ");
            }
            if(finalList.Count > 0) {
                final.Remove(final.Length - 1, 1);
                final.Remove(final.Length - 1, 1);
            }
            final.Append("]");
            Console.WriteLine("Please enter output path");
            String outPath = Console.ReadLine();
            System.IO.StreamWriter file = new System.IO.StreamWriter(outPath.ToString());
            file.WriteLine(final);
            file.Dispose();

            #endregion

        } 

    }
}


