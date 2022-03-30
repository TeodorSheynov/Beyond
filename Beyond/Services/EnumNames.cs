using System;
using System.Collections.Generic;
using Beyond.Data.Models.Enums;
using Beyond.Models.Control;
using Beyond.Services.Interfaces;

namespace Beyond.Services
{
    public class EnumNames : IEnumNames
    {
        public List<PilotRanksViewModel> EnumRankNames()
        {
            var decoratedRanks=new List<PilotRanksViewModel>();
            foreach (int rankName in Enum.GetValues(typeof(Rank)))
            {
                var name = Enum.GetName(typeof(Rank), rankName);
                var model= new PilotRanksViewModel()
                {
                    Name = UiRankDecorator(name),
                    Value = rankName
                };
                decoratedRanks.Add(model);
            }

            return decoratedRanks;
        }

        public string UiRankDecorator(string rank)
        {
            return rank switch
            {
                "TrainingCaptain" => "Training Captain",
                "Captain" => "Captain",
                "SeniorFirstOfficer" => "Senior First Officer",
                "SecondOfficer" => "Second Officer",
                "CadetTrainee" => "Cadet/Trainee",
                _ => throw new ArgumentException("Unknown rank.")
            };
        }
    }
}
