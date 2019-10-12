using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwoDecks
{
    class Deck
    {
        public string[] Description;

        private List<Card> cards;
        private Random random = new Random();

        public Deck(string filename)
        {
            cards = new List<Card>();
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    bool invalidCard = false;
                    string nextCard = reader.ReadLine();
                    string[] cardParts = nextCard.Split(new char[] { ' ' });
                    Values value = Values.Ace;
                    switch (cardParts[0])
                    {
                        case "Ace": value = Values.Ace; break;
                        case "Two": value = Values.Two; break;
                        default: invalidCard = true; break;
                    }
                    Suits suit = Suits.Clubs;
                    switch (cardParts[2])
                    {
                        case "Hearts": suit = Suits.Hearts; break;
                        default: invalidCard = true; break;
                    }
                    if (!invalidCard)
                    {
                        MessageBox.Show("Oznacio si folder33 sa Desktopa i u njemu file 'Ace of Hearts' !");
                        Description = cardParts;
                    }
                    else
                    {
                        Description = cardParts;
                    }
                  
                }
            }
        }

        public Deck()
        {
            cards = new List<Card>();
            for (int suit = 0; suit <= 3; suit++)
                for (int value = 1; value <= 3; value++)
                    cards.Add(new Card((Suits)suit, (Values)value));
        }

        public Deck(IEnumerable<Card> initialCards)
        {
            cards = new List<Card>(initialCards);
        }

        public int Count { get { return cards.Count; } }

        public void Add(Card cardToAdd)
        {
            cards.Add(cardToAdd);
        }

        public Card Deal(int index)
        {
            Card CardToDeal = cards[index];
            cards.RemoveAt(index);
            return CardToDeal;
        }

        public void Shuffle()
        {
            List<Card> NewCards = new List<Card>();
            while (cards.Count > 0)
            {
                int CardToMove = random.Next(cards.Count);
                NewCards.Add(cards[CardToMove]);
                cards.RemoveAt(CardToMove);
            }
            cards = NewCards;
        }

        public IEnumerable<string> GetCardNames()
        {
            string[] CardNames = new string[cards.Count];
            for (int i = 0; i < cards.Count; i++)
            {
                CardNames[i] = cards[i].Name;
            }
            return CardNames;
        }

        public void Sort()
        {
            cards.Sort(new CardComparer_bySuit());
        }

        public Card Peek(int cardNumber)
        {
            return cards[cardNumber];
        }

        public Card Deal()      //Ovdje vraca Deal Method a prosljedjuje prvu kartu u kolekciji.
        {
            return Deal(0);
        }

        public Deck PullOutValues(Values value) //Koristi Player klasa u metodi:  public Deck DoYouHaveAny(Values value)  
        {
            Deck deckToReturn = new Deck(new Card[] { });
            for (int i = cards.Count - 1; i >= 0; i--)
                if (cards[i].Value == value)
                    deckToReturn.Add(Deal(i));
            return deckToReturn;
        }

        public void SortByValue()
        {
            cards.Sort(new CardComparer_byValue());
        }
    }
}
