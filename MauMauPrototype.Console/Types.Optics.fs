namespace MauMauPrototype

open SimpleOptics

open AnalogGameEngine

module GameDataOptic =
    let deck =
        Lens((fun data -> data.deck), (fun data deck' -> { data with deck = deck' }))

    let pile =
        Lens((fun data -> data.pile), (fun data pile' -> { data with pile = pile' }))

module PlayerDataOptic =
    let hand =
        Lens((fun playerData -> playerData.hand), (fun playerData hand' -> { playerData with hand = hand' }))

    let name =
        Lens((fun playerData -> playerData.name), (fun playerData name' -> { playerData with name = name' }))

module PlayerOptic =
    let hand = PlayerOptic.data >-> PlayerDataOptic.hand
    let name = PlayerOptic.data >-> PlayerDataOptic.name

module GameOptic =
    let data: Lens<Game, GameData> = GameOptic.data
    let deck = data >-> GameDataOptic.deck
    let pile = data >-> GameDataOptic.pile

    let inline playerHand idx =
        GameOptic.playerData idx >-> PlayerDataOptic.hand
