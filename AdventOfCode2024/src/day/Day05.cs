using System.Diagnostics;

using AdventOfCode2024;


public class Day05 : BaseDay
{

    Dictionary<int, List<int>> rules = new();
    List<List<int>> updates = new();
    
    public Day05(string[]? inputLines = null) : base(inputLines)
    {
        BuildRuleUpdates();
    }

    private void BuildRuleUpdates()
    {
        bool buildUpdates = false;
        foreach (string line in InputLines)
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

    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);

        await Task.Run(() =>
        {
            foreach (List<int> update in updates)
            {
                bool isValid = true;
                for (int i = update.Count - 1; i >= 0; i--)
                {
                    int element = update[i];
                    bool rulesValid = ValidateElement(element, update.Skip(i + 1).ToList());
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

    private Dictionary<int, Dictionary<List<int>, bool>> memoValidateElement = new Dictionary<int, Dictionary<List<int>, bool>>();

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

        if (!memoValidateElement.ContainsKey(element))
        {
            memoValidateElement[element] = new Dictionary<List<int>, bool>();
        }

        if (!memoValidateElement[element].ContainsKey(followingList))
        {
            bool result = followingList.All(x => rules[element].Contains(x));
            memoValidateElement[element][followingList] = result;
            return result;
        }
        else
        {
            return memoValidateElement[element][followingList];
        }
    }

    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {

            foreach (List<int> update in updates)
            {
                bool isValid = true;
                for (int i = update.Count - 1; i >= 0; i--)
                {
                    int element = update[i];
                    bool rulesValid = ValidateElement(element, update.Skip(i + 1).ToList());
                    if (!rulesValid)
                    {
                        isValid = false;
                        break;
                    }
                }

                if (!isValid)
                {
                    for (int i = update.Count - 1; i >= 0; i--)
                    {
                        int element = update[i];
                        List<int> validUpdate = new();
                        isValid = BuildValidUpdate(element, update.Where(x => x != element).ToList(), ref validUpdate);
                        if (isValid)
                        {
                            part2 += validUpdate[validUpdate.Count / 2];
                            break;
                        }
                    }
                }
            }
        });

        return part2;
    }

    private bool BuildValidUpdate(int element, List<int> remainingElements, ref List<int> validUpdate)
    {
        bool rulesValid = ValidateElement(element, validUpdate);
        if (!rulesValid)
        {
            return false;
        }
        
        validUpdate.Insert(0, element);
        if (remainingElements.Count == 0)
        {
            return true;
        }

        for (int i = remainingElements.Count - 1; i >= 0; i--)
        {
            int nextElement = remainingElements[i];
            List<int> nextValidUpdate = validUpdate.ToList();
            bool isValid = BuildValidUpdate(nextElement, remainingElements.Where(x => x != nextElement).ToList(), ref nextValidUpdate);
            if (isValid)
            {
                validUpdate = nextValidUpdate;
                return true;
            }
        }
      
        return false;
    }
}
