using CounterStrikeSharp.API.Modules.Utils;
using SharedLibrary;
using SharedLibrary.Entries;
using System.Diagnostics;

namespace _unitTests
{
    [TestClass]
    public class AdminMenuTests
    {
        const string _playerStatFilePath = "./Resources/";

        [TestMethod]
        public void PrintTopTest()
        {
            string resultMessage;

            resultMessage = PrintTop(1, false);
            Debug.WriteLine(resultMessage);
            resultMessage = PrintTop(2, false);
            Debug.WriteLine(resultMessage);
            resultMessage = PrintTop(3, false);
            Debug.WriteLine(resultMessage);

        }

        [TestMethod]
        public void TeamShuffleTest()
        {
            Dictionary<string, PlayerStatEntry> statEntry = StatisticHelper.LoadMonthsStats(_playerStatFilePath, 3);

            List<PlayerStatEntry> players = [];

            foreach (var player in statEntry.OrderByDescending(p => p.Value.Score))
            {
                players.Add(player.Value);
            }

            var method1Result = GetShuffleResult(players, 1);
            var method2Result = GetShuffleResult(players, 2);
            var method3Result = GetShuffleResult(players, 3);

            var bestMethod = method1Result.Difference <= method2Result.Difference && method1Result.Difference <= method3Result.Difference ? method1Result :
                             method2Result.Difference <= method3Result.Difference ? method2Result : method3Result;

            var result = $"Used shuffle method {bestMethod.MethodNumber} with difference {bestMethod.Difference:F2}% (Method1: {method1Result.Difference:F2}%, Method2: {method2Result.Difference:F2}%, Method3: {method3Result.Difference:F2}%)";
            Console.WriteLine(result);
        }

        private static ShuffleResult GetShuffleResult(List<PlayerStatEntry> sortedPlayers, int methodNumber)
        {
            var teamTSteamId2List = new List<string>();
            var teamCTSteamId2List = new List<string>();

            int totalPlayers = sortedPlayers.Count;
            int maxTeamSizeT = totalPlayers / 2 + (totalPlayers % 2);
            int maxTeamSizeCT = totalPlayers / 2;

            double difference = methodNumber switch
            {
                1 => ShuffleMethod1(sortedPlayers, maxTeamSizeT, maxTeamSizeCT, teamTSteamId2List, teamCTSteamId2List),
                2 => ShuffleMethod2(sortedPlayers, maxTeamSizeT, maxTeamSizeCT, teamTSteamId2List, teamCTSteamId2List),
                3 => ShuffleMethod3(sortedPlayers, maxTeamSizeT, maxTeamSizeCT, teamTSteamId2List, teamCTSteamId2List),
                _ => double.MaxValue
            };

            return new ShuffleResult(methodNumber, teamTSteamId2List, teamCTSteamId2List, difference);
        }

        private static double ShuffleMethod1(List<PlayerStatEntry> sortedPlayers, int maxTeamSizeT, int maxTeamSizeCT, List<string> teamTSteamId2List, List<string> teamCTSteamId2List)
        {
            double sumScoreT = 0;
            double sumScoreCT = 0;

            foreach (var player in sortedPlayers)
            {
                bool teamTHasSpace = teamTSteamId2List.Count < maxTeamSizeT;
                bool teamCTHasSpace = teamCTSteamId2List.Count < maxTeamSizeCT;

                if (!teamTHasSpace)
                {
                    teamCTSteamId2List.Add(player.Identity);
                    sumScoreCT += player.Score;
                    continue;
                }

                if (!teamCTHasSpace)
                {
                    teamTSteamId2List.Add(player.Identity);
                    sumScoreT += player.Score;
                    continue;
                }

                if (sumScoreT <= sumScoreCT)
                {
                    teamTSteamId2List.Add(player.Identity);
                    sumScoreT += player.Score;
                }
                else
                {
                    teamCTSteamId2List.Add(player.Identity);
                    sumScoreCT += player.Score;
                }
            }

            return StatisticHelper.GetPercentageDifference(sumScoreCT, sumScoreT);
        }

