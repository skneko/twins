﻿using System;
using System.Collections.Generic;
using System.Linq;
using static Twins.Utils.CollectionExtensions;

namespace Twins.Models
{
    public class CategoryGame : Game
    {
        public CategoryGame(int height, int width, Deck deck, TimeSpan timeLimit, TimeSpan turnLimit, Board.Cell[,] cells = null, int level = 0)
            : base(height, width, deck, timeLimit, turnLimit, cells, level)
        {
            if (deck.Categories.Count() < 2)
            {
                throw new ArgumentException("La baraja seleccionada no tiene suficientes categorías definidas.");
            }

            SetRandomReferenceCategory();
        }

        public override IEnumerable<Board.Cell> TryMatch()
        {
            var reference = Board.FlippedCells.First().Card;
            bool isMatch = Board.FlippedCells.All(
                c => c.Card.Equals(reference)
                    && c.Card.Categories.Contains(Board.ReferenceCategory));
            return HandleMatchResult(isMatch);
        }

        public override void EndTurn()
        {
            base.EndTurn();

            if (!IsFinished && Board.ReferenceCard == null)
            {
                CycleReferenceCategory();
            }
        }

        void CycleReferenceCategory()
        {
            var sameCategoryCards = Board.Cells.Where(c => !c.KeepRevealed
                                    && c.Card.Categories.Contains(Board.ReferenceCategory));
            if (sameCategoryCards.Any())
            {
                Board.ReferenceCard = RandomHiddenCard(sameCategoryCards);
            }
            else
            {
                SetRandomReferenceCategory();
            }
        }

        void SetRandomReferenceCategory()
        {
            var card = RandomHiddenCard(Board.Cells);
            Board.ReferenceCard = card;
            Board.ReferenceCategory = card.Categories
                                          .ToList()
                                          .PickRandom();
        }
    }
}
