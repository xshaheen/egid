namespace EGID.Infrastructure.Auth.Models
{
    public class ChangePin2Model
    {
        public string CardId { get; set; }

        public string Puk { get; set; }

        public string NewPin2 { get; set; }
    }
}
