using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace _5000
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue dans le jeu de dés 5000!");
            string[] playerNames = { "\u001b[31mA\u001b[0m", "\u001b[34mB\u001b[0m", "\u001b[32mC\u001b[0m", "\u001b[33mD\u001b[0m" };
            int[] totalPoints = new int[] { 0, 0, 0, 0 };

            while (true)
            {
                for (int currentPlayerIndex = 0; currentPlayerIndex < 4; currentPlayerIndex++)
                {
                    Console.WriteLine($"{playerNames[currentPlayerIndex]}, appuyez sur une touche pour lancer les dés...");
                    Console.WriteLine();
                    Console.ReadKey();

                    int currentPlayerPoints = PlayTurn();

                    Console.WriteLine($"{playerNames[currentPlayerIndex]}, votre score total est de {currentPlayerPoints} points.");
                    Console.WriteLine();
                    totalPoints[currentPlayerIndex] += currentPlayerPoints;

                    if (totalPoints[currentPlayerIndex] >= 5000)
                    {
                        Console.WriteLine($"{playerNames[currentPlayerIndex]}, félicitations! Vous avez atteint {totalPoints[currentPlayerIndex]} points. Vous avez gagné!");
                        Console.WriteLine();  
                        return;
                    }
                }
            }
        }
        static void EndGame(string[] playerNames, int[] totalPoints)
        {
            Console.WriteLine("Fin de la partie. Voici les scores finaux :");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"{playerNames[i]}: {totalPoints[i]} points");
                Console.WriteLine();
            }
        }
        static int PlayTurn()
        {
            int totalPoints = 0;
            Random random = new Random();
            int remainingDice = 5;

            while (true)
            {
                int[] diceResults = RollDice(remainingDice, random);
                int roundPoints = CalculatePoints(diceResults);

                if (roundPoints == 0)
                {
                    Console.WriteLine("Vous avez lancé un 1. Tour suivant!");
                    break;
                }

                Console.WriteLine($"Résultat du lancer : {string.Join(", ", diceResults)}");
                Console.WriteLine($"Points du tour : {roundPoints}");

                totalPoints += roundPoints;

                if (remainingDice == 0)
                {
                    Console.WriteLine("Vous avez marqué avec tous les dés. Vous pouvez recommencer avec les 5 dés !");
                    remainingDice = 5;
                }
                else
                {
                    Console.WriteLine($"Total des points : {totalPoints}");
                    Console.WriteLine($"Dés restants : {remainingDice}");

                    if (totalPoints >= 500) 
                    {
                        Console.WriteLine("Vous décidez de ne pas relancer.");
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Vous décidez de relancer les dés qui n'ont pas marqué de points.");
                        Console.WriteLine();
                        remainingDice = diceResults.Count(result => result == 1 || result == 5);
                    }
                }
            }

            return totalPoints;
        }
        static int[] RollDice(int numDice, Random random)
        {
            int[] results = new int[numDice];

            for (int i = 0; i < numDice; i++)
            {
                results[i] = random.Next(1, 7);
            }

            return results;
        }
        static int CalculatePoints(int[] diceResults)
        {
            int points = 0;

            List<int> counts = diceResults.GroupBy(x => x).Select(g => g.Count()).ToList();

            foreach (var count in counts)
            {
                if (count == 1) 
                {
                    if (count == 1) points += 100; 
                    if (count == 5) points += 50;  
                }
                else if (count == 3) 
                {
                    points += 100 * diceResults.First(d => diceResults.Count(x => x == d) == 3);
                }
            }

            if (counts.Contains(3) && counts.Contains(1)) 
            {
                points += 1000;
            }
            return points; 
        }
    }
}
