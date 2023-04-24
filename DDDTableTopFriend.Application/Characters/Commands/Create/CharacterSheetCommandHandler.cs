using DDDTableTopFriend.Application.Characters.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Characters.Commands.Create;

public class CharacterSheetCommandHandler : IRequestHandler<CreateCharacterCommand, ErrorOr<CharacterResult>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;

    public CharacterSheetCommandHandler(
        ICharacterRepository characterRepository,
        ISkillRepository skillRepository,
        IStatusRepository statusRepository,
        ICachingService cachingService,
        IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _skillRepository = skillRepository;
        _statusRepository = statusRepository;
        _cachingService = cachingService;
        _unitOfWork = unitOfWork;
    }

    public Task<ErrorOr<CharacterResult>> Handle(
        CreateCharacterCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
