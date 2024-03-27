using Microsoft.AspNetCore.Mvc;
using Student.MvcApp.Models;
using Student.MvcApp.RabbitMq;
using System.Diagnostics;
namespace Student.MvcApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRabbitMqProducer _rabbitMqProducer;

    public HomeController(ILogger<HomeController> logger, IRabbitMqProducer rabbitMqProducer)
    {
        _logger = logger;
        _rabbitMqProducer = rabbitMqProducer;
    }

    public IActionResult Index()
    {


        //_rabbitMqProducer.SendMessage<string>("Burda bir problem var her seferinde yeni connection oluşturuyor fakat mesaj kuyruğa düşmüyor. ");
        Person person = new Person()
        {
            Id = 1,
            Name = "Adam",
            Age = 25
        };

        _rabbitMqProducer.SendMessage<Person>(person);

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
