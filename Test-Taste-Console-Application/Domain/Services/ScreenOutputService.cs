using System;
using System.Linq;
using Test_Taste_Console_Application.Constants;
using Test_Taste_Console_Application.Domain.Objects;
using Test_Taste_Console_Application.Domain.Services.Interfaces;
using Test_Taste_Console_Application.Utilities;

namespace Test_Taste_Console_Application.Domain.Services
{
    /// <inheritdoc />
    /// This class implements the IOutputService interface to display solar system data in a formatted console output.
    /// It provides three main display functions for different views of the planetary data.
    public class ScreenOutputService : IOutputService
    {
        /// The class uses the IPlanetService and IMoonService to get the data from the API.
        private readonly IPlanetService _planetService;
        /// The class uses the IPlanetService and IMoonService to get the data from the API.
        private readonly IMoonService _moonService;
        /// The class uses the ConsoleWriter class to create the output.
        public ScreenOutputService(IPlanetService planetService, IMoonService moonService)
        {
            _planetService = planetService;
            _moonService = moonService;
        }
        /// The function outputs all the planets and their moons to the console.
        public void OutputAllPlanetsAndTheirMoonsToConsole()
        {
            //The service gets all the planets from the API.
            var planets = _planetService.GetAllPlanets().ToArray();

            //If the planets aren't found, then the function stops and tells that to the user via the console.
            if (!planets.Any())
            {
                Console.WriteLine(OutputString.NoPlanetsFound);
                return;
            }
            //The function creates a header for the output.
            //The column sizes and labels for the planets are configured here. 
            var columnSizesForPlanets = new[] { 20, 20, 30, 20 };
            var columnLabelsForPlanets = new[]
            {
                OutputString.PlanetNumber, OutputString.PlanetId, OutputString.PlanetSemiMajorAxis,
                OutputString.TotalMoons
            };
            // The function creates a header for the output.
            //The column sizes and labels for the moons are configured here.
            //The second moon's column needs the 2 extra '-' characters so that it's aligned with the planet's column.
            var columnSizesForMoons = new[] { 20, 70 + 2 };
            var columnLabelsForMoons = new[]
            {
                OutputString.MoonNumber, OutputString.MoonId
            };

            //The for loop creates the correct output.
            for (int i = 0, j = 1; i < planets.Length; i++, j++)
            {
                //First the line is created.
                ConsoleWriter.CreateLine(columnSizesForPlanets);

                //Under the line the header is created.
                ConsoleWriter.CreateText(columnLabelsForPlanets, columnSizesForPlanets);

                //Under the header the planet data is shown.
                ConsoleWriter.CreateText(
                    new[]
                    {
                        j.ToString(), CultureInfoUtility.TextInfo.ToTitleCase(planets[i].Id),
                        planets[i].SemiMajorAxis.ToString(),
                        planets[i].Moons.Count.ToString()
                    },
                    columnSizesForPlanets);

                //Under the planet data the header for the moons is created.
                ConsoleWriter.CreateLine(columnSizesForPlanets);
                ConsoleWriter.CreateText(columnLabelsForMoons, columnSizesForMoons);

                //The for loop creates the correct output.
                for (int k = 0, l = 1; k < planets[i].Moons.Count; k++, l++)
                {
         
                    var moonId = planets[i].Moons.ElementAt(k).Id;
                    ConsoleWriter.CreateText(
                        new[]
                        {
                            l.ToString(), moonId != null ? CultureInfoUtility.TextInfo.ToTitleCase(moonId) : "Unknown Moon"
                        },
                        columnSizesForMoons);
                }

                //Under the data the footer is created.
                ConsoleWriter.CreateLine(columnSizesForMoons);
                ConsoleWriter.CreateEmptyLines(2);

                /*
                    This is an example of the output for the planet Earth:
                    --------------------+--------------------+------------------------------+--------------------
                    Planet's Number     |Planet's Id         |Planet's Semi-Major Axis      |Total Moons
                    10                  |Terre               |0                             |1
                    --------------------+--------------------+------------------------------+--------------------
                    Moon's Number       |Moon's Id
                    1                   |La Lune
                    --------------------+------------------------------------------------------------------------
                */
            }
        }

