namespace EGID.Infrastructure.Auth.Services.Dto
{
    public class CreateCardDto
    {
        public string OwnerId { get; set; }

        public string Puk { get; set; }

        public string Pin1 { get; set; }

        public string Pin2 { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}