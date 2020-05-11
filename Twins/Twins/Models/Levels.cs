﻿using System;
using Twins.Components;
using Twins.Models.Builders;

namespace Twins.Models
{
    public static class Levels
    {
        public static GameBuilder Level1 { get; } = new GameBuilder(3, 2)
                                            .OfKind(GameBuilder.GameKind.Standard)
                                            .WithTimeLimit(TimeSpan.FromMinutes(2))
                                            .WithTurnTimeLimit(TimeSpan.FromSeconds(10))
                                            .WithDeck(BasicDeck.Numeros);

        public static GameBuilder Level2 { get; } = new GameBuilder(4, 2)
                                            .OfKind(GameBuilder.GameKind.Standard)
                                            .WithTimeLimit(TimeSpan.FromMinutes(2))
                                            .WithTurnTimeLimit(TimeSpan.FromSeconds(10))
                                            .WithDeck(BasicDeck.Numeros);

        public static GameBuilder Level3 { get; } = new GameBuilder(4, 3)
                                            .OfKind(GameBuilder.GameKind.Standard)
                                            .WithTimeLimit(TimeSpan.FromMinutes(2))
                                            .WithTurnTimeLimit(TimeSpan.FromSeconds(10))
                                            .WithDeck(BasicDeck.Numeros);

        public static GameBuilder Level4 { get; } = new GameBuilder(4, 4)
                                            .OfKind(GameBuilder.GameKind.Standard)
                                            .WithTimeLimit(TimeSpan.FromMinutes(2))
                                            .WithTurnTimeLimit(TimeSpan.FromSeconds(10))
                                            .WithDeck(BasicDeck.Deportes);

        public static GameBuilder Level5 { get; } = new GameBuilder(5, 4)
                                            .OfKind(GameBuilder.GameKind.Standard)
                                            .WithTimeLimit(TimeSpan.FromMinutes(2))
                                            .WithTurnTimeLimit(TimeSpan.FromSeconds(10))
                                            .WithDeck(BasicDeck.Deportes);

        public static GameBuilder Level6 { get; } = new GameBuilder(6, 4)
                                            .OfKind(GameBuilder.GameKind.Standard)
                                            .WithTimeLimit(TimeSpan.FromMinutes(2))
                                            .WithTurnTimeLimit(TimeSpan.FromSeconds(10))
                                            .WithDeck(BasicDeck.Deportes);

        public static GameBuilder Level7 { get; } = new GameBuilder(6, 4)
                                            .OfKind(GameBuilder.GameKind.Standard)
                                            .WithTimeLimit(new TimeSpan(0, 1, 30))
                                            .WithTurnTimeLimit(TimeSpan.FromSeconds(10))
                                            .WithDeck(BasicDeck.Animales);

        public static GameBuilder Level8 { get; } = new GameBuilder(6, 4)
                                            .OfKind(GameBuilder.GameKind.ReferenceCard)
                                            .WithTimeLimit(new TimeSpan(0, 1, 30))
                                            .WithTurnTimeLimit(TimeSpan.FromSeconds(10))
                                            .WithDeck(BasicDeck.Animales);

        public static GameBuilder Level9 { get; } = new GameBuilder(6, 4)
                                            .OfKind(GameBuilder.GameKind.ReferenceCard)
                                            .WithTimeLimit(new TimeSpan(0, 1, 30))
                                            .WithTurnTimeLimit(TimeSpan.FromSeconds(10))
                                            .WithDeck(BasicDeck.Animales);

        public static GameBuilder Level10 { get; } = new GameBuilder(6, 4)
                                            .OfKind(GameBuilder.GameKind.ReferenceCard)
                                            .WithTimeLimit(TimeSpan.FromMinutes(1))
                                            .WithTurnTimeLimit(TimeSpan.FromSeconds(10))
                                            .WithDeck(BasicDeck.Animales);
    }
}
