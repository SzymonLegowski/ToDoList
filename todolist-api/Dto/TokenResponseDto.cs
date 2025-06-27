namespace todolist_api.Dto
{
    public class TokenResponseDto
    {
        public required int UserId { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}