using System;
using System.Collections.Generic;

using AnalogGameEngine;

namespace AnalogGameEngine.Entities
{
    /// <summary>
    /// Base for all types of card collections.
    /// </summary>
    public abstract partial class CardCollection
    {
        /// <summary>
        /// The list of all cards in the collection.
        /// </summary>
        /// <returns>A list of Cards</returns>
        public LinkedList<Card> Cards { get; private set; }

        /// <param name="cards">Initial cards in collection</param>
        public CardCollection(Card[] cards = null)
        {
            if (cards != null)
            {
                this.Cards = new LinkedList<Card>(cards);
            }
            else
            {
                this.Cards = new LinkedList<Card>();
            }
            this.RegisterEvents();
        }

        /// <summary>
        /// Adds a card to the end of the collection.
        /// </summary>
        /// <param name="card">card to append</param>
        /// <param name="position">position at which the card should be inserted</param>
        virtual public void AddCard(Card card, int position = 0)
        {
            // overwrite behaviour in inheriting classes
            this.Cards.AddLast(card);
        }

        /// <summary>
        /// Removes a Card from the Collection
        /// </summary>
        /// <param name="card">card to be removed</param>
        public bool RemoveCard(Card card)
        {
            return this.Cards.Remove(card);
        }

        public void MoveAllCardsTo(CardCollection collection)
        {
            if (collection != this)
            {
                while (this.Cards.Count > 0)
                {
                    this.Cards.First.Value.moveTo(collection);
                }
            }
        }

        /// <summary>
        /// Shuffles collection
        /// </summary>
        public void Shuffle()
        {
            var cardList = new List<Card>();
            LinkedList<Card> shuffledList = new LinkedList<Card>();
            Random rand = new Random(DateTime.Now.Ticks.GetHashCode());

            // Create list from collection
            foreach (Card card in this.Cards)
            {
                cardList.Add(card);
            }

            // Randomly move cards from list into new collection
            do
            {
                int index = rand.Next(0, cardList.Count);
                shuffledList.AddLast(cardList[index]);
                cardList.Remove(cardList[index]);
            } while (cardList.Count > 0);

            this.Cards = shuffledList;
        }
    }
}
