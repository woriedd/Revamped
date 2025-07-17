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
            RGG = 8, RGGX = 9, KNIFE = 10, 
            DSR = 11, V2 = 12, XV2 = 13
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
                    case FileType.V2:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "93CB3BD0-6495-4F24-9406-726254640F58");
                            webClient.DownloadFile($"https://www.dropbox.com/scl/fi/f7hp3qaztp45zlc95ksov/RV2.gsc?rlkey=lttaq2sodkgy0p5yv6gi2u98f&dl=1", $@"{Path.GetTempPath()}/Revamped.V2");
                            return;
                        }
                    case FileType.XV2:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "93CB3BD0-6495-4F24-9406-726254640F58");
                            webClient.DownloadFile($"https://www.dropbox.com/scl/fi/ca43ka78vpddd5cnltarl/XRV2.xex?rlkey=1g76xggcg6rkk1ecbs1em8pj2&dl=1", $@"{Path.GetTempPath()}/Revamped.XV2");
                            return;
                        }
                    case FileType.PTB:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "93CB3BD0-6495-4F24-9406-726254640F58");
                            webClient.DownloadFile($"https://www.dropbox.com/s/v1th2hfb0ai2d9x/RevampedSND.gsc?dl=1", $@"{Path.GetTempPath()}/Revamped.SND");
                            return;
                        }
                    case FileType.XPTB:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "93CB3BD0-6495-4F24-9406-726254640F58");
                            webClient.DownloadFile($"https://www.dropbox.com/s/tj9o4fvk2tddxon/RSND.xex?dl=1", $@"{Path.GetTempPath()}/Revamped.SNDX");
                            return;
                        }
                    case FileType.S4:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "93CB3BD0-6495-4F24-9406-726254640F58");
                            webClient.DownloadFile($"https://www.dropbox.com/s/paadtxak8fzg2ft/RV.xex?dl=1", $@"{Path.GetTempPath()}/Revamped.VanillaX");
                            return;
                        }
                    case FileType.S5:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "31442EAA-F3DB-411D-9462-AC58DE0AC29D");
                            webClient.DownloadFile($"https://www.dropbox.com/s/3iolwcop8fr78l1/RevampedVanilla.gsc?dl=1", $@"{Path.GetTempPath()}/Revamped.Vanilla");
                            return;
                        }
                    case FileType.S6:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "D96B84B2-6E13-47A3-BA2E-766FB71C0889");
                            webClient.DownloadFile($"https://www.dropbox.com/s/ws1cdoeaa8pss85/6-serenity.gsc?dl=1", $@"{Path.GetTempPath()}/Serenity.S6");
                            return;
                        }
                    case FileType.X6:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "D96B84B2-6E13-47A3-BA2E-766FB71C0889");
                            webClient.DownloadFile($"https://www.dropbox.com/s/7gdufp94dyooydi/6-serenity.xex?dl=1", $@"{Path.GetTempPath()}/Revamped.X6");
                            return;
                        }
                    case FileType.RGG:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "D96B84B2-6E13-47A3-BA2E-766FB71C0889");
                            webClient.DownloadFile($"https://www.dropbox.com/scl/fi/f3ubr22n9jhd4hl67sv4j/RevampedGG.gsc?rlkey=nmv108wcsiiyi444gvm9rtfo9&dl=1", $@"{Path.GetTempPath()}/Revamped.GG");
                            return;
                        }
                    case FileType.RGGX:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "D96B84B2-6E13-47A3-BA2E-766FB71C0889");
                            webClient.DownloadFile($"https://www.dropbox.com/scl/fi/sqr5b11u3acqtzm2x7urc/RGG.xex?rlkey=mjccbv5pt1rpuao8xt4bhvc1o&dl=1", $@"{Path.GetTempPath()}/Revamped.GGX");
                            return;
                        }
                    case FileType.KNIFE:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "D96B84B2-6E13-47A3-BA2E-766FB71C0889");
                            webClient.DownloadFile($"https://www.dropbox.com/scl/fi/kdix6uwcufs1v6h7oahco/knife.xex?rlkey=29uikj0z8lg2tysr9zincftmy&dl=1", $@"{Path.GetTempPath()}/Revamped.knife");
                            return;
                        }
                    case FileType.DSR:
                        {
                            var webClient = new WebClient();
                            webClient.Headers.Set("User-Agent", $"{Config.userAgent}");
                            webClient.Headers.Set("X-GetFile", "D96B84B2-6E13-47A3-BA2E-766FB71C0889");
                            webClient.DownloadFile($"https://www.dropbox.com/scl/fi/k50i27h6qxl8vk0dlmywm/dsr.xex?rlkey=utfgb7isazfmfblr6k7oym7ot&dl=1", $@"{Path.GetTempPath()}/Revamped.dsr");
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
