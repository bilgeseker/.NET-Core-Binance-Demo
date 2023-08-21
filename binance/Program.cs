using binance.Snippets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
//app.UseWebSockets();
//var webSocketOptions = new WebSocketOptions
//{
//    KeepAliveInterval = TimeSpan.FromMinutes(20)
//};

//app.UseWebSockets(webSocketOptions);
//app.Run(async (context) =>
//{
//    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
//    var socketFinishedTcs = new TaskCompletionSource<object>();

//    BackgroundSocketProcessor.AddSocket(webSocket, socketFinishedTcs);

//    await socketFinishedTcs.Task;
//});