using AnalogGameEngine.Entities;
using System;
using System.Linq;

using MauMauPrototype.Effects;

namespace MauMauPrototype {
    class Program {
        static void Main(string[] args) {
            var conductor = new MauMauConductor();
            var game = conductor.StartGame();

            while (!GameHasEnded(game)) {
                // Change player screen
                Console.Clear();
                Console.WriteLine("It's your turn, " + game.ActivePlayer.Name + "!");
                Console.Write("Press any key");
                Console.ReadKey();

                // Draw Phase
                if (game.Stacks["deck"].Cards.Count > 0) {
                    game.Stacks["deck"].TopCard.moveTo(game.ActivePlayer.Sets["hand"]);
                }

                // Show own cards
                Console.Clear();
                Console.WriteLine("Cards in deck: " + game.Stacks["deck"].Cards.Count);
                Console.WriteLine("Card on pile: " + game.Stacks["discard-pile"].TopCard.Type);
                Console.WriteLine();
                Console.WriteLine("Your cards (" + game.ActivePlayer.Name + "):");
                var i = 1;
                var oldColor = Console.ForegroundColor;
                foreach (var card in game.ActivePlayer.Sets["hand"].Cards) {
                    if (cardPlayable(game, card)) {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine(i++ + ": " + card.Type);
                }
                Console.ForegroundColor = oldColor;
                Console.WriteLine();

                // Play Phase
                var validOption = false;
                if (Array.TrueForAll(game.ActivePlayer.Sets["hand"].Cards.ToArray(), c => !cardPlayable(game, c))) {
                    // No card playable
                    Console.WriteLine("None of your cards is playable.");
                    if (game.Stacks["deck"].TopCard != null) {
                        Console.WriteLine("You drew: " + game.Stacks["deck"].TopCard.Type);
                        game.Stacks["deck"].TopCard.moveTo(game.ActivePlayer.Sets["hand"]);
                    }
                    Console.WriteLine("Press any key");
                    Console.ReadKey();
                    validOption = true;
                }

                while (!validOption) {
                    Console.Write("Which card do you want to play? Type a number: ");
                    string option = Console.ReadLine();
                    int number;
                    if (int.TryParse(option, out number) && number > 0
                        && number <= game.ActivePlayer.Sets["hand"].Cards.Count) {
                        if (playCard(game, game.ActivePlayer.Sets["hand"].Cards.ElementAt(number - 1))) {
                            validOption = true;
                        }
                        else {
                            Console.WriteLine("This card is not playable right now!");
                        }
                    }
                }

                // Advance to next Player
                game.NextTurn();
            }
        }

        static bool cardPlayable(MauMauGame game, MauMauCard card) {
            MauMauCard topCard = game.Stacks["discard-pile"].TopCard;
            MauMauCardType topCardType = (MauMauCardType)topCard.Type;

            var alwaysPlayableValues = new Values[] { Values.Jack };
            bool colorMatch = topCardType.Color == ((MauMauCardType)card.Type).Color;
            bool valueMatch = topCardType.Value == ((MauMauCardType)card.Type).Value;
            bool alwaysPlayable = alwaysPlayableValues.Contains(((MauMauCardType)card.Type).Value);

            return colorMatch || valueMatch || alwaysPlayable;
        }

        static bool playCard(MauMauGame game, MauMauCard card) {
            if (cardPlayable(game, card)) {
                card.moveTo(game.Stacks["discard-pile"]);
                card.activateEffects();
                return true;
            }
            else {
                return false;
            }
        }

        static bool GameHasEnded(MauMauGame game) {
            var ended = false;
            foreach (var player in game.Players) {
                ended = ended || player.Sets["hand"].Cards.Count == 0;
            }
            return ended;
        }
    }
}
