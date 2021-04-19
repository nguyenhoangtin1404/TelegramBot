using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;

namespace TelegramBot
{
    class job
    {
        public string chatId { get; set;}
        public string name { get; set; }
        public bool status { get; set; }
        public string createDate { get; set; }
        public string endDate { get; set; }

        public job()
        {

        }
        public void add(string strChatId,string strName,bool bStatus, string strCreateDate, string strEndDate)
        {
            chatId = strChatId;
            name = strName;
            status = bStatus;
            createDate = strCreateDate;
            endDate = strEndDate;
        }
    }
}
