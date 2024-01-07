namespace LearningHub.Domain.MessageQueueLabs.Mappings;

[UsedImplicitly]
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<OperationLog, OperationLogDto>().ReverseMap();
    }
}