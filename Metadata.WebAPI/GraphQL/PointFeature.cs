using GraphQL.Types;

namespace Metadata.WebAPI.GraphQL
{
    [CamelCase]
    public enum PointFeature
    {
        None = 0,
        Always = 1,
        PointsOnly = 2,
        FeatureOnly = 3
    }
}
