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
            this.ReceiveKickOut += new RobotEventHandler(OutPut1);
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
            //写你的处理方法
        }
        /// <summary>
        /// 插件名称
        /// </summary>
        public override String PluginName { get { return "插件名称"; } }
        /// <summary>
        /// 插件唯一名称（英文+数字）
        /// </summary>
        public override String PluginKey { get { return "DemoPlugClass"; } }
        /// <summary>
        /// 作者
        /// </summary>
        public override string Author { get { return "您的姓名/网名"; } }
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
