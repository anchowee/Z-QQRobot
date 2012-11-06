using System;
using System.Collections.Generic;
using System.Text;
using QQRobot.SDK;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DemoPlug
{
    /// <summary>
    /// 标准空类库
    /// </summary>
    public class DemoPlugClass : ClientSdk
    {
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
            this.ReceiveClusterIM += new RobotEventHandler(OutPut1);
            this.ReceiveNormalIM += new RobotEventHandler(OutPut1);
            this.ReceiveTempClusterIM += new RobotEventHandler(OutPut1);
            this.ReceiveTempSession += new RobotEventHandler(OutPut1);
        }
        /// <summary>
        /// 加载设置窗体
        /// </summary>
        public override void SetForm()
        {
            new set().ShowDialog();
        }
        /// <summary>
        /// 获取笑话
        /// </summary>
        /// <returns></returns>
        private string GetXiaohua()
        {
            try
            {
                string[] xiaohua  = File.ReadAllLines(Application.StartupPath + "//笑话.txt");
                int lin = new Random().Next(0, xiaohua.Length);
                return xiaohua[lin];
            }
            catch
            {
                return "没有找到笑话文件库或者缺少字段，请检查。";
            }
        }
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="e"></param>
        public void OutPut1(MessageClass e)
        {
            if (e.Message.Contains("笑话"))
            {
                string 笑话 = GetXiaohua();
                switch (e.Event)
                {
                    case QQRobotEvent.ReceiveClusterIM:
                        SendClusterMessage(e.ClusterNum, 笑话);
                        break;
                    case QQRobotEvent.ReceiveNormalIM:
                        SendMessage(e.Sender, 笑话);
                        break;
                    case QQRobotEvent.ReceiveTempClusterIM:
                        SendTempClusterMessage(e.ClusterNum, 笑话);
                        break;
                    case QQRobotEvent.ReceiveTempSession:
                        SendTempSession(e.Sender, 笑话);
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 插件名称
        /// </summary>
        public override String PluginName { get { return "笑话插件"; } }
        /// <summary>
        /// 插件唯一名称（英文+数字）
        /// </summary>
        public override String PluginKey { get { return "Xiaohuachajian"; } }
        /// <summary>
        /// 作者
        /// </summary>
        public override string Author { get { return "天使"; } }
        /// <summary>
        /// 插件版本
        /// </summary>
        public override Version PluginVersion { get { return new Version(2, 0, 0, 0); } }
        /// <summary>
        /// 插件图片（暂时无效）
        /// </summary>
        public override Image PlugImage { get { return null; } }
        /// <summary>
        /// 插件说明
        /// </summary>
        public override string Description { get { return "安装该插件后即可随机发送笑话"; } }
        /// <summary>
        /// 安装协议，不需要请留空
        /// </summary>
        public override string Agreement { get { return "安装该插件后需要在机器人根目录新建一个“笑话.txt”文件，每行一个笑话。"; } }
    }
}
