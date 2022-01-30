namespace AnalogGameEngine.Core

type Player<'Data> = { data: 'Data }

type Game<'Data, 'PlayerData> =
    {
        activePlayerIdx: int
        data: 'Data
        players: Player<'PlayerData> array
    }

type Card<'CardType> = { cardType: 'CardType }

type CardList<'CardType> = Card<'CardType> list
