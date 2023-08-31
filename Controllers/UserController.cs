using JsonFlatFileDataStore;
using Microsoft.AspNetCore.Mvc;

namespace RestSample.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{   

    private readonly ILogger<UsersController> _logger;
    private readonly IDocumentCollection<User> _users;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;

        var store = new DataStore("db.json");
        _users = store.GetCollection<User>();
    }

    [HttpPost]
    public void Post([FromBody] User user)
    {
        _users.InsertOne(user);
    }

    [HttpGet]
    public IEnumerable<User> Get()
    {
        return _users.AsQueryable().ToList();
    }

    [HttpGet("{id:int}")]
    public User GetById(int id)
    {
        return _users.AsQueryable().FirstOrDefault(user => user.Id == id);
    }
    
    
}
public class User
{
    public int Id {get; set;}
    public string Name {get; set;}
}
