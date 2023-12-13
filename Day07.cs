namespace AOC2023
{
    internal class Day07
    {
        private static readonly string input = File.ReadAllText(@"Input\07.txt");

        private readonly List<Hand> Hands = new();

        internal int Part1()
        {
            foreach (var line in input.Split(Environment.NewLine))
            {
                var values = line.Split(' ');
                Hands.Add(new()
                {
                    Cards = values[0].ToCharArray().ToList(),
                    Bid = int.Parse(values[1])
                });
            }
            Hands.Sort();
            int rank = 0;
            int answer = 0;
            foreach (var hand in Hands)
            {
                rank++;
                answer += hand.Bid * rank;
            }
            return answer;
        }

        internal int Part2()
        {
            foreach (var hand in Hands) { hand.IsPart2 = true; }
            Hands.Sort();
            int rank = 0;
            int answer = 0;
            foreach (var hand in Hands)
            {
                rank++;
                answer += hand.Bid * rank;
            }
            return answer;
        }


        internal class Hand : IComparable
        {
            public List<char>? Cards { get; set; }
            public int Bid { get; set; }
            public bool IsPart2 { get; set; }

            public override string ToString()
            {
                return $"Hand: {new string(Cards.ToArray())}, {HandType}, {Bid}";
            }

            public int CompareTo(object? obj)
            {
                if (obj is not Hand otherHand) throw new ArgumentException("These two things are not comparable.");
                if (otherHand.HandType != HandType)
                {
                    return (int)HandType - (int)otherHand.HandType;
                }

                for (int i = 0; i < 5; i++)
                {
                    if (otherHand.Cards[i] == Cards[i]) continue;
                    return IsPart2
                        ? Values2[Cards[i]] - Values2[otherHand.Cards[i]]
                        : Values[Cards[i]] - Values[otherHand.Cards[i]];
                }
                return 0;
            }

            public HandType HandType
            {
                get
                {
                    var groups = new Dictionary<char, int>();
                    foreach (var card in Cards)
                    {
                        if (!groups.ContainsKey(card))
                        {
                            groups.Add(card, 1);
                        }
                        else
                        {
                            groups[card]++;
                        }
                    }

                    if (IsPart2 && groups.ContainsKey('J'))
                    {
                        int jokers = groups['J'];
                        groups.Remove('J');

                        switch (jokers)
                        {
                            case 5:
                                groups.Add('A', 5);
                                break;
                            case 4:
                                groups[groups.ElementAt(0).Key] += jokers;
                                break;
                            case 3:
                                {
                                    var c = groups.MaxBy(g => Values2[g.Key]).Key;
                                    groups[c] += jokers;
                                    break;
                                }
                            case 2:
                                switch (groups.Count)
                                {
                                    case 1:
                                        groups[groups.ElementAt(0).Key] += jokers;
                                        break;
                                    case 2:
                                        var key = groups.First(g => g.Value == 2).Key;
                                        groups[key] += jokers;
                                        break;
                                    case 3:
                                        var key2 = groups.MaxBy(g => Values2[g.Key]).Key;
                                        groups[key2] += jokers;
                                        break;
                                    default:
                                        throw new Exception("Unexpected number of groups");
                                }
                                break;
                            case 1:
                                switch (groups.Count)
                                {
                                    case 1:
                                        groups[groups.ElementAt(0).Key] += jokers;
                                        break;
                                    case 2:
                                        if (groups.Min(g => g.Value) == 1)
                                        {
                                            var key = groups.First(g => g.Value == 3).Key;
                                            groups[key] += jokers;
                                        }
                                        else
                                        {
                                            var key = groups.MaxBy(g => Values2[g.Key]).Key;
                                            groups[key] += jokers;
                                        }
                                        break;
                                    case 3:
                                        var key1 = groups.First(g => g.Value == 2).Key;
                                        groups[key1] += jokers;
                                        break;
                                    case 4:
                                        var key2 = groups.MaxBy(g => Values2[g.Key]).Key;
                                        groups[key2] += jokers;
                                        break;
                                    default:
                                        throw new Exception("Unexpected number of groups");
                                }
                                break;
                        }
                    }

                    return groups.Count switch
                    {
                        1 => HandType.FiveOfAKind,
                        2 => groups.Min(g => g.Value) switch
                        {
                            1 => HandType.FourOfAKind,
                            2 => HandType.FullHouse,
                            _ => throw new ArgumentException("Unexpected minimum number in a group"),
                        },
                        3 => groups.Max(g => g.Value) != 3 ? HandType.TwoPair : HandType.ThreeOfAKind,
                        4 => HandType.OnePair,
                        5 => HandType.HighCard,
                        _ => throw new ArgumentException("Unexpected number of groups"),
                    };
                }
            }
        }

        internal static Dictionary<char, int> Values = new()
        {
            { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            { 'T', 10 }, { 'J', 11 }, { 'Q', 12 }, { 'K', 13 }, { 'A', 14 }
        };

        internal static Dictionary<char, int> Values2 = new()
        {
            { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            { 'T', 10 }, { 'J', 1 }, { 'Q', 12 }, { 'K', 13 }, { 'A', 14 }
        };
    }

    internal enum HandType
    {
        HighCard = 1,
        OnePair = 2,
        TwoPair = 3,
        ThreeOfAKind = 4,
        FullHouse = 5,
        FourOfAKind = 6,
        FiveOfAKind = 7
    }

}
