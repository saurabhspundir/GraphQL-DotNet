using GraphQL;
using GraphQL.Types;
using Metadata.WebAPI.GraphQL.Models;

namespace Metadata.WebAPI.GraphQL
{
    /// <summary>
    /// Query for GraphQL. This provide data. this can be integrated with data layer
    /// </summary>
    public sealed class RootQuery : ObjectGraphType
    {
    readonly List<Property> _propertyValues = new()
        {
            new()
            {
                IncludeInCalculation = true,
                PropertyState = "CA",
                IncludeInSearch = false,
                AlwaysRequired = true,
                Features = new()
                {
                    new()
                    {
                        IdentificationKey = Guid.NewGuid(),
                        PointFeature = PointFeature.Always,
                        Relationship = Relationship.Known
                    },
                    new()
                    {
                        IdentificationKey = Guid.NewGuid(),
                        PointFeature = PointFeature.FeatureOnly,
                        Relationship = Relationship.UnKnown
                    },
                    new()
                    {
                        IdentificationKey = Guid.NewGuid(),
                        PointFeature = PointFeature.PointsOnly,
                        Relationship = Relationship.SecondDegree
                    }
                }
            },
            new()
            {
                IncludeInCalculation = true,
                PropertyState = "NY",
                IncludeInSearch = false,
                AlwaysRequired = true,
                Features = new()
                {
                    new()
                    {
                        IdentificationKey = Guid.NewGuid(),
                        PointFeature = PointFeature.Always,
                        Relationship = Relationship.Known
                    },
                    new()
                    {
                        IdentificationKey = Guid.NewGuid(),
                        PointFeature = PointFeature.FeatureOnly,
                        Relationship = Relationship.ThirdDegree
                    },
                    new()
                    {
                        IdentificationKey = Guid.NewGuid(),
                        PointFeature = PointFeature.PointsOnly,
                        Relationship = Relationship.SecondDegree
                    }
                    ,
                    new()
                    {
                        IdentificationKey = Guid.NewGuid(),
                        PointFeature = PointFeature.PointsOnly,
                        Relationship = Relationship.SecondDegree
                    }
                }
            }
        };
    public RootQuery()
        {
            Field<ListGraphType<PropertyType>>("property")
                .Argument<StringGraphType>("propertyState").Description("property state")
                .Resolve(context =>
                {
                    var state = context.GetArgument<string>("propertyState");
                    return _propertyValues.Where(property =>
                        property.PropertyState.Equals(state, StringComparison.InvariantCultureIgnoreCase));
                });
        }
    }
}
