using Gruda.RagBot.Aspire.Ollama;

const string completionModel = "gemma2";
const string embeddingModel = "nomic-embed-text";
const int textEmbeddingVectorSize = 768;

var builder = DistributedApplication.CreateBuilder(args);

var qdrant = builder.AddQdrant("qdrant")
    .WithDataVolume("qdrant-data");

var ollama = builder.AddOllama("ollama")
    .AddModel(completionModel)
    .AddModel(embeddingModel)
    .WithDataVolume("ollama")
    .WithEnvironment("OLLAMA_MAX_LOADED_MODELS", "2")
    .WithEnvironment("OLLAMA_NUM_PARALLEL", "2");

builder.AddProject<Projects.Gruda_RagBot_Host>("RagBot")
    .WithReference(qdrant)
    .WithReference(ollama)
    .WithEnvironment("KernelOptions__ChatModelId", completionModel)
    .WithEnvironment("KernelOptions__TextEmbeddingModelId", embeddingModel)
    .WithEnvironment("KernelOptions__TextEmbeddingVectorSize", textEmbeddingVectorSize.ToString());

builder.Build().Run();