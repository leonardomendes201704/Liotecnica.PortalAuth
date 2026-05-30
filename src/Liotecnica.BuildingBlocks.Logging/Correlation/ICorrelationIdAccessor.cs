namespace Liotecnica.BuildingBlocks.Logging.Correlation;

public interface ICorrelationIdAccessor
{
    string? CorrelationId { get; set; }
}
