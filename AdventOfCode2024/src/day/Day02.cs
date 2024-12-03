using System.Diagnostics;
using System.Text.RegularExpressions;
using AdventOfCode2024;


public class Day02 : BaseDay
{

    public Day02(string[]? inputLines = null) : base(inputLines)
    {
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        string[] lines = InputLines;
        Debug.Assert(lines.Length != 0);
        await Task.Run(() =>
        {

            foreach (string line in lines)
            {
                string[] nums = Regex.Split(line, @"\s+");
                Debug.Assert(nums.Length >= 2);
                bool isIncreasing = int.Parse(nums[0]) < int.Parse(nums[1]);
                bool isValid = true;

                for (int i = 0; i < nums.Length - 1; i++)
                {
                    int left = int.Parse(nums[i]);
                    int right = int.Parse(nums[i + 1]);
                    int delta = Math.Abs(right - left);

                    if (delta == 0 || delta > 3)
                    {
                        isValid = false;
                        break;
                    }

                    if (isIncreasing)
                    {
                        if (left >= right)
                        {
                            isValid = false;
                            break;
                        }
                    }
                    else
                    {
                        if (right >= left)
                        {
                            isValid = false;
                            break;
                        }
                    }

                }

                if (isValid)
                {
                    part1 += 1;
                }
            }

        });

        return part1;
    }


    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        string[] lines = InputLines;
        Debug.Assert(lines.Length != 0);
        await Task.Run(() =>
        {

            foreach (string line in lines)
            {
                string[] nums = Regex.Split(line, @"\s+");
                Debug.Assert(nums.Length >= 2);
                int unSafeIndex = -1;
                bool isValid = IsReactorSafe(nums, out unSafeIndex);
                if (!isValid)
                {
                    //Try again removing the unsafe index AND it's neighbors
                    string[] nums1 = nums.Where((x, i) => i != unSafeIndex).ToArray();
                    string[] nums2 = nums.Where((x, i) => i != unSafeIndex + 1).ToArray();
                    string[] nums3 = nums.Where((x, i) => i != unSafeIndex - 1).ToArray();

                    bool isValid1 = IsReactorSafe(nums1, out unSafeIndex);
                    bool isValid2 = IsReactorSafe(nums2, out unSafeIndex);
                    bool isValid3 = IsReactorSafe(nums3, out unSafeIndex);

                    isValid = isValid1 || isValid2 || isValid3;
                }
                if (isValid)
                {
                    part2 += 1;
                }
            }
        });

        return part2;
    }

    private static bool IsReactorSafe(string[] nums, out int unSafeIndex)
    {
        bool isIncreasing = int.Parse(nums[0]) < int.Parse(nums[1]);
        bool isValid = true;
        unSafeIndex = -1;

        for (int i = 0; i < nums.Length - 1; i++)
        {
            int left = int.Parse(nums[i]);
            int right = int.Parse(nums[i + 1]);
            int delta = Math.Abs(right - left);

            if (delta == 0 || delta > 3)
            {
                isValid = false;
            }

            if (isIncreasing)
            {
                if (left >= right)
                {
                    isValid = false;
                }
            }
            else
            {
                if (right >= left)
                {
                    isValid = false;
                }
            }

            if (!isValid)
            {
                unSafeIndex = i;
                break;
            }

        }

        return isValid;
    }
}
