using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spring.Context;
using Spring.Context.Support;

namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();//拿到容器
            IUserInfoService lister = (IUserInfoService)ctx.GetObject("UserInfoService");
            MessageBox.Show(lister.showMsg());
            
        }
    }
}
