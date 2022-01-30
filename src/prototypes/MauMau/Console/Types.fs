namespace MauMau.Console

open AnalogGameEngine.Core

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

type GameData = { deck: CardList; pile: CardList }

type PlayerData = { name: string; hand: CardList }
type Player = Player<PlayerData>

type Game = Game<GameData, PlayerData>
