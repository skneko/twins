﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twins.Components;
using Twins.Models;

namespace Twins.ViewModels
{
    public class BoardViewModel : ViewModelBase
    {
        public class CardComponentMatrix
        {
            readonly CardComponent[,] cardComponents;

            public CardComponentMatrix(int height, int width)
            {
                cardComponents = new CardComponent[height, width];
            }

            public CardComponent this[Board.Cell cell] {
                get => cardComponents[cell.Row, cell.Column];
                set => cardComponents[cell.Row, cell.Column] = value;
            }
                
        }
        
        public CardComponentMatrix CardComponents { get; }

        public Board Board { get; }

        public bool InteractionAllowed { get; private set; }

        public BoardViewModel(Board board) {
            Board = board;

            CardComponents = new CardComponentMatrix(Board.Height, Board.Width);
            foreach (var cell in Board.Cells)
            {
                var component = new CardComponent(cell.Card);
                component.Clicked += () => OnCellClicked(cell);
                CardComponents[cell] = component;
            }

            Board.CellFlipped += OnCellFlipped;
            Board.CellUnflipped += OnCellUnflipped;
            Board.CellKeepRevealedStatusChanged += OnCellKeepRevealedStatusChanged;

            InteractionAllowed = true;
        }

        private async void OnCellsMatched(IEnumerable<Board.Cell> cells)
        {
            foreach (var cell in cells)
            {
               await CardComponents[cell].Matched();
            }
        }

        async void OnCellFlipped(Board.Cell cell)
        {
            await CardComponents[cell].Flip();
        }

        async void OnCellUnflipped(Board.Cell cell)
        {
            await CardComponents[cell].Unflip();
        }

        async void OnCellKeepRevealedStatusChanged(Board.Cell cell, bool reveal)
        {
            if (reveal)
            {
                await CardComponents[cell].Flip();
            }
            else
            {
                await CardComponents[cell].Unflip();
            }
        }

        async void OnCellClicked(Board.Cell cell)
        {
            if (!InteractionAllowed) return;

            try
            {
                Board.FlipCell(cell.Row, cell.Column);
            }
            catch (InvalidOperationException) { }

            if (Board.Game.ShouldTryMatch())
            {
                InteractionAllowed = false;

                var matched = Board.Game.TryMatch();
                if (matched.Any())
                {
                    OnCellsMatched(matched);
                    await Task.Delay(1500);
                }
                else
                {
                    await Task.Delay(1000);
                }

                if (Board.Game.ShouldEndTurn())
                {
                    Board.Game.EndTurn();
                }

                InteractionAllowed = true;
            }
        }
    }
}