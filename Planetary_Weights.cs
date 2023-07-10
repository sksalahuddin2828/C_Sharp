using System;

class PlanetaryWeights
{
    const double MERCURY_GRAVITY = 0.376;
    const double VENUS_GRAVITY = 0.889;
    const double MARS_GRAVITY = 0.378;
    const double JUPITER_GRAVITY = 2.36;
    const double SATURN_GRAVITY = 1.081;
    const double URANUS_GRAVITY = 0.815;
    const double NEPTUNE_GRAVITY = 1.14;

    static void Main()
    {
        Console.Write("Enter a weight on Earth: ");
        double earthWeight = double.Parse(Console.ReadLine());

        Console.Write("Enter a planet: ");
        string planet = Console.ReadLine().ToLower().ToUpperFirstChar();

        while (!IsValidPlanet(planet))
        {
            if (planet == "Earth")
            {
                Console.WriteLine("Please select a planet other than Earth.");
            }
            else
            {
                Console.WriteLine("Error: {0} is not a planet.", planet);
            }

            Console.Write("Enter a planet: ");
            planet = Console.ReadLine().ToLower().ToUpperFirstChar();
        }

        double planetWeight;

        switch (planet)
        {
            case "Mercury":
                planetWeight = earthWeight * MERCURY_GRAVITY;
                break;
            case "Venus":
                planetWeight = earthWeight * VENUS_GRAVITY;
                break;
            case "Mars":
                planetWeight = earthWeight * MARS_GRAVITY;
                break;
            case "Jupiter":
                planetWeight = earthWeight * JUPITER_GRAVITY;
                break;
            case "Saturn":
                planetWeight = earthWeight * SATURN_GRAVITY;
                break;
            case "Uranus":
                planetWeight = earthWeight * URANUS_GRAVITY;
                break;
            default:
                planetWeight = earthWeight * NEPTUNE_GRAVITY;
                break;
        }

        double roundedWeight = Math.Round(planetWeight, 2);

        Console.WriteLine("The equivalent weight on {0}: {1}", planet, roundedWeight);
    }

    static bool IsValidPlanet(string planet)
    {
        string[] validPlanets = { "Mercury", "Venus", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
        return Array.Exists(validPlanets, p => p.Equals(planet, StringComparison.OrdinalIgnoreCase));
    }
}

static class StringExtensions
{
    public static string ToUpperFirstChar(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToUpper(input[0]) + input.Substring(1);
    }
}
