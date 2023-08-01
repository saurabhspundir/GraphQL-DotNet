using GraphQL.Types;

namespace Metadata.WebAPI.GraphQL
{
    /// <summary>
    /// Schema for GraphQL
    /// </summary>
    public class PropertySchema: Schema
    {
        public PropertySchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<RootQuery>();
        }
    }
}
