using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileyFaceGameEngine
{
    public class Deck
    {
        List<string> cardTypes = new List<string> {"A","K","Q","J","10","9","8","7","6","5","4","3","2"};
        List<int> cardValues = new List<int> {14,13,12,11,10,9,8,7,6,5,4,3,2};
        List<char> cardSuits = new List<char> {'♣','♦','♥','♠'};
        public List<Card> Cards { get; set; }

        public Deck()
        {
            GenerateDeck();
        }

        private void GenerateDeck()
        {
            Cards = new List<Card>();
            foreach (var suit in cardSuits)
            {
                foreach (var type in cardTypes)
                {
                    var card = new Card
                    {
                        Suit = suit,
                        Value = type
                    };
                    Cards.Add(card);
                }
            }
        }

        public void Shuffle()
        {
            var AmountOfCards = Cards.Count;
            var usedCardIndecies = new List<int>();
            var newDeck = new List<Card>();
            var rand = new Random();

            for (int i = 0; i < AmountOfCards; i++)
            {
                var index = GetIndex(usedCardIndecies, AmountOfCards, rand);
                usedCardIndecies.Add(index);

                newDeck.Add(Cards[index]);
            }

            Cards = newDeck;
        }

        private int GetIndex(List<int> usedCardIndecies, int amountOfCards, Random rand)
        {
            var newIndexFound = false;
            var index = 0;

            while (!newIndexFound)
            {
                index = rand.Next(0, amountOfCards);

                if (usedCardIndecies.Count == 0)
                {
                    newIndexFound = true;
                }
                else
                {
                    foreach (var cardIndex in usedCardIndecies)
                    {
                        if (index != cardIndex) newIndexFound = true;
                    }
                }
            }

            return index;
        }

        public void OutputDeck()
        {
            foreach (var card in Cards)
            {
                Console.WriteLine(card.Value + card.Suit);
            }
        }
    }
}
