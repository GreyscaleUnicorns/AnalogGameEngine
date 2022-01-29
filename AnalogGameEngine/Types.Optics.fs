namespace AnalogGameEngine

open SimpleOptics
open SimpleOptics.Presets

[<RequireQualifiedAccess>]
module PlayerOptic =
    let data =
        Lens((fun (player: Player<'a>) -> player.data), (fun player data' -> { player with data = data' }))

[<RequireQualifiedAccess>]
module CardOptic =
    let cardType =
        Lens((fun card -> card.cardType), (fun card cardType' -> { card with cardType = cardType' }))

[<RequireQualifiedAccess>]
module CardListOptic =
    let card idx : Prism<CardList<'a>, Card<'a>> = ListOptic.index idx

[<RequireQualifiedAccess>]
module GameOptic =
    let activePlayerIdx =
        Lens((fun game -> game.activePlayerIdx), (fun game active' -> { game with activePlayerIdx = active' }))

    let data =
        Lens((fun game -> game.data), (fun game data' -> { game with data = data' }))

    let players =
        Lens((fun game -> game.players), (fun game players' -> { game with players = players' }))

    let inline player idx = players >-> ArrayOptic.index idx
    let inline playerData idx = player idx >-> PlayerOptic.data

    let activePlayer =
        Lens(
            (fun game ->
                let idx = Optic.get activePlayerIdx game
                Optic.get (player idx) game |> Option.get),
            (fun game player' ->
                let idx = Optic.get activePlayerIdx game
                let game' = Optic.set (player idx) player' game
                Optic.set activePlayerIdx idx game')
        )
