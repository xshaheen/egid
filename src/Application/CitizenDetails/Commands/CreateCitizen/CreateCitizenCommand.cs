using System;
using System.Threading;
using System.Threading.Tasks;
using EGID.Common.Exceptions;
using EGID.Common.Models.File;
using EGID.Domain.Entities;
using EGID.Domain.Enums;
using EGID.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EGID.Application.CitizenDetails.Commands.CreateCitizen
{
    public class CreateCitizenCommand : IRequest<string>
    {
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

        public HealthInfo HealthInfo { get; set; }

        #region Handler 

        public class CreateCitizenCommandHandler : IRequestHandler<CreateCitizenCommand, string>
        {
            private readonly IEgidDbContext _context;
            private readonly IFilesDirectoryService _directories;

            public CreateCitizenCommandHandler(IEgidDbContext context, IFilesDirectoryService directories)
            {
                _context = context;
                _directories = directories;
            }

            public async Task<string> Handle(CreateCitizenCommand request, CancellationToken cancellationToken)
            {
                var id = Guid.NewGuid();

                try
                { 
                    await request.Photo.SaveAsync(_directories.CitizenPhotosDirectory, id);
                }
                catch (Exception)
                {
                    throw new FileProcessingException(request.Photo.Name);
                }

                var citizen = new CitizenDetail
                {
                    Id = id.ToString(),
                    CardId = request.AccountId,
                    FatherId = request.FatherId,
                    MotherId = request.MotherId,
                    FullName = request.FullName,
                    Address = request.Address,
                    Gender = request.Gender,
                    Religion = request.Religion,
                    SocialStatus = request.SocialStatus,
                    DateOfBirth = request.DateOfBirth,
                    PhotoUrl = id.ToString()
                };

                await _context.CitizenDetails.AddAsync(citizen, cancellationToken);

                try
                {
                    await _context.SaveChangesAsync(cancellationToken);
                }
                catch (Exception)
                {
                    throw new DbUpdateException();
                }

                return citizen.Id;
            }
        }

        #endregion
    }
}
