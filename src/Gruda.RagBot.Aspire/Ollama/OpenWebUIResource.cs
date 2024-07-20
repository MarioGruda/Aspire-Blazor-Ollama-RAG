namespace Gruda.RagBot.Aspire.Ollama;

public class OpenWebUIResource : ContainerResource, IResourceWithConnectionString
{
	const string PrimaryEndpointName = "http";

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenWebUIResource"/> class.
    /// </summary>
    /// <param name="name">The name of the resource.</param>
    public OpenWebUIResource(string name) : base(name)
    {
    }

    private EndpointReference? _primaryEndpoint;

    /// <summary>
    /// Gets the http endpoint for the Open WebUI resource.
    /// </summary>
    public EndpointReference PrimaryEndpoint => _primaryEndpoint ??= new(this, PrimaryEndpointName);

    /// <summary>
    /// Gets the connection string expression for the Open WebUI endpoint.
    /// </summary>
    public ReferenceExpression ConnectionStringExpression =>
        ReferenceExpression.Create(
            $"{PrimaryEndpoint.Property(EndpointProperty.Url)}");
}