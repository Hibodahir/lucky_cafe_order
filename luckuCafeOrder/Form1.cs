using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace luckuCafeOrder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
                    }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String customer_name = txtcustomername.Text;

                if (customer_name == "")
                {
                    MessageBox.Show("pls enter name");
                }
                else
                {
                    int quantity = int.Parse(txtquantity.Text);

                    if (quantity < 1)
                    {
                        MessageBox.Show("quantity must be 1 or more");
                    }
                    else if (rbtea.Checked == false && rbcoffe.Checked == false && rbjuice.Checked == false)
                    {
                        MessageBox.Show("pls select a drink");
                    }
                    else
                    {
                        double price = 0;

                        String drink = "";

                        if (rbtea.Checked)
                        {
                            price = 1;
                            drink = "Tea";
                        }
                        else if (rbcoffe.Checked)
                        {
                            price = 2;
                            drink = "Coffee";
                        }
                        else if (rbjuice.Checked)
                        {
                            price = 3;
                            drink = "Juice";
                        }
                        LstOrderLisft.Items.Clear();
                        Random rand = new Random();
                        bool lucky = false;
                        

                        for (int i = 1; i <= quantity; i++)
                        {
                            int luckynumber = rand.Next(1, 51);
                            LstOrderLisft.Items.Add(i + " ) " + customer_name + " - " + drink + " - lucky: " + luckynumber + "- $" +
                                 price.ToString("f2"));

                            if (luckynumber == 7 || luckynumber == 17)

                            {
                                lucky = true;
                            }

                        }
                        double total = price * quantity;
                        double beforediscount = total;
                        double afterdiscount = total;

                        if (cbxstudent.Checked)
                        {



                            double discount = total * 0.10;
                            afterdiscount = total - discount;

                            LstOrderLisft.Items.Add("-----------------");
                            LstOrderLisft.Items.Add("Member discount");
                            LstOrderLisft.Items.Add("before discount" + beforediscount.ToString("f2"));
                            LstOrderLisft.Items.Add("after discount" + afterdiscount.ToString("f2"));

                            total = afterdiscount;



                        }
                        
                        lbltotalprice.Text = "Total price:$ " + total.ToString("f2");

                        if (lucky)
                        {
                            lblluckymassege.Text = "You got a free  cookie!";
                            LstOrderLisft.Items.Add("----------------");
                            LstOrderLisft.Items.Add("you got a free cookie");

                        }
                        else
                        {
                            lblluckymassege.Text = "No prize this time.";
                            LstOrderLisft.Items.Add("----------------");
                            LstOrderLisft.Items.Add("no prize this time");
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (LstOrderLisft.Items.Count == 0)
            {
                MessageBox.Show("no order to save");
            }
            else
            {
                StreamWriter writter = File.AppendText("orders.txt");

                for (int i = 0; i < LstOrderLisft.Items.Count; i++)
                {
                    writter.WriteLine(LstOrderLisft.Items[i]);


                }
                writter.WriteLine(lbltotalprice.Text);
                writter.WriteLine(lblluckymassege.Text);

                writter.Close();


                MessageBox.Show("order saved");




            }

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtcustomername.Text = "";


            txtquantity.Text = "";
            lbltotalprice.Text = "";
            lblluckymassege.Text = "";
            LstOrderLisft.Items.Clear();
            cbxstudent.Checked = false;



        }

        private void btnloadfromfile_Click(object sender, EventArgs e)
        {
            if (File.Exists("orders.txt") == false)
            {
                MessageBox.Show("no file found");
            }
            else
            {
                try
                {
                    LstOrderLisft.Items.Clear();
                    StreamReader readerfile;
                    readerfile = File.OpenText("orders.txt");

                    while (!readerfile.EndOfStream)
                    {
                        String line = readerfile.ReadLine();
                        LstOrderLisft.Items.Add(line);

                    }
                    readerfile.Close();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}