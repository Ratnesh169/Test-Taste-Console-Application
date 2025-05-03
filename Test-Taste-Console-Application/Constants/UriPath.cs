namespace Test_Taste_Console_Application.Constants
{
    public static class UriPath
    {
        public const string BaseUri = "https://api.le-systeme-solaire.net";
        private const string BodiesUri = "/rest/bodies";

        // Modified to include avgTemp in planet query
        public const string GetAllPlanetsWithMoonsQueryParameters =
            BodiesUri + "?data=id,semiMajorAxis,moons,moon,rel,avgTemp&filter[]=isPlanet,neq,false";

        // Modified to include avgTemp in moon query
        public const string GetAllMoonsWithMassQueryParameters =
            BodiesUri + "?data=id,mass,massValue,massExponent,avgTemp&filter[]=aroundPlanet,gt,null";

        // Modified base path to support additional parameters
        public const string GetMoonByIdQueryParameters = BodiesUri + "/?data=id,massValue,massExponent,avgTemp";
    }
}