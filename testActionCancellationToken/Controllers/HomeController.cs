using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace testActionCancellationToken.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string Index()
        {
            return "ok";
        }

        /// <summary>
        /// Using CancellationTokens in ASP.NET Core MVC controllers
        /// https://andrewlock.net/using-cancellationtokens-in-asp-net-core-mvc-controllers/
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("/slow")]
        public async Task<string> Get(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"starting to do slow work - {DateTime.Now}");

            await Task.Delay(10_000, cancellationToken);

            var msg = $"Finish slow delay of 10 seconds - {DateTime.Now}";

            _logger.LogInformation(msg);

            return msg;
        }
    }
}