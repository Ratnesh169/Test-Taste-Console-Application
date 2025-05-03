using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Test_Taste_Console_Application.Domain.DataTransferObjects;

namespace Test_Taste_Console_Application.Domain.Objects
{
    public class Planet
    {
        //The Id is the name of the planet.
        public string Id { get; set; }
        //The semi major axis is the distance from the planet to its star.
        public float SemiMajorAxis { get; set; }
        //The moons are the moons of the planet.Contains domain Moon objects (not MoonDto objects)
        public ICollection<Moon> Moons { get; set; }
        //The average moon gravity is the average of all the moons of the planet.
        //Currently a placeholder that always returns 0
        // Would normally calculate average gravity of all moons
        public float AverageMoonGravity
        {   
            get => 0.0f;
        }
       
        public float? AverageMoonTemperature
        {
            get
            {
                if (!HasMoons()) return null;

                try
                {
                    // Only average moons that have temperature data
                    var moonsWithTemp = Moons.Where(m => m.AverageTemperature.HasValue);
                    if (!moonsWithTemp.Any()) return null;

                    return moonsWithTemp.Average(m => m.AverageTemperature.Value);
                }
                catch (InvalidOperationException)
                {
                    // Return null if the average calculation fails
                    return null;
                }
            }
        }

        //Constructor that takes a PlanetDto object and creates a Planet object
        //Safely handles null moon collections
        //Converts each MoonDto to domain Moon objects
        public Planet(PlanetDto planetDto)
        {
            Id = planetDto.Id;
            SemiMajorAxis = planetDto.SemiMajorAxis;
            Moons = new Collection<Moon>();
            if(planetDto.Moons != null)
            {
                foreach (MoonDto moonDto in planetDto.Moons)
                {
                    Moons.Add(new Moon(moonDto));
                }
            }
        }

        //Helper method to check for moons
        public Boolean HasMoons()
        {
            return (Moons != null && Moons.Count > 0);
        }
    }
}
