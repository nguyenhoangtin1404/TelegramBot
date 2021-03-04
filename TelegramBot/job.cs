using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;

namespace TelegramBot
{
    class job
    {
        public string name { get; set; }
        public bool status { get; set; }
        public string createDate { get; set; }
        public string endDate { get; set; }

        public job()
        {

        }
        public void add(string strName,bool bStatus, string strCreateDate, string strEndDate)
        {
            name = strName;
            status = bStatus;
            createDate = strCreateDate;
            endDate = strEndDate;
        }
    }
}
