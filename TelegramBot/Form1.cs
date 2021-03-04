using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    public partial class Form1 : Form
    {
        static ITelegramBotClient botClient;
        static Telegram.Bot.Types.Chat chatid;
        static List<job> listJob = new List<job>();
        public Form1()
        {
            InitializeComponent();
        }

   
        static async void Bot_OnMessage(object sender, MessageEventArgs messageEventArgs)
        {

            chatid = messageEventArgs.Message.Chat;

            var message = messageEventArgs.Message;
            if (message == null || message.Type != MessageType.Text)
                return;

            switch (message.Text.Split(' ').First())
            {
                //  tự động nhắc việc
                case "/auto":
                    await auto(message);
                    break;

                // thêm việc
                case "/add":
                    await add(message);
                    break;

                // hiển thị danh sách việc
                case "/list":
                    await list(message);
                    break;

                // xóa việc
                case "/delete":
                    await delete(message);
                    break;
                // sửa việc
                case "/edit":
                    await edit(message);
                    break;
                // hoàn thành 
                case "/done":
                    await done(message);
                    break;

                default:
                    await list(message);
                    break;

            }
         }

        private static async Task  done(Telegram.Bot.Types.Message message)
        {
            await botClient.SendTextMessageAsync(
                     chatId: chatid,
                     text: "Đã hoàn thành công việc"
                   );

        }

        private static async Task edit(Telegram.Bot.Types.Message message)
        {
            await botClient.SendTextMessageAsync(
                   chatId: chatid,
                   text: "Đã sửa công việc."
                 );
        }

        private static async Task delete(Telegram.Bot.Types.Message message)
        {
            await botClient.SendTextMessageAsync(
                  chatId: chatid,
                  text: "Đã xóa công việc."
                );
        }

        private static async Task list(Telegram.Bot.Types.Message message)
        {
            string itext ="" ;
            foreach (job j in  listJob)
            {
                itext +=(j.name +"\n");
            }
            await botClient.SendTextMessageAsync(
                  chatId: chatid,
                  text: "Bạn đang có "+ listJob.Count + " công việc chưa xử lý" + itext
                );
        }

        private static async Task add(Telegram.Bot.Types.Message message)
        {
            job j = new job();
            j.add("Nội dung công việc", false, "01/01/2021", "");
            listJob.Add(j);
                await botClient.SendTextMessageAsync(
                  chatId: chatid,
                  text: "Đã thêm nội dung " + j.name +" -- Danh sách công việc có  "+ listJob.Count  +""
                );
        }
        static async Task auto(Telegram.Bot.Types.Message message)
        {
            await botClient.SendTextMessageAsync(
            chatId: chatid,
            text: "Tự động nhắc việc"
          );
        }

        void button1_Click(object sender, EventArgs e)
        {
                    botClient.StopReceiving();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            botClient = new TelegramBotClient("1675389030:AAGI74b3h8P7gwJemTwHPqiQHDHEMW2CoOs");
            var me = botClient.GetMeAsync().Result;
            richTextBox1.Text = $"I am user {me.Id} and my name is {me.FirstName}.";
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

        }
    }
}
