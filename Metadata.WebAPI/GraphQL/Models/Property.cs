using GraphQL;

namespace Metadata.WebAPI.GraphQL.Models;

public class Property
{
    [Ignore]
    public string PropertyState { get; set; } = string.Empty;
    [Ignore]
    public bool IncludeInCalculation { get; set; }
    [Ignore]
    public bool AlwaysRequired { get; set; }
    [Ignore]
    public bool IncludeInSearch { get; set; }
    public List<Feature> Features { get; set; }=new List<Feature>();

}
