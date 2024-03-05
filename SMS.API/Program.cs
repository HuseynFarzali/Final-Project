using Microsoft.EntityFrameworkCore;
using SMS.DAL.Data.Database_Context;
using SMS.WebTools.Tools;

namespace SMS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplication.CreateBuilder(args).CreateWebAppUsing<Startup>().Run();
        }
    }
}