namespace MauMauPrototype

open System

open AnalogGameEngine

module Program =
    let rec playPhase game =
        printfn "Which card do you want to play? Type a number: "

        match Console.ReadLine() with
        | PInt value when value <= ActivePlayer.getHandSize game ->
            match ActivePlayer.playCard game (value - 1) with
            | game, true -> game
            | game, false -> playPhase game
        | _ -> playPhase game

    let rec turnLoop game =
        // Check if game is over
        match game with
        | Game.Ended -> printfn "Game ended"
        | Game.Running ->
            let mutable game = game

            // Change player screen
            Console.Clear()
            printfn "It's your turn, %s!" (ActivePlayer.getName game)
            printfn "Press any key"
            Console.ReadKey() |> ignore

            // Draw a card
            game <-
                Deck.drawCard (ActivePlayer.getIdx game) game
                |> fst

            // Show own cards
            Console.Clear()
            printfn "Cards in deck: %i" (Deck.getSize game)
            printfn "Card on pile: %A" (Pile.getSize game)
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
            if List.forall (Card.isPlayable game) (ActivePlayer.getHand game) then
                // No card playable
                printfn "None of your cards is playable."
                // Draw a card
                let (game', card) = Deck.drawCard (ActivePlayer.getIdx game) game
                game <- game'
                printfn "You drew: %A" card
                printfn "Press any key"
                Console.ReadKey() |> ignore
            else
                game <- playPhase game

            game |> Game.nextPlayer |> turnLoop

    [<EntryPoint>]
    let main _ =
        let game =
            [ "Hans"; "Peter"; "Friedrich" ]
            |> List.map Player.create
            |> List.toArray
            |> Game.create

        turnLoop game
        0
