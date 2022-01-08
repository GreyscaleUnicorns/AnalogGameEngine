namespace MauMauPrototype

open AnalogGameEngine.Optics
open AnalogGameEngine
open System

[<RequireQualifiedAccess>]
module Players =
    let inline get game = Optic.get GameOptic.players game

    let getAmount game = get game |> Array.length

    let removeHandCards game =
        let removeCardsFromHand player =
            Optic.set PlayerOptic.hand CardList.empty player

        let removeCardsFromHands players = Array.map removeCardsFromHand players

        Optic.map GameOptic.players removeCardsFromHands game

[<RequireQualifiedAccess>]
module Deck =
    let inline get game = Optic.get GameOptic.deck game

    let getSize game = get game |> CardStack.size

    let drawCard playerIndex game =
        match get game |> CardStack.splitTopCard with
        | Some card, tail ->
            let game' =
                game
                // Update player hand
                |> Optic.map (GameOptic.playerHand playerIndex) (CardList.addCardToFront card)
                // Update deck
                |> Optic.set GameOptic.deck tail

            game', card |> Optic.get CardOptic.cardType
        | None, _ -> failwith "No more cards left in deck :("

    let everyoneDraws game =
        [ 0 .. (Players.getAmount game - 1) ]
        |> List.fold (fun game index -> drawCard index game |> fst) game

    let rec everyoneDrawsX x game =
        match x with
        | 0 -> game
        | x when x > 0 ->
            let game' = everyoneDraws game
            everyoneDrawsX (x - 1) game'
        | x -> failwithf "x must be positive but was %i" x

    let putCardOnPile game =
        match get game |> CardStack.splitTopCard with
        | Some card, tail ->
            game
            // Update pile
            |> Optic.map GameOptic.pile (CardStack.addCardToTop card)
            // Update deck
            |> Optic.set GameOptic.deck tail
        | None, _ -> failwith "No more cards left in deck :("

[<RequireQualifiedAccess>]
module Pile =
    let inline get game = Optic.get GameOptic.pile game

    let getSize game = get game |> CardStack.size

    let getTopCardType game =
        get game
        |> CardStack.getTopCard
        |> Optic.get CardOptic.cardType

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Card =
    let alwaysPlayableValues = [ Jack ]

    let isPlayable game card =
        let (cardValue, cardColor) = Optic.get CardOptic.cardType card
        let (topCardValue, topCardColor) = Pile.getTopCardType game

        let colorMatch = cardColor = topCardColor
        let valueMatch = cardValue = topCardValue
        let alwaysPlayable = List.contains cardValue alwaysPlayableValues

        colorMatch || valueMatch || alwaysPlayable

[<RequireQualifiedAccess>]
module ActivePlayer =
    let inline get game = Optic.get GameOptic.activePlayer game

    let inline getIdx game =
        Optic.get GameOptic.activePlayerIdx game

    let inline getHand game = get game |> Optic.get PlayerOptic.hand

    let inline getHandCard game cardIdx =
        getHand game
        |> Optic.get (CardListOptic.card cardIdx)
        |> Option.get

    let inline getHandSize game = getHand game |> CardList.length
    let inline getName game = get game |> Optic.get PlayerOptic.name

    let playCard game cardIdx =
        let card = getHandCard game cardIdx

        if Card.isPlayable game card then
            let game' =
                game
                // Update player hand
                |> Optic.map (GameOptic.playerHand (getIdx game)) (CardList.removeCardAt cardIdx)
                // Update pile
                |> Optic.map GameOptic.pile (CardStack.addCardToTop card)

            game', true
        else
            printfn "Card is not playable"
            game, false

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Game =
    let random = new Random()

    let (|Running|Ended|) game =
        Optic.get GameOptic.players game
        |> Array.forall (
            (Optic.get PlayerOptic.hand)
            >> CardList.isEmpty
            >> not
        )
        |> function
            | true -> Running
            | false -> Ended

    let reset game =
        let newDeck =
            let cardSet = Seq.allPairs (CardValue.getAll ()) (CardColor.getAll ())

            Seq.append cardSet cardSet
            |> Seq.map Card.create
            |> Seq.toList
            |> CardList.shuffle random

        game
        |> Game.Base.reset
        |> Optic.set GameOptic.deck newDeck
        |> Players.removeHandCards
        // Distribute first six hand cards
        |> Deck.everyoneDrawsX 6
        // Put first card onto pile
        |> Deck.putCardOnPile

    let create players =
        let data =
            {
                deck = CardStack.empty
                pile = CardStack.empty
            }

        Game.Base.create data players |> reset
