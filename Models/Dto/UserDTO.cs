namespace API.Models.Dto
{
    public class UserDTO
    {
        public string UserId { get; set; }
        public string LoginId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }

    }
}
