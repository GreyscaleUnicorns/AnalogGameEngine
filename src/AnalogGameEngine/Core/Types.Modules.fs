namespace AnalogGameEngine.Core

open System

open SimpleOptics

[<RequireQualifiedAccess>]
module Game =
    let inline getPlayerAmount game =
        Optic.get GameOptic.players game |> Array.length

    let inline getNextPlayerIdx game =
        let activeIdx = Optic.get GameOptic.activePlayerIdx game
        (activeIdx + 1) % getPlayerAmount game

    let inline nextPlayer game =
        Optic.set GameOptic.activePlayerIdx (getNextPlayerIdx game) game

    /// Contains functions which do only part of the job for the base but should be
    /// implemented fully in the game itself to handle data correctly
    module Base =
        let inline reset game =
            Optic.set GameOptic.activePlayerIdx 0 game

        let create data players =
            {
                activePlayerIdx = 0
                data = data
                players = players
            }

[<RequireQualifiedAccess>]
module Card =
    let create cardType = { cardType = cardType }

[<RequireQualifiedAccess>]
module CardList =
    let empty: CardList<'a> = []
    let inline addCardToFront card list : CardList<'a> = card :: list
    let inline addCardToBack card list : CardList<'a> = list |> List.append [ card ]
    let inline isEmpty (list: CardList<'a>) = List.isEmpty list
    let inline getFirstCard (list: CardList<'a>) = List.head list
    let inline length (list: CardList<'a>) = List.length list
    let inline removeCardAt idx (list: CardList<'a>) = List.removeAt idx list

    let inline shuffle (random: Random) list : CardList<'a> =
        list |> List.sortBy (fun _ -> random.Next())

    let inline splitFirstCard (list: CardList<'a>) =
        match list with
        | [] -> None, []
        | head :: tail -> Some head, tail

    let inline tryGetFirstCard (list: CardList<'a>) = List.tryHead list

[<RequireQualifiedAccess>]
module CardStack =
    let empty: CardList<'a> = CardList.empty
    let addCardToTop = CardList.addCardToFront
    let addCardToBottom = CardList.addCardToBack
    let getTopCard = CardList.getFirstCard
    let shuffle = CardList.shuffle
    let size = CardList.length
    let splitTopCard = CardList.splitFirstCard
    let tryGetTopCard = CardList.tryGetFirstCard
