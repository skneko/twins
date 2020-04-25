﻿using System;
using System.Linq;
using Twins.Components;
using Twins.Models;
//using Twins.Views.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardView : ContentPage
    {
        private bool IsMuted { get; set; }
        private double UsableBoardAreaSize { get { return Math.Min(boardArea.Width, boardArea.Height); } }

        public BoardView()
        {
            IsMuted = false;
            InitializeComponent();

            //boardArea.LayoutChanged += EnforceBoardAspectRatio;   
        }

        private void EnforceBoardAspectRatio(object sender = null, EventArgs e = null)
        {
            if (UsableBoardAreaSize > 0) {
                var usableWidth = boardArea.Width;
                var usableHeight = boardArea.Height;

                var columns = board.ColumnDefinitions.Count;
                var rows = board.RowDefinitions.Count;

                var cellSide = Math.Min(usableHeight / rows, usableWidth / columns);

                board.WidthRequest = cellSide * columns;
                board.HeightRequest = cellSide * rows;
                InvalidateMeasure();
            }
        }

        private void OnPause(object sender, EventArgs e)
        {
            PauseView.OnPause();
        }

        private void OnMute(object sender, EventArgs e)
        {
            if(!IsMuted)
                MuteButton.ImageSource=ImageSource.FromFile("Assets/Icons/mute.png");
            else
                MuteButton.ImageSource = ImageSource.FromFile("Assets/Icons/volume.png");
            IsMuted = !IsMuted;
        }
    }
}