using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GuessGame.Game;
using GuessGame.Players;

namespace GuessGame
{
    internal static class Program
    {
        private static async Task Main()
        {
            var players = InputPlayers();
            var game = new Board(players);
            await game.PlayAsync();
            Console.WriteLine(game.GameResult);
        }

        private static IEnumerable<(string, PlayerType)> InputPlayers()
        {
            Console.WriteLine("Enter amount of players");
            var playersNumberString = Console.ReadLine();
            var playerNumbers = Parse(playersNumberString);

            while (playerNumbers < 2 || playerNumbers > 8)
            {
                Console.WriteLine("The amount of participating players – 2 through 8. Enter again");
                playersNumberString = Console.ReadLine();
                playerNumbers = Parse(playersNumberString);
            }

            var players = new List<(string, PlayerType)>();
            while (playerNumbers-- != 0)
            {
                Console.WriteLine("Input a name of the player");
                var name = Console.ReadLine();

                PlayerType type;
                Console.WriteLine(EnumDescriptionToString());
                Console.WriteLine("Print a corresponding number of the type to pick it");
                while (true)
                {
                    var typeString = Console.ReadLine();

                    if (typeString != null && int.TryParse(typeString, out var enumVal))
                        if (Enum.IsDefined(typeof(PlayerType), enumVal))
                        {
                            type = (PlayerType) enumVal;
                            break;
                        }

                    Console.WriteLine("Number is incorrect. Try again");
                }

                players.Add((name, type));
            }

            return players;
        }

        private static string EnumDescriptionToString()
        {
            var enumType = typeof(PlayerType);
            var values = Enum.GetValues(enumType);
            var result = new StringBuilder();

            foreach (PlayerType val in values) result.AppendLine($"{(int) val}: {val}");

            return result.ToString();
        }


        private static int Parse(string input)
        {
            return input switch
            {
                "1" => 1,
                "2" => 2,
                "3" => 3,
                "4" => 4,
                "5" => 5,
                "6" => 6,
                "7" => 7,
                "8" => 8,
                _ => -1
            };
        }
    }
}