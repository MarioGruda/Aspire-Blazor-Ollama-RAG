using System.ComponentModel.DataAnnotations;

namespace Gruda.RagBot.Kernel;

public class KernelOptions
{
	[Required] public required string ChatModelId { get; set; }

	[Required] public required string TextEmbeddingModelId { get; set; }

	[Required] public required int TextEmbeddingVectorSize { get; set; }
}