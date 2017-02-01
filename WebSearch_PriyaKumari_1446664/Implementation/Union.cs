using System;
using System.Collections.Generic;


namespace WebSearch_PriyaKumari_1446664.Implementation
{
    public class Union
    {
        /* Add all items from first list to finalList and 
        then check  from second list what documents are not present in final list and add them */
        public List<int> evaluateORQuery(List<int> firstList, List<int> secondList)
        {
            List<int> finalList = new List<int>();
            int i = 0;
            if (firstList.Count > 0 && secondList.Count == 0) {
                finalList.AddRange(firstList);
            }
            if (firstList.Count == 0 && secondList.Count > 0)
            {
                finalList.AddRange(secondList);

            }
            if (firstList.Count > 0 && secondList.Count > 0)
            {

                finalList.AddRange(firstList);
                while (i < secondList.Count)
                {
                    if (isFoundinFinalList(secondList[i], finalList))
                    {
                        finalList.Add(secondList[i]);
                    }
                    i++;
                }
            }

            return finalList;
        }

        /* check  from second list what documents are not present in final list and add them 
        returns false if item is present in the finalList otherwise returns true*/

        public bool isFoundinFinalList(int documentID , List<int> finalList) {
            int i = 0;
            while (i < finalList.Count) {
                if (finalList[i] == documentID)
                {
                    return false;
                }
                i++;
            }

            return true;
        }
    }
}
