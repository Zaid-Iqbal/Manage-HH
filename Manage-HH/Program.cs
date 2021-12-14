using System.Windows.Forms;

namespace Manage_HH
{
    class Program
    {
        public static void Main(string[] args)
        {
            SQL.con.Open();

            Application.Run(new UI());

            SQL.con.Close();
        }
    }
}
