namespace MauMauPrototype

open AnalogGameEngine.Optics
open AnalogGameEngine

module Game =
    let drawCardFromDeck game playerIndex : Game =
        match CardStack.splitTopCard game.data.deck with
        | Some card, tail ->
            game
            // Update player hand
            |> Optic.map (GameOptic.playerHand playerIndex) (CardList.addCardToFront card)
            // Update deck
            |> Optic.set GameOptic.deck tail
        | None, _ -> failwith "No more cards left in deck :("
