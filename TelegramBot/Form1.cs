using System;
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
        public Form1()
        {
            InitializeComponent();
        }

   
        static async void Bot_OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            if (messageEventArgs.Message.Text != null)
                await botClient.SendTextMessageAsync(
                  chatId: messageEventArgs.Message.Chat,
                  text: "You said:\n" + messageEventArgs.Message.Text
                );

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

        private static Task done(Telegram.Bot.Types.Message message)
        {
            job j = new job();
            j.add("x",false,"01/01/2021","");
            throw new NotImplementedException();
        }

        private static Task edit(Telegram.Bot.Types.Message message)
        {
            throw new NotImplementedException();
        }

        private static Task delete(Telegram.Bot.Types.Message message)
        {
            throw new NotImplementedException();
        }

        private static Task list(Telegram.Bot.Types.Message message)
        {
            throw new NotImplementedException();
        }

        static async Task add(Telegram.Bot.Types.Message message)
        {
                Console.WriteLine(message);
        }
        static async Task auto(Telegram.Bot.Types.Message message)
        {
                throw new NotImplementedException();
        }

        void button1_Click(object sender, EventArgs e)
        {
                    botClient.StopReceiving();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            botClient = new TelegramBotClient("1675389030:AAGI74b3h8P7gwJemTwHPqiQHDHEMW2CoOs");
            var me = botClient.GetMeAsync().Result;
            richTextBox1.Text = $"Hello, World! I am user {me.Id} and my name is {me.FirstName}.";
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

        }
    }
}