        private static double ShuffleMethod2(List<PlayerStatEntry> sortedPlayers, int maxTeamSizeT, int maxTeamSizeCT, List<string> teamTSteamId2List, List<string> teamCTSteamId2List)
        {
            double sumScoreT = 0;
            double sumScoreCT = 0;
            bool switchTeam = true;

            foreach (var player in sortedPlayers)
            {
                if (switchTeam)
                {
                    if (teamTSteamId2List.Count < maxTeamSizeT)
                    {
                        teamTSteamId2List.Add(player.Identity);
                        sumScoreT += player.Score;
                    }
                    else
                    {
                        teamCTSteamId2List.Add(player.Identity);
                        sumScoreCT += player.Score;
                    }
                }
                else
                {
                    if (teamCTSteamId2List.Count < maxTeamSizeCT)
                    {
                        teamCTSteamId2List.Add(player.Identity);
                        sumScoreCT += player.Score;
                    }
                    else
                    {
                        teamTSteamId2List.Add(player.Identity);
                        sumScoreT += player.Score;
                    }
                }
                switchTeam = !switchTeam;
            }

            return StatisticHelper.GetPercentageDifference(sumScoreCT, sumScoreT);
        }

        private static double ShuffleMethod3(List<PlayerStatEntry> sortedPlayers, int maxTeamSizeT, int maxTeamSizeCT, List<string> teamTSteamId2List, List<string> teamCTSteamId2List)
        {
            double sumScoreT = 0;
            double sumScoreCT = 0;
            bool assignToT = true; // Start with T team (which gets the extra player if odd total)

            foreach (var player in sortedPlayers)
            {
                bool teamTFull = teamTSteamId2List.Count >= maxTeamSizeT;
                bool teamCTFull = teamCTSteamId2List.Count >= maxTeamSizeCT;

                // If one team is full, add to the other
                if (teamTFull && !teamCTFull)
                {
                    teamCTSteamId2List.Add(player.Identity);
                    sumScoreCT += player.Score;
                    assignToT = !assignToT;
                    continue;
                }

                if (teamCTFull && !teamTFull)
                {
                    teamTSteamId2List.Add(player.Identity);
                    sumScoreT += player.Score;
                    assignToT = !assignToT;
                    continue;
                }

                // Both teams have space: use alternating strategy biased by score difference
                // If score difference is large, prioritize adding to lower-scoring team
                double scoreDifference = Math.Abs(sumScoreT - sumScoreCT);
                bool shouldAddToLowerScoreTeam = scoreDifference > player.Score * 0.5;

                if (shouldAddToLowerScoreTeam)
                {
                    if (sumScoreT < sumScoreCT)
                    {
                        teamTSteamId2List.Add(player.Identity);
                        sumScoreT += player.Score;
                    }
                    else
                    {
                        teamCTSteamId2List.Add(player.Identity);
                        sumScoreCT += player.Score;
                    }
                }
                else
                {
                    // Default: alternate between teams (with T team preference for even distribution)
                    if (assignToT)
                    {
                        teamTSteamId2List.Add(player.Identity);
                        sumScoreT += player.Score;
                    }
                    else
                    {
                        teamCTSteamId2List.Add(player.Identity);
                        sumScoreCT += player.Score;
                    }
                }

                assignToT = !assignToT;
            }

            return StatisticHelper.GetPercentageDifference(sumScoreCT, sumScoreT);
        }

        private record PlayerShuffleData(string SteamId2, PlayerStatEntry Stats);

        private record ShuffleResult(int MethodNumber, List<string> TeamTSteamId2List, List<string> TeamCTSteamId2List, double Difference);


        private static double GetSumScores(List<PlayerStatEntry> tPlayers)
        {
            double sumScore = 0.0;
            foreach (var player in tPlayers)
            {
                sumScore += player.Score;
            }
            return sumScore;
        }

        private static string PrintTop(int month, bool reverse = false)
        {
            Debug.WriteLine($"In the last {month} month:");
            var storedStats =
                StatisticHelper.LoadMonthsStats(_playerStatFilePath, month)
                .Where(p => p.Value.Kill > 200)
                .OrderByDescending(p => p.Value.Score).ToList()
                ?? [];

            if (reverse)
            {
                storedStats.Reverse();
            }

            string topList = string.Empty;
            if (storedStats != null && storedStats.Count != 0)
            {

                if (storedStats.Count < 10)
                {
                    for (int i = 0; i < storedStats.Count; i++)
                    {
                        topList += $"{i + 1}. {storedStats[i].Value.Name}({storedStats[i].Value.Score:F2}), ";
                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        topList += $"{i + 1}. {storedStats[i].Value.Name}({storedStats[i].Value.Score:F2}), ";
                    }
                }

            }
            return $" {topList}";
        }


    }
}
