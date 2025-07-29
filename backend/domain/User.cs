public class User {
    private int id {get;}
    private string username {get; set;}
    private string password {get; set;}
    private string email {get; set;}
    private string phone {get; set;}

    private List<Account> accounts;

    public User(int id, string username, string password, string email, string phone){
        this.id = id;
        this.username = username;
        this.password = password;
        this.email = email;
        this.phone = phone;
        this.accounts = new List<Account>();


    }
    
}