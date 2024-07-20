using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Plugins.Memory;

#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0050

namespace Gruda.RagBot.Kernel;

public class KernelService(Microsoft.SemanticKernel.Kernel kernel)
{
	private readonly KernelFunction _function = kernel.CreateFunctionFromPrompt(Constants.Prompt);

	public IAsyncEnumerable<StreamingKernelContent> GetResponseStreamed(string userInput, ChatHistory history)
	{
		string historyAsString = string.Join(Environment.NewLine + Environment.NewLine, history);
		var arguments = new KernelArguments()
		{
			["history"] = historyAsString, // todo add history to prompt
			["userInput"] = userInput,
			[TextMemoryPlugin.CollectionParam] = Constants.MemoryCollectionName,
			[TextMemoryPlugin.LimitParam] = "3",
			[TextMemoryPlugin.RelevanceParam] = "0.70"
		};

		return _function.InvokeStreamingAsync(kernel, arguments);
	}

	public async Task<string?> CreateTextEmbedding(string text)
	{
		ISemanticTextMemory textMemory = kernel.GetRequiredService<ISemanticTextMemory>();
		string embeddings = await textMemory.SaveInformationAsync(
			collection: Constants.MemoryCollectionName,
			text: text,
			id: Guid.NewGuid().ToString());

		return embeddings;
	}
}