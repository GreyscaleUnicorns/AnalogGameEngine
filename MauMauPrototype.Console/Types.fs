namespace MauMauPrototype

open AnalogGameEngine

type CardColor =
    | Clubs
    | Spades
    | Diamonds
    | Hearts

type CardValue =
    | Seven
    | Eight
    | Nine
    | Ten
    | Jack
    | Queen
    | King
    | Ace

type CardType = CardValue * CardColor
type Card = Card<CardType>
type CardList = CardList<CardType>

type GameData = { deck: CardList }

type PlayerData = { hand: CardList }

type Game = Game<GameData, PlayerData>
