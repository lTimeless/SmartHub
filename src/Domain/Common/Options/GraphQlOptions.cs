using HotChocolate.Execution.Options;
using HotChocolate.Types.Pagination;
using System.ComponentModel.DataAnnotations;

namespace SmartHub.Domain.Common.Options
{
	public class GraphQlOptions
	{
		[Required] public int MaxAllowedComplexity { get; set; }

		[Required] public int MaxAllowedExecutionDepth { get; set; }

		[Required] public PagingOptions Paging { get; set; } = default!;

		[Required] public RequestExecutorOptions Request { get; set; } = default!;
	}
}