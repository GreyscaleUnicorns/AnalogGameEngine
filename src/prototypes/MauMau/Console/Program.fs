namespace MauMau.Console

open System

open AnalogGameEngine.Core

module Program =
    let rec playPhase game =
        printfn "Which card do you want to play? Type a number: "

        match Console.ReadLine() with
        | PInt value when value <= ActivePlayer.getHandSize game ->
            match ActivePlayer.playCard game (value - 1) with
            | game, Some card -> game, card
            | game, None -> playPhase game
        | _ -> playPhase game

    let rec turnLoop game =
        // Check if game is over
        match game with
        | Game.Ended winner -> printfn "Game ended: %s won" (Player.getName winner)
        | game ->
            let mutable game = game

            // Change player screen
            printfn "It's your turn, %s!" (ActivePlayer.getName game)
            printfn "Press any key"
            Console.ReadKey() |> ignore

            // Show own cards
            Console.Clear()
            printfn "Cards in deck: %i" (Deck.getSize game)
            printfn "Card on pile: %A" (Pile.getTopCardType game)
            printfn ""

            printfn "Your cards (%s):" (ActivePlayer.getName game)

            // Save old console color to restore it later
            let oldColor = Console.ForegroundColor
            let mutable i = 1

            for card in ActivePlayer.getHand game do
                Console.ForegroundColor <-
                    if Card.isPlayable game card then
                        ConsoleColor.Green
                    else
                        ConsoleColor.Red

                printfn "%i: %A" i card
                i <- i + 1
            // Restore old console color
            Console.ForegroundColor <- oldColor
            printfn ""

            // Play Phase
            let playedCard =
                if List.forall (Card.isPlayable game >> not) (ActivePlayer.getHand game) then
                    // No card playable
                    printfn "None of your cards is playable."
                    // Draw a card
                    let (game', card) = Deck.drawCard (ActivePlayer.getIdx game) game
                    game <- game'
                    printfn "You drew: %A" card
                    printfn "Press any key"
                    Console.ReadKey() |> ignore
                    None
                else
                    let (game', playedCard) = playPhase game
                    game <- game'
                    Some playedCard

            Console.Clear()
            // Print what happened last turn
            Option.iter (Card.printEffect game) playedCard

            game <- Game.nextPlayer game
            turnLoop game

    [<EntryPoint>]
    let main _ =
        let game =
            [ "Hans"; "Peter"; "Friedrich" ]
            |> List.map Player.create
            |> List.toArray
            |> Game.create

        Console.Clear()

        turnLoop game
        0
