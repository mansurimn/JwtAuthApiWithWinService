// See https://aka.ms/new-console-template for more information
using WindowsServiceClient;

Console.WriteLine("Hello, World!");
WindowsService service = new WindowsService();
string token = await service.AuthenticateAndGetTokenAsync("testUser", "testPassword");

// Call the protected GET endpoint
string getResult = await service.GetProductInfoAsync( token);
Console.WriteLine(getResult);
Console.ReadKey();

