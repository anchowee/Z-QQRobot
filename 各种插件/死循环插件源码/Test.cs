using System;
using System.Collections.Generic;
using System.Text;
using QQRobot.SDK;
using System.Threading;
using System.Drawing;

namespace DemoPlug
{
    /// <summary>
    /// 标准空类库
    /// </summary>
    public class DemoPlugClass : ClientSdk
    {
        uint Cluster = 0;
        Thread th;
        /// <summary>
        /// 安装插件
        /// </summary>
        public override bool Install()
        {
            return true;
        }
        /// <summary>
        /// 初始化插件
        /// </summary>
        public override void Init()
        {
            //加载您需要处理的事件
            this.ReceiveTempClusterIM += new RobotEventHandler(OutPut1);

        }
        /// <summary>
        /// 加载设置窗体
        /// </summary>
        public override void SetForm()
        {
            new set().ShowDialog();
        }
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="e"></param>
        public void OutPut1(MessageClass e)
        {
            if (e.Message == "start")
            {
                Cluster = e.ClusterNum;
                for (int i = 10; i > 0; i--)
                {
                    SendTempClusterMessage(Cluster, string.Format("程序进入倒计时，剩余：{0}秒" ,i));
                    Thread.Sleep(1000);
                }
                SendTempClusterMessage(Cluster, string.Format("请稍等，程序正在初始化。"));
                Thread.Sleep(1000);
                th = new Thread(new ThreadStart(ThreadBody));
                th.Start();
            }
            else if (e.Message == "stop")
            {
                try
                {
                    th.Abort();
                }
                catch
                {

                }
            }
            else
            {
                return;
            }
        }
        private void ThreadBody()
        {
            while (true)
            {
                SendTempClusterMessage(Cluster, GetRnd(20 ,true ,true ,true ,true ,""));
                Thread.Sleep(1);
            }
        }



        //随机字符串生成器的主要功能如下： 
        //1、支持自定义字符串长度
        //2、支持自定义是否包含数字
        //3、支持自定义是否包含小写字母
        //4、支持自定义是否包含大写字母
        //5、支持自定义是否包含特殊符号
        //6、支持自定义字符集
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length">目标字符串的长度</param>
        /// <param name="useNum">是否包含数字，1=包含，默认为包含</param>
        /// <param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        /// <param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        /// <param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        /// <param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        /// <returns>指定长度的随机字符串</returns>
        public string GetRnd(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;

            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }

            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }

            return s;
        }
        /// <summary>
        /// 插件名称
        /// </summary>
        public override String PluginName { get { return "shuataolunzu"; } }
        /// <summary>
        /// 插件唯一名称（英文+数字）
        /// </summary>
        public override String PluginKey { get { return "shuataolunzu"; } }
        /// <summary>
        /// 作者
        /// </summary>
        public override string Author { get { return "aaaaaaa"; } }
        /// <summary>
        /// 插件版本
        /// </summary>
        public override Version PluginVersion { get { return new Version(0, 0, 0, 0); } }
        /// <summary>
        /// 插件图片（暂时无效）
        /// </summary>
        public override Image PlugImage { get { return null; } }
        /// <summary>
        /// 插件说明
        /// </summary>
        public override string Description { get { return "您的说明"; } }
        /// <summary>
        /// 安装协议，不需要请留空
        /// </summary>
        public override string Agreement { get { return "安装协议"; } }
    }
}
