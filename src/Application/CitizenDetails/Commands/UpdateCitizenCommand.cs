using System;
using System.Threading;
using System.Threading.Tasks;
using EGID.Common.Exceptions;
using EGID.Common.Models.Files;
using EGID.Domain.Enums;
using EGID.Domain.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EGID.Application.CitizenDetails.Commands
{
    public class UpdateCitizenCommand : IRequest
    {
        public string Id { get; set; }

        public string AccountId { get; set; }

        public string FatherId { get; set; }
        public string MotherId { get; set; }

        public FullName FullName { get; set; }
        public Address Address { get; set; }

        public Gender Gender { get; set; }
        public Religion Religion { get; set; }
        public SocialStatus SocialStatus { get; set; }
        public DateTime DateOfBirth { get; set; }

        public BinaryFile Photo { get; set; }

        #region Validator

        public class UpdateCitizenValidator : AbstractValidator<UpdateCitizenCommand>
        {
            public UpdateCitizenValidator()
            {
                RuleFor(x => x.AccountId).MaximumLength(128);
                RuleFor(x => x.MotherId).MaximumLength(128);
                RuleFor(x => x.FatherId).MaximumLength(128);

                RuleFor(x => x.FullName).NotNull();

                RuleFor(x => x.FullName.FirstName)
                    .MaximumLength(50)
                    .When(x => x.FullName?.FirstName != null);

                RuleFor(x => x.FullName.SecondName)
                    .MaximumLength(50)
                    .When(x => x.FullName?.SecondName != null);

                RuleFor(x => x.FullName.ThirdName)
                    .MaximumLength(50)
                    .When(x => x.FullName?.ThirdName != null);

                RuleFor(x => x.FullName.LastName)
                    .MaximumLength(50)
                    .When(x => x.FullName?.LastName != null);
            }
        }

        #endregion

        #region Handler

        public class UpdateCitizenCommandHandler : IRequestHandler<UpdateCitizenCommand>
        {
            private readonly IEgidDbContext _context;
            private readonly IFilesDirectoryService _directories;

            public UpdateCitizenCommandHandler(IEgidDbContext context, IFilesDirectoryService directories)
            {
                _context = context;
                _directories = directories;
            }

            public async Task<Unit> Handle(UpdateCitizenCommand request, CancellationToken cancellationToken)
            {
                var citizen = await _context.CitizenDetails.FindAsync(request.Id);

                if (citizen == null) throw new EntityNotFoundException("Citizen", request.Id);

                if (request.Photo != null)
                {
                    try
                    {
                        // will override old photo
                        await request.Photo.SaveAsync(_directories.CitizenPhotosDirectory, Guid.Parse(request.Id));
                    }
                    catch (Exception)
                    {
                        throw new FileProcessingException(request.Photo.Name);
                    }
                }

                // update any changes

                if (request.AccountId != null) citizen.CardId = request.AccountId;

                if (request.FatherId != null) citizen.FatherId = request.FatherId;
                if (request.MotherId != null) citizen.MotherId = request.MotherId;

                if (request.FullName != null) citizen.FullName = request.FullName;
                if (request.Address != null) citizen.Address = request.Address;

                if (request.Gender != Gender.None) citizen.Gender = request.Gender;
                if (request.Religion != Religion.None) citizen.Religion = request.Religion;
                if (request.SocialStatus != SocialStatus.None) citizen.SocialStatus = request.SocialStatus;
                if (request.DateOfBirth != default) citizen.DateOfBirth = request.DateOfBirth;

                _context.CitizenDetails.Update(citizen);

                try
                {
                    await _context.SaveChangesAsync(cancellationToken);
                }
                catch (Exception)
                {
                    throw new DbUpdateException();
                }

                return Unit.Value;
            }
        }

        #endregion
    }
}