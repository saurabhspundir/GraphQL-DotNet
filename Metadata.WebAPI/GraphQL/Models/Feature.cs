namespace Metadata.WebAPI.GraphQL.Models;

public class Feature
{
    public Guid IdentificationKey { get; set; }= Guid.Empty;

    public Relationship Relationship { get; set; } = Relationship.UnKnown;

    public PointFeature PointFeature { get; set; } = PointFeature.None;

}