        public void OutputAllMoonsAndTheirMassToConsole()
        {
            //The function works the same way as the PrintAllPlanetsAndTheirMoonsToConsole function. You can find more comments there.
            var moons = _moonService.GetAllMoons().ToArray();

            if (!moons.Any())
            {
                Console.WriteLine(OutputString.NoMoonsFound);
                return;
            }

            var columnSizesForMoons = new[] { 20, 20, 30, 20 };
            var columnLabelsForMoons = new[]
            {
                OutputString.MoonNumber, OutputString.MoonId, OutputString.MoonMassExponent, OutputString.MoonMassValue
            };

            ConsoleWriter.CreateHeader(columnLabelsForMoons, columnSizesForMoons);

            for (int i = 0, j = 1; i < moons.Length; i++, j++)
            {
                ConsoleWriter.CreateText(
                    new[]
                    {
                        j.ToString(), CultureInfoUtility.TextInfo.ToTitleCase(moons[i].Id),
                        moons[i].MassExponent.ToString(), moons[i].MassValue.ToString()
                    },
                    columnSizesForMoons);
            }

            ConsoleWriter.CreateLine(columnSizesForMoons);
            ConsoleWriter.CreateEmptyLines(2);

            /*
                This is an example of the output for the moon around the earth:
                --------------------+--------------------+------------------------------+--------------------
                Moon's Number       |Moon's Id           |Moon's Mass Exponent          |Moon's Mass Value
                --------------------+--------------------+------------------------------+--------------------
                1                   |Lune                |22                            |7,346             
                ...more data...
                --------------------+--------------------+------------------------------+--------------------
             */
        }

        public void OutputAllPlanetsAndTheirAverageMoonGravityToConsole()
        {
            //The function works the same way as the PrintAllPlanetsAndTheirMoonsToConsole function. You can find more comments there.
            var planets = _planetService.GetAllPlanets().ToArray();
            if (!planets.Any())
            {
                Console.WriteLine(OutputString.NoMoonsFound);
                return;
            }

            var columnSizes = new[] { 20, 30 };
            var columnLabels = new[]
            {
                OutputString.PlanetId, OutputString.PlanetMoonAverageGravity
            };


            ConsoleWriter.CreateHeader(columnLabels, columnSizes);

            foreach (Planet planet in planets)
            {
                if (planet.HasMoons())
                {
                    ConsoleWriter.CreateText(new string[] { $"{planet.Id}", $"{planet.AverageMoonGravity}" }, columnSizes);
                }
                else
                {
                    ConsoleWriter.CreateText(new string[] { $"{planet.Id}", $"-" }, columnSizes);
                }
            }

            ConsoleWriter.CreateLine(columnSizes);
            ConsoleWriter.CreateEmptyLines(2);

            /*
                --------------------+--------------------------------------------------
                Planet's Number     |Planet's Average Moon Gravity
                --------------------+--------------------------------------------------
                1                   |0.0f
                --------------------+--------------------------------------------------
            */
        }



        public void OutputAllPlanetsAndTheirAverageMoonTemperatureToConsole()
        {
            Console.WriteLine("Loading planetary data with moon temperatures...");
            var planets = _planetService.GetAllPlanets()
                .Where(p => p.HasMoons())
                .ToArray();

            if (!planets.Any())
            {
                Console.WriteLine(OutputString.NoPlanetsWithMoonsFound);
                return;
            }

            Console.WriteLine("Calculating average moon temperatures...");
            var columnSizes = new[] { 25, 40 };
            var columnLabels = new[]
            {
        OutputString.PlanetId,
        OutputString.PlanetMoonAverageTemperature
    };

            ConsoleWriter.CreateHeader(columnLabels, columnSizes);

            foreach (Planet planet in planets)
            {
                // Handle cases where temperature is 0 (meaning no data)
                string temperatureDisplay = (planet.AverageMoonTemperature.HasValue && planet.AverageMoonTemperature.Value != 0)
                    ? $"{planet.AverageMoonTemperature:F2}°K"
                    : "No temperature data";

                ConsoleWriter.CreateText(
                    new string[] {
                CultureInfoUtility.TextInfo.ToTitleCase(planet.Id),
                temperatureDisplay
                    },
                    columnSizes);
            }

            ConsoleWriter.CreateLine(columnSizes);
            ConsoleWriter.CreateEmptyLines(2);

            // Add statistics about temperature data availability
            var planetsWithValidTemp = planets.Count(p =>
                p.AverageMoonTemperature.HasValue &&
                p.AverageMoonTemperature.Value != 0);

            Console.WriteLine($"Temperature data available for {planetsWithValidTemp} of {planets.Length} planets with moons.");
            
        }


}
}
