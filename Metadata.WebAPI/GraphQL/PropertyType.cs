using GraphQL.Types;
using Metadata.WebAPI.GraphQL.Models;

namespace Metadata.WebAPI.GraphQL
{
    /// <summary>
    /// Type definition of GraphQL
    /// </summary>
    public sealed class PropertyType:ObjectGraphType<Property>
    {
        public PropertyType()
        {
            Name = "Property";
            Description = "Property With Features flag";
            Field(d => d.PropertyState, nullable: false).Description("Features").Argument(typeof(StringGraphType),"propertyState");
            Field(d => d.Features, nullable: true).Description("Features");
        }
    }
}
