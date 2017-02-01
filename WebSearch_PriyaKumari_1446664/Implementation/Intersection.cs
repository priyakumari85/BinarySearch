using System;
using System.Collections.Generic;

namespace WebSearch_PriyaKumari_1446664.Implementation
{
    public class Intersection
    {
        public List<int> evaluateAndQuery(List<int> firstList, List<int> secondList)
        {
            List<int> finalList = new List<int>();
            int i = 0;
            int j = 0;
            firstList.Sort();
            secondList.Sort();
            while (i < firstList.Count && j < secondList.Count) {
                if (firstList[i] > secondList[j])
                {
                    j++;
                }
                else if (secondList[j] > firstList[i])
                {
                    i++;
                }
                else
                {
                    finalList.Add(firstList[i]);
                    i++;
                    j++;
                }
            }
            return finalList;
        }
    }
}
