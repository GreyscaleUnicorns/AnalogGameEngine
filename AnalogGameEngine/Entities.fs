namespace AnalogGameEngine

open System

open AnalogGameEngine.Optics

type Player<'Data> = { data: 'Data }

module Player =
    // Optics
    let data =
        Lens((fun player -> player.data), (fun player data' -> { player with data = data' }))

type Game<'Data, 'PlayerData> =
    {
        activePlayer: int
        data: 'Data
        players: Player<'PlayerData> array
    }

module GameOptic =
    let activePlayer =
        Lens((fun game -> game.activePlayer), (fun game active' -> { game with activePlayer = active' }))

    let data =
        Lens((fun game -> game.data), (fun game data' -> { game with data = data' }))

    let players =
        Lens((fun game -> game.players), (fun game players' -> { game with players = players' }))

    let inline player index = players >-> Optic.Array.index index
    let inline playerData index = player index >-> Player.data

module Game =
    let inline getPlayerAmount game = game.players |> Array.length

    let getNextPlayer game =
        let index = (game.activePlayer + 1) % (getPlayerAmount game)

        Optic.get (GameOptic.player index) game
        |> Option.get

type Card<'CardType> = { cardType: 'CardType }

type CardList<'CardType> = Card<'CardType> list

module CardList =
    let addCardToFront card list : Card<'a> list = card :: list

    let addCardToBack card list : Card<'a> list = list |> List.append [ card ]

    let splitFirstCard (list: Card<'a> list) =
        match list with
        | [] -> None, []
        | head :: tail -> Some head, tail

module CardStack =
    let addCardToTop = CardList.addCardToFront
    let addCardToBottom = CardList.addCardToBack
    let splitTopCard = CardList.splitFirstCard
