﻿using System;
using Twins.Components;
using Twins.Models;
using Twins.Utils;
using Twins.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardView : ContentPage
    {
        public BoardView(Board board)
        {
            InitializeComponent();
            BoardViewModel boardViewModel = new BoardViewModel(board);
            BindingContext = boardViewModel;
            PauseMenu.BindingContext = boardViewModel;

            turnLabel.SetBinding(Label.TextProperty, "Value");
            turnLabel.BindingContext = boardViewModel.Board.Game.Turn;

            globalTimeLabel.SetBinding(Label.TextProperty, "Time");
            globalTimeLabel.BindingContext = boardViewModel.Board.Game.GameClock.TimeLeft;

            turnTimeLabel.SetBinding(Label.TextProperty, "Time");
            turnTimeLabel.SetBinding(Label.TextColorProperty, "Color");
            turnTimeLabel.BindingContext = boardViewModel.Board.Game.TurnClock.TimeLeft;

            successLabel.SetBinding(Label.TextProperty, "Value");
            successLabel.BindingContext = boardViewModel.Board.Game.MatchSuccesses;

            board.Game.Score.Changed += (_) =>
            {
                scoreLabel.Text = board.Game.Score.PositiveValue.ToString();
            };
            scoreLabel.Text = board.Game.Score.PositiveValue.ToString();

            turn2PointLabel.SetBinding(Label.TextColorProperty, "Color");
            turn2PointLabel.BindingContext = boardViewModel.Board.Game.TurnClock.TimeLeft;

            turnTextLabel.SetBinding(Label.TextColorProperty, "Color");
            turnTextLabel.BindingContext = boardViewModel.Board.Game.TurnClock.TimeLeft;

            board.Game.GameEnded += OnGameEnded;

            referenceCard.Clicked += () => { };

            FillBoard(board.Height, board.Width);
        }

        protected override void OnAppearing()
        {
            var board = ((BoardViewModel)BindingContext).Board;
            board.ReferenceCardChanged += OnReferenceCardChanged;
            OnReferenceCardChanged(board.ReferenceCard);
        }

        private void OnGameEnded(bool victory)
        {
            Game game = ((BoardViewModel)BindingContext).Board.Game;
            EndGameModal.SetStadistics(
                game.Score.PositiveValue,
                game.GameClock.GetTimeSpan(),
                victory, game.ResultOfGame);
            EndGameModal.IsVisible = true;
        }

        private void FillBoard(int height, int width)
        {

            for (int i = 0; i < height; i++)
            {
                board.RowDefinitions.Add(new RowDefinition { Height = new GridLength(120) });
            }
            for (int i = 0; i < width; i++)
            {
                board.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            }

            BoardViewModel viewModel = (BoardViewModel)BindingContext;
            foreach (Board.Cell cell in viewModel.Board.Cells)
            {
                CardComponent card = viewModel.CardComponents[cell];
                card.VerticalOptions = LayoutOptions.Center;
                card.HorizontalOptions = LayoutOptions.Center;
                board.Children.Add(card, cell.Row, cell.Column);
            }
            boardArea.WidthRequest = 122 * height;
            boardArea.HeightRequest = 122 *  width;
            board.WidthRequest = 122 * height;
            board.HeightRequest = 122 *  width;

        }

        private async void OnReferenceCardChanged(Card card)
        {
            if (card != null)
            {
                referenceCardFrame.IsVisible = true;
                referenceCard.Card = card;
                if (!referenceCard.Flipped)
                {
                    await referenceCard.Flip();
                }
            }
            else
            {
                if (referenceCard.Flipped)
                {
                    await referenceCard.Unflip();
                }
            }
        }

        private void OnPause(object sender, EventArgs e)
        {
            ((BoardViewModel)BindingContext).Board.Game.Pause();
            PauseMenu.OnPause();
        }

        private void OnMute(object sender, EventArgs e)
        {
            var player = new AudioPlayer();
            if (player.GetVolume() == 0.0) { player.ChangeVolume(100.0); }
            else { player.ChangeVolume(0.0); }
        }

        public ResumeGameView GetResumeGameView() 
        {
            return EndGameModal;
        }
    }
}