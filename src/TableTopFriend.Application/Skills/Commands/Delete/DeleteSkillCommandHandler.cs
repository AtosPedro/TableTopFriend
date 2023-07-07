using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Application.Skills.Common;
using TableTopFriend.Domain.AggregateSkill.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Skills.Commands.Delete;

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, ErrorOr<bool>>
{
    private readonly ISkillRepository _skillRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteSkillCommandHandler(
        ISkillRepository skillRepository,
        IDateTimeProvider dateTimeProvider,
        ICachingService cachingService,
        IUnitOfWork unitOfWork)
    {
        _skillRepository = skillRepository;
        _dateTimeProvider = dateTimeProvider;
        _cachingService = cachingService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<bool>> Handle(
        DeleteSkillCommand request,
        CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetById(SkillId.Create(request.SkillId), cancellationToken);
        if (skill is null)
            return Errors.Skill.NotRegistered;

        skill.MarkToDelete(_dateTimeProvider.UtcNow);

        return await _unitOfWork.Execute(async _ =>
        {
            await _skillRepository.Remove(skill);
            await _cachingService.RemoveCacheValueAsync<SkillResult>(skill.Id.Value.ToString());
            return skill is not null;
        },
        cancellationToken);
    }
}
