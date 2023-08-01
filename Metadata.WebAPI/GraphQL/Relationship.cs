using GraphQL.Types;

namespace Metadata.WebAPI.GraphQL
{
    [CamelCase]
    public enum Relationship
    {
        UnKnown = 0,
        Known = 1,
        SecondDegree = 2,
        ThirdDegree = 3
    }
}
