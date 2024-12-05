using System.Diagnostics;

using AdventOfCode2024;


public class Day05 : BaseDay
{

    Dictionary<int, List<int>> rules = new();
    List<List<int>> updates = new();
    
    public Day05(string[]? inputLines = null) : base(inputLines)
    {
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        bool buildUpdates = false;
       
        
        foreach(string line in InputLines)
        {
            if (string.IsNullOrEmpty(line))
            {
                buildUpdates = true;
                continue;
            }

            if (!buildUpdates)
            {
                string[] nums = line.Split('|');
                Debug.Assert(nums.Length == 2);
                int left = int.Parse(nums[0].Trim());
                int right = int.Parse(nums[1].Trim());
                if (!rules.ContainsKey(left))
                {
                    rules.Add(left, new List<int>());
                }

                rules[left].Add(right);
                continue;
            }

            List<int> update = line.Split(',').Select(int.Parse).ToList();
            updates.Add(update);
        }


        await Task.Run(() => {
            //For each update
            //step through from the last element to the first (You may skip the last element since it is always valid)
            //For each element, check the rules that for the dictionary key of element, all the already seen elements are in its list
            //Continue until you have validated the entire update. If you get to the end, it is valid

            foreach (List<int> update in updates)
            {
                bool isValid = true;
                for (int i = update.Count - 1; i >= 0; i--)
                {
                    int element = update[i];
                    bool rulesValid = ValidateElement(element, update.Skip(i+1).ToList());
                    if (!rulesValid)
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    //Take the middle element in the update
                    part1 += update[update.Count / 2];
                }
            }

        });
        
        return part1;
    }

    private bool ValidateElement(int element, List<int> followingList)
    {
        if (followingList.Count == 0)
        {
            return true;
        }

        if (!rules.ContainsKey(element))
        {
            return false;
        }

        //TODO: Missing page numbers in the rules should be ignored. Both here and in the following list.
        return followingList.All(x => rules[element].Contains(x));
    }

    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {

        });

        return part2;
    }
}
