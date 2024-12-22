using System.Diagnostics;

using AdventOfCode2024;


public class Day22 : BaseDay
{
    struct BananaPrice
    {
        public int Price;
        public int Delta;
    }

    List<int> InitialSecretNumbers = new List<int>();
    List<int> FinalSecretNumbers = new List<int>();

    List<List<BananaPrice>> BuyersPrices = new List<List<BananaPrice>>();

    public Day22(string[]? inputLines = null) : base(inputLines)
    {
        foreach (string line in InputLines)
        {
            int number = int.Parse(line);
            Debug.Assert(number > 0);
            InitialSecretNumbers.Add(number);
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        long part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {
            foreach(int num in InitialSecretNumbers)
            {
                int finalSecret = CalculateSecretTo(num, 2000);
                FinalSecretNumbers.Add(finalSecret);
                Console.WriteLine($"{num}: {finalSecret}");
                part1 += finalSecret;
            }
        });
        
        Console.WriteLine($"Part 1: {part1}");
        return 0;
    }

    private int CalculateSecretTo(int curSecret, int generations)
    {
        List<BananaPrice> prices = new List<BananaPrice>();
        int lastPrice = 0;
        for (int i = 0; i < generations; i++)
        {
            //Step 1
            //Mul curSecretby 64
            //XOR Val by curSecret - Set to curSecret
            //Prune (bitwise & with 16777216-1)
            int valMul = curSecret << 6;
            int valMix = valMul ^ curSecret;
            curSecret = valMix & 16777215;

            //Step 2
            //curSecret dikide by 32
            //XOR Val by curSecret - Set to curSecret
            //Prune (bitwise & with 16777216-1)
            int valDiv = curSecret >> 5;
            valMix = valDiv ^ curSecret;
            curSecret = valMix & 16777215;

            //Step 3
            //Mul curSecretby 2048
            //XOR Val by curSecret - Set to curSecret
            //Prune (bitwise & with 16777216-1)
            valMul = curSecret << 11;
            valMix = valMul ^ curSecret;
            curSecret = valMix & 16777215;

            //Storage for Part 2
            BananaPrice price = new BananaPrice();
            price.Price = curSecret % 10;
            price.Delta = price.Price - lastPrice;
            lastPrice = price.Price;
            prices.Add(price);
        }

        BuyersPrices.Add(prices);
        
        return curSecret;
    }

    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {

            //Step 1
            foreach(int num in InitialSecretNumbers)
            {
                int finalSecret = CalculateSecretTo(num, 2000);
                FinalSecretNumbers.Add(finalSecret);
            }

            //Step 2            
            int curBest = 0;
            //foreach(List<BananaPrice> prices in BuyersPrices)
            for (int p = 0; p < BuyersPrices.Count; p++)
            {
                Console.WriteLine($"Buyer {p}");
                List<BananaPrice> prices = BuyersPrices[p];
                for(int i = 1; i < prices.Count - 3; i++)
                {
                    BananaPrice first = prices[i];
                    BananaPrice second = prices[i + 1];
                    BananaPrice third = prices[i + 2];
                    BananaPrice fourth = prices[i + 3];
                  
                    int bananaCount = CalculatePrice(first.Delta, second.Delta, third.Delta, fourth.Delta);

                    if (bananaCount > curBest)
                    {
                        curBest = bananaCount;
                    }
                }
            }

            Console.WriteLine($"Part 2: {curBest}");
        });

        
        return 0;
    }

    private Dictionary<(int, int, int, int), int> _bananaPrices = new Dictionary<(int, int, int, int), int>();
    private int CalculatePrice(int delta1, int delta2, int delta3, int delta4)
    {
        var key = (delta1, delta2, delta3, delta4);
        if (_bananaPrices.ContainsKey(key))
        {
            return _bananaPrices[key];
        }

        int bananaCount = 0;
        foreach (List<BananaPrice> BuyerList in BuyersPrices)
        {
            int bestPrice = 0;
            for (int i = 1; i < BuyerList.Count - 3; i++)
            {
                BananaPrice first = BuyerList[i];
                if (first.Delta != delta1)
                {
                    continue;
                }
                BananaPrice second = BuyerList[i + 1];
                if (second.Delta != delta2)
                {
                    continue;
                }
                BananaPrice third = BuyerList[i + 2];
                if (third.Delta != delta3)
                {
                    continue;
                }
                BananaPrice fourth = BuyerList[i + 3];
                if (fourth.Delta != delta4)
                {
                    continue;
                }
                
                bestPrice = fourth.Price;
                break; //Stop the first time you hear it per buyer                
            }

            bananaCount += bestPrice;
        }

        _bananaPrices[key] = bananaCount;

        return bananaCount;

    }
}
