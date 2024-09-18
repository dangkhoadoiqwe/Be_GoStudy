﻿using GO_Study_Logic.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.ViewModel.ZaloPay
{
    public class CreateZalopayPayRequest
    {
        public CreateZalopayPayRequest(int appId, string appUser, long appTime,
            long amount, string appTransId, string bankCode, string description, string embedData = "{}", string item = "[]")
        {
            AppId = appId;
            AppUser = appUser;
            AppTime = appTime;
            Amount = amount;
            AppTransId = appTransId;
            BankCode = bankCode;
            Description = description;
            EmbedData = embedData;
            Item = item;
        }

        public int AppId { get; set; }
        public string AppUser { get; set; } = string.Empty;
        public long AppTime { get; set; }
        public long Amount { get; set; }
        public string AppTransId { get; set; } = string.Empty;
        public string BankCode { get; set; } = string.Empty;
        public string EmbedData { get; set; } = "{}";
        public string Item { get; set; } = "[]";
        public string Mac { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Tạo chữ ký HMAC SHA256 theo định dạng yêu cầu của ZaloPay
        public void MakeSignature(string key)
        {
            var data = AppId + "|" + AppTransId + "|" + AppUser + "|" + Amount + "|"
              + AppTime + "|" + "|";

            this.Mac = HashHelper.HmacSHA256(data, key);
        }

        // Tạo nội dung gửi yêu cầu ZaloPay
        public Dictionary<string, string> GetContent()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            keyValuePairs.Add("appid", AppId.ToString());
            keyValuePairs.Add("appuser", AppUser);
            keyValuePairs.Add("apptime", AppTime.ToString());
            keyValuePairs.Add("amount", Amount.ToString());
            keyValuePairs.Add("apptransid", AppTransId);
            keyValuePairs.Add("description", Description);
            keyValuePairs.Add("bankcode", "zalopayapp");
            keyValuePairs.Add("mac", Mac);

            return keyValuePairs;
        }

        // Gửi yêu cầu tới ZaloPay và xử lý phản hồi
        public (bool, string) GetLink(string paymentUrl)
        {
            using var client = new HttpClient();
            var content = new FormUrlEncodedContent(GetContent());
            var response = client.PostAsync(paymentUrl, content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Response from ZaloPay: " + responseContent);
                var responseData = JsonConvert.DeserializeObject<CreateZalopayPayResponse>(responseContent);
                if (responseData.returnCode == 1)
                {
                    return (true, responseData.orderUrl);
                }
                else
                {
                    return (false, responseData.returnMessage);
                }
            }
            else
            {
                return (false, response.ReasonPhrase ?? string.Empty);
            }
        }
    }
}