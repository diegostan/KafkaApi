using KafkaProducer.API;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ProducerService>();

var app = builder.Build();


app.MapPost("/", async ([FromServices]ProducerService producer, [FromQuery] string message) => 
{
    return await producer.SendMessage(message);
});

app.Run();
