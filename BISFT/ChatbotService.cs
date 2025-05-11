using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FuzzySharp;
using System.Collections.Generic;

namespace BISFT
{
    public class ChatbotService
    {
        private readonly string hfToken = "Yout_Hugging_Face_Token";
        private readonly string apiUrl = "https://api-inference.huggingface.co/models/HuggingFaceH4/zephyr-7b-beta";
        private readonly DatabaseService db;

        public ChatbotService()
        {
            db = new DatabaseService();
        }

        private string lastContext = null;

        public async Task<string> AskChatbotAsync(string userMessage)
        {
            userMessage = userMessage.ToLower().Trim();

            // Handle follow-up responses like "yes"
            if (lastContext == "segment_suggestion_pending" && (userMessage.Contains("yes") || userMessage.Contains("show")))
            {
                lastContext = null;
                return await db.GetCustomerSegmentsSummaryAsync();
            }

            var commands = new List<(string[] patterns, Func<Task<string>> action, string contextKey)>
    {
        (new[] { "inventory total", "total products", "how many products", "product count", "how many items" },
            async () => {
                lastContext = null;
                int total = await db.GetTotalInventoryItemsAsync();
                return $"📦 You have {total} products in inventory.";
            }, null),

        (new[] { "product details" },
            async () => {
                lastContext = null;
                var match = Regex.Match(userMessage, @"product\s*details\s*[:\-]?\s*(.+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string productName = match.Groups[1].Value.Trim();
                    return await db.GetProductDetailsAsync(productName);
                }
                return "❗ Please use format: Product details: ProductName";
            }, null),

        (new[] { "total revenue", "sales amount", "overall revenue" },
            async () => {
                lastContext = null;
                decimal revenue = await db.GetTotalRevenueAsync();
                return $"💰 Total revenue generated is Rs. {revenue:N2}.";
            }, null),

        (new[] { "total customers", "how many customers", "customer count" },
            async () => {
                lastContext = null;
                int customers = await db.GetTotalCustomersAsync();
                return $"👥 You have {customers} customers.";
            }, null),

        (new[] { "today sales", "today's revenue", "sales today" },
            async () => {
                lastContext = null;
                decimal sales = await db.GetTodaysSalesAsync();
                return $"📅 Today's sales amount is Rs. {sales:N2}.";
            }, null),

        (new[] { "low stock", "stock alerts", "inventory alerts" },
            async () => {
                lastContext = null;
                return await db.GetLowStockAlertsAsync();
            }, null),

        (new[] { "highest stock", "max stock", "most quantity" },
            async () => {
                lastContext = null;
                return await db.GetHighestStockProductAsync();
            }, null),

        (new[] { "lowest stock", "min stock", "least quantity" },
            async () => {
                lastContext = null;
                return await db.GetLowestStockProductAsync();
            }, null),

        (new[] { "top selling", "highest sales", "most sold" },
            async () => {
                lastContext = null;
                return await db.GetTopSellingProductAsync();
            }, null),

        (new[] { "least selling", "lowest sales", "least sold" },
            async () => {
                lastContext = null;
                return await db.GetLeastSellingProductAsync();
            }, null),

        (new[] { "most profit", "most profitable", "highest profit" },
            async () => {
                lastContext = null;
                return await db.GetMostProfitableProductAsync();
            }, null),

        (new[] { "least profit", "least profitable", "lowest profit" },
            async () => {
                lastContext = null;
                return await db.GetLeastProfitableProductAsync();
            }, null),

        (new[] { "segmentation", "customer segment", "retain", "retain customer" },
            async () => {
                lastContext = "segment_suggestion_pending";
                return
                    "🧠 Based on your customer segmentation data:\n" +
                    "• Focus on loyal/high-value customers by offering personalized discounts, early access to sales, and loyalty programs.\n" +
                    "• Re-engage at-risk segments with reminder emails or limited-time offers.\n" +
                    "• Use personalized marketing messages based on segment behaviors.\n" +
                    "• Offer support and feedback forms to improve satisfaction.\n\n" +
                    "Would you like to view customers from a specific segment?";
            }, "segment_suggestion_pending")
    };

            // Fuzzy matching logic
            foreach (var (patterns, action, contextKey) in commands)
            {
                foreach (var pattern in patterns)
                {
                    int score = FuzzySharp.Fuzz.PartialRatio(pattern, userMessage);
                    if (score >= 80)
                    {
                        lastContext = contextKey;
                        return await action();
                    }
                }
            }

            lastContext = null;
            return "❓ Sorry, I can’t help with that.";
        }


    }
}
