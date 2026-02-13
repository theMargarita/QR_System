using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Helpers
{
    public static class QrGenerateHelper
    {
        // En metod som BÅDE Context och ContextPart kan använda
        public static string Generate(string prefix = "QR")
        {
            return $"{prefix}_{Guid.NewGuid().ToString("N")[..12].ToUpper()}";
        }

        // Kontrollera om token är unik i Context 
        public static async Task<bool> IsUniqueInContext(QrDbContext context, string token)
        {
            return !await context.Contexts.AnyAsync(c => c.QrToken == token);
        }

        // Kontrollera om token är unik i ContextPart-tabellen
        public static async Task<bool> IsUniqueContextParts(QrDbContext context, string token)
        {
            return !await context.ContextParts.AnyAsync(p => p.QrToken == token);
        }

        //generate unik token för Context
        public static async Task<string> GenerateUniqueForContext(QrDbContext context)
        {
            string token;
            do
            {
                token = Generate("CTX");
            } while (!await IsUniqueInContext(context, token));

            return token;   
        }
        //generate unik token för ContextPart
        public static async Task<string> GenerateUniqueForContextPart(QrDbContext context)
        {
            string token;
            do
            {
                token = Generate("TBL"); //tbl is for table 
            } while (!await IsUniqueInContext(context, token));

            return token;   
        }

        
    }
}
