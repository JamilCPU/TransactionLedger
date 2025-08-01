public class User {
    private int Id {get;}
    private string Username {get; set;}
    private string Password {get; set;}
    private string Email {get; set;}
    private string Phone {get; set;}

    private List<Account> Accounts;

    public User(int id, string username, string password, string email, string phone){
        this.Id = id;
        this.Username = username;
        this.Password = password;
        this.Email = email;
        this.Phone = phone;
        this.Accounts = new List<Account>();


    }
    
}