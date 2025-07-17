using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SupItsTom.API
{
    public class Config
    {
        public string userAgent = "SAPIClient/1.0";
    }

    internal class Core
    {
        public void HandleError(WebException error)
        {
            if (error.Status == WebExceptionStatus.ProtocolError)
            {
                var errorType = ((HttpWebResponse)error.Response).StatusDescription;
                var rayId = $"{((HttpWebResponse)error.Response).Headers.Get("CF-RAY")}";

                throw new Exception($"------ SupItsTom API Error ------\nError: {errorType}\nID: {rayId}");
            }
        }
    }

    public class Serenity
    {
        Core Core = new Core();
        Config Config = new Config();
        private readonly string baseUrl = "https://api.supitstom.net/prod-serenity";

        #region enums
        public enum NewsType
        {
            News = 0,
            Ticker = 1,
        }

        public enum FileType
        {
            PTB = 0, XPTB = 1,
            S4 = 2, S5 = 3, S6 = 4,
            X4 = 5, X5 = 6, X6 = 7,
        }
        #endregion

        #region methods
        public string GetNewsFeed(NewsType type)
        {
            try
            {
                switch (type)
                {
                    case NewsType.News:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");

                            return webClient.DownloadString($"{baseUrl}/motd?_=news");
                        }

                    case NewsType.Ticker:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");

                            return webClient.DownloadString($"{baseUrl}/motd?_=ticker");
                        }
                }
                return null;
            }
            catch (WebException e)
            {
                Core.HandleError(e);
                return null;
            }
        }

        /// <summary>
        /// Used to download Serenity to the local user's temp directory
        /// File Name: SupItsTom.Serenity.<FileType> <- FileType being S5X or whatever
        /// </summary>
        /// <param name="type">S5 = Serenity V5 GSC, S5X = Serenity V5 Infection, ect.</param>
        public void GetFilesOfTheDay(FileType type)
        {
            try
            {
                switch (type)
                {
                    case FileType.PTB:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "0A90FDAD-5188-42D1-8110-5B8C79605C26");
                            webClient.DownloadFile($"{baseUrl}/updates", $@"{Path.GetTempPath()}/SupItsTom.Serenity.PTB");
                            return;
                        }
                    case FileType.S4:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "93CB3BD0-6495-4F24-9406-726254640F58");
                            webClient.DownloadFile($"{baseUrl}/updates", $@"{Path.GetTempPath()}/SupItsTom.Serenity.S4");
                            return;
                        }
                    case FileType.S5:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "31442EAA-F3DB-411D-9462-AC58DE0AC29D");
                            webClient.DownloadFile($"{baseUrl}/updates", $@"{Path.GetTempPath()}/SupItsTom.Serenity.S5");
                            return;
                        }
                    case FileType.S6:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "D96B84B2-6E13-47A3-BA2E-766FB71C0889");
                            webClient.DownloadFile($"{baseUrl}/updates", $@"{Path.GetTempPath()}/SupItsTom.Serenity.S6");
                            return;
                        }

                    case FileType.XPTB:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "47EA3937-2606-4426-BA90-E8F94E7CCA50");
                            webClient.DownloadFile($"{baseUrl}/updates", $@"{Path.GetTempPath()}/SupItsTom.Serenity.XPTB");
                            return;
                        }

                    case FileType.X4:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "C6B22F4F-F4A7-4A18-934B-8BB98019FEB0");
                            webClient.DownloadFile($"{baseUrl}/updates", $@"{Path.GetTempPath()}/SupItsTom.Serenity.X4");
                            return;
                        }
                    case FileType.X5:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "2DB96077-FA36-46F3-9F8A-B5B05CADB9F0");
                            webClient.DownloadFile($"{baseUrl}/updates", $@"{Path.GetTempPath()}/SupItsTom.Serenity.X5");
                            return;
                        }
                    case FileType.X6:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "2D4EF9F9-0D00-4E40-A7FE-C843EFBDFBE4");
                            webClient.DownloadFile($"{baseUrl}/updates", $@"{Path.GetTempPath()}/SupItsTom.Serenity.X6");
                            return;
                        }
                }
            }
            catch (WebException e)
            {
                Core.HandleError(e);
            }
        }

        /// <summary>
        /// Used after OAuth to lookup entitlements on user id
        /// </summary>
        /// <param name="userId">Discord User Snowflake</param>
        public bool GetEntitlements(string userId)
        {
            try
            {
                var webClient = new WebClient();
                webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                webClient.Headers.Set("X-User-ID", $"{userId}");

                string request = webClient.DownloadString($"https://serenitybot.api.supitstom.net/entitlements");

                JObject jsonObject = JObject.Parse(request);

                string isPremium = (string)jsonObject["isPremium"];

                if (isPremium == "true") return true;
                else return false;
            }
            catch (WebException e)
            {
                Core.HandleError(e);
                return false;
            }
        }
        #endregion
    }
}
