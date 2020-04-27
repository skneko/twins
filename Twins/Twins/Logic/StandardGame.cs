﻿using System;
using System.Collections.Generic;
using System.Linq;
using Twins.Logic;
using Twins.Model;

namespace Twins.Models
{
    public class StandardGame : Game
    {
        const int GroupSize = 2;

        public Deck Deck { get; }

        public int RemainingMatches { get; private set; }

        public StandardGame(int height, int width, Deck deck, TimeSpan timeLimit, TimeSpan turnLimit) : base(timeLimit, turnLimit)
        {
            var populationStrategy = new CyclicRandomPopulationStrategy(GroupSize, deck);
            Board = new Board(height, width, this, populationStrategy);

            Deck = deck;

            RemainingMatches = height * width / GroupSize;

            Board.CellFlipped += OnCellFlipped;
        }

        public override IEnumerable<Board.Cell> TryMatch()
        {
            var reference = Board.FlippedCells.First().Card;
            bool isMatch = Board.FlippedCells.All(c => c.Card == reference);

            IEnumerable<Board.Cell> matched;
            if (isMatch)
            {
                MatchSuccesses++;

                foreach (var cell in Board.FlippedCells) {
                    Board.SetCellKeepRevealed(cell.Row, cell.Column, true);
                }

                RemainingMatches--;
                matched = new List<Board.Cell>(Board.FlippedCells);
            }
            else
            {
                MatchFailures++;
                matched = Enumerable.Empty<Board.Cell>();
            }

            return matched;
        }

        public override bool ShouldTryMatch()
        {
            return Board.FlippedCells.Count > GroupSize - 1;
        }

        public override bool ShouldEndTurn()
        {
            return true;
        }

        public override void EndTurn()
        {
            base.EndTurn();

            Board.UnflipAllCells();
            Board.ReferenceCard = null;
            Turn.Turn++;
        }

        private void OnCellFlipped(Board.Cell cell)
        {
            if (Board.ReferenceCard == null)
            {
                Board.ReferenceCard = cell.Card;
            }
        }
    }
}
