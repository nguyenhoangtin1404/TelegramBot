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
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace TelegramBot
{
    public partial class Form1 : Form
    {
        static ITelegramBotClient botClient;
        static Telegram.Bot.Types.Chat chatid;
        static List<job> listJob = new List<job>();
        private System.Threading.Timer timer;
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
                // hiển thị danh sách công việc đã hoàn thành
                case "/listdone":
                    await listdone(message);
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

        private static async Task done(Telegram.Bot.Types.Message message)
        { 
            
            // /done is 5 character
            string name = message.Text.Substring(5);
            foreach (job j in listJob)
            {
                if (j.chatId == chatid.ToString() && j.name == name)
                    j.status = true;
            }
            await botClient.SendTextMessageAsync(
                     chatId: chatid,
                     text: "Đã hoàn thành công việc: " + name
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
            // /delete is 7 character
            string name = message.Text.Substring(7);
            await botClient.SendTextMessageAsync(
                  chatId: chatid,
                  text: "Đã xóa công việc."
                );
        }

        private static async Task list(Telegram.Bot.Types.Message message)
        {
            string itext ="" ;
            int icount = 0;
            foreach (job j in  listJob)
            {
                if(j.chatId == chatid.ToString() && j.status == false)
                {
                    itext += (icount +". "+j.name + "\n");
                    icount++;
                }
            }
            await botClient.SendTextMessageAsync(
                  chatId: chatid,
                  text: "Bạn đang có "+ icount + " công việc chưa xử lý \n" + itext
                );
        }

        private static async Task listdone(Telegram.Bot.Types.Message message)
        {
            string itext = "";
            int dcount = 0;
            foreach (job j in listJob)
            {
                if (j.chatId == chatid.ToString() && j.status == true)
                {
                    itext += (j.name + "\n");
                    dcount++;
                }
            }
            await botClient.SendTextMessageAsync(
                  chatId: chatid,
                  text: "Bạn đang có " + dcount + " công việc đã xử lý \n" + itext
                );
        }

        private static async Task add(Telegram.Bot.Types.Message message)
        {
            job j = new job();
            // /add is 4 character
            j.add(chatid.ToString(), message.Text.Substring(4), false, System.DateTime.Now.ToString(), "");
            listJob.Add(j);
                await botClient.SendTextMessageAsync(
                  chatId: chatid,
                  text: "Đã thêm nội dung " + j.name +" Đang có "+ listJob.Count  +" Công việc"
                );
        }
       
        static async Task auto(Telegram.Bot.Types.Message message)
        {
            // /auto is 5 character
            string time = message.Text.Substring(5); 

            await botClient.SendTextMessageAsync(
            chatId: chatid,
            text: "Tự động nhắc việc"
          );
        }
        
        static async Task nhacviec(ChatId id)
        { 
            await botClient.SendTextMessageAsync(
                   chatId: id,
                   text: "Bạn đang có xxxxxx"
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
