namespace MauMauPrototype

open AnalogGameEngine.Optics
open AnalogGameEngine

module GameDataOptic =
    let deck =
        Lens((fun data -> data.deck), (fun data deck' -> { data with deck = deck' }))

module PlayerDataOptic =
    let hand =
        Lens((fun playerData -> playerData.hand), (fun playerData hand' -> { playerData with hand = hand' }))

module GameOptic =
    let data: Lens<Game, GameData> = GameOptic.data
    let deck = data >-> GameDataOptic.deck

    let inline playerHand index =
        GameOptic.playerData index
        >-> PlayerDataOptic.hand
