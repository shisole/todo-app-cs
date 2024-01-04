public class UserInfoDto {
    public string Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public UserInfoDto () {
        Username = string.Empty;
    }
    public UserInfoDto (User user) => (Username, FirstName, LastName) = (user.Username, user.FirstName, user.LastName);
}